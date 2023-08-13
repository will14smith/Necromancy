using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Google.OrTools.LinearSolver;
using Necromancy.Data;
using Necromancy.Models;

namespace Necromancy;

public class RitualSolver
{
    private static readonly Item InitialItem = new("Initial");
    
    public static IReadOnlyDictionary<Ritual, int> Solve(IReadOnlyCollection<Ritual> rituals, IReadOnlyDictionary<Item, int> initial, IReadOnlyDictionary<Item, int> goal)
    {
        var timeout = TimeSpan.FromSeconds(5);

        var solver = Solver.CreateSolver("SCIP");
        solver.SetTimeLimit((long)timeout.TotalMilliseconds);

        var denormalisedRituals = RitualEnumerator.ApplyGlyphDuration(rituals).ToList();
        
        var initialItems = initial.Select(x => x.Key.Count(x.Value)).Append(InitialItem.Count(1)).ToList();
        var initialRitual = new Ritual(InitialItem.Name, 0, 0, 0, Array.Empty<GlyphCount>(), Array.Empty<ItemCount>(), initialItems);
        denormalisedRituals.Add(initialRitual);

        
        var ritualVariables = CreateVariables(solver, denormalisedRituals);
        
        var initialConstraint = solver.MakeConstraint(0, 1, InitialItem.Name);
        initialConstraint.SetCoefficient(ritualVariables[initialRitual], 1);
        
        SetConstraints(solver, ritualVariables, goal);
        SetObjective(solver, ritualVariables);

        Solve(solver);

        return ExtractSolution(ritualVariables, rituals);
    }
    
    private static IReadOnlyDictionary<Ritual, Variable> CreateVariables(Solver solver, IReadOnlyCollection<Ritual> rituals) => 
        rituals.ToDictionary(ritual => ritual, ritual => solver.MakeIntVar(0, double.PositiveInfinity, ritual.Name));

    private static void SetConstraints(Solver solver, IReadOnlyDictionary<Ritual, Variable> ritualVariables, IReadOnlyDictionary<Item, int> goal)
    {
        var ritualGeneratedItems = ritualVariables.Keys.SelectMany(x => x.Outputs.Select(y => y.Item)).ToHashSet();

        foreach (var item in Items.All)
        {
            var minTarget = double.NegativeInfinity;
            if (goal.TryGetValue(item, out var target))
            {
                minTarget = target;
            }
            else if (ritualGeneratedItems.Contains(item))
            {
                minTarget = 0;
            }

            var constraint = solver.MakeConstraint(minTarget, double.PositiveInfinity, item.Name);
            foreach (var (ritual, ritualVariable) in ritualVariables)
            {
                var itemDelta = ritual.Delta(item);

                constraint.SetCoefficient(ritualVariable, itemDelta);
            }
        }
    }
    
    private static void SetObjective(Solver solver, IReadOnlyDictionary<Ritual, Variable> ritualVariables)
    {
        var objective = solver.Objective();
        foreach (var (ritual, ritualVariable) in ritualVariables)
        {
            // optimise for time
            objective.SetCoefficient(ritualVariable, ritual.EffectiveDuration);
            
            // optimise for operations (AFK)
            // objective.SetCoefficient(ritualVariable, 1);
            
            // TODO optimise for cost (i.e. subj robes)
        }

        objective.SetMinimization();
    }
    
    private static void Solve(Solver solver)
    {
        var status = solver.Solve();
        switch (status)
        {
            case Solver.ResultStatus.OPTIMAL:
                return;
            
            case Solver.ResultStatus.FEASIBLE:
                Console.WriteLine("Found a solution but it's maybe not optimal");
                return;
            
            default:
                throw new InvalidOperationException($"solver returned {status}");
        }
    }

    private static readonly Regex _ritualNormalisationRegex = new Regex(@"^(\d+) \* (.+)$");
    
    private static IReadOnlyDictionary<Ritual, int> ExtractSolution(IReadOnlyDictionary<Ritual, Variable> ritualVariables, IReadOnlyCollection<Ritual> originalRituals)
    {
        var solution = new ConcurrentDictionary<Ritual, int>();
        foreach (var (ritual, ritualVariable) in ritualVariables)
        {
            if (ritual.Name == InitialItem.Name)
            {
                continue;
            }
            
            var count = ritualVariable.SolutionValue();
            if (count > 0)
            {
                var (normalisedRitual, normalisedCount) = NormaliseRitual(ritual, originalRituals);
                var totalCount = normalisedCount * (int)Math.Ceiling(count);
                
                solution.AddOrUpdate(normalisedRitual, static (_, x) => x, static (_, x, y) => x + y, totalCount);
            }
        }

        return solution;
    }

    private static (Ritual Ritual, int Count) NormaliseRitual(Ritual ritual, IReadOnlyCollection<Ritual> originalRituals)
    {
        // return (ritual, 1);
        
        var normalisationMatch = _ritualNormalisationRegex.Match(ritual.Name);
        if (!normalisationMatch.Success)
        {
            throw new InvalidOperationException($"failed to normalise ritual: {ritual.Name}");
        }

        var count = int.Parse(normalisationMatch.Groups[1].Value);
        var name = normalisationMatch.Groups[2].Value;
        var normalisedRitual = originalRituals.First(x => x.Name == name);

        return (normalisedRitual, count);
    }
}