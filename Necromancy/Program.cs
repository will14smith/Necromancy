using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using Necromancy;
using Necromancy.Data;
using Necromancy.Models;

var initial = new State(new []
{
    Items.WeakNecroplasm.Count(45277),
    Items.LesserNecroplasm.Count(6197),
    Items.GreaterNecroplasm.Count(360),

    Items.BasicInk.Count(63173),
    Items.RegularInk.Count(148),
    Items.GreaterInk.Count(24),
    
    Items.Soul.Count(5000),
});
var goal = new State(new[]
{     
    // T70->T80 weapon upgrade
    Items.GreaterEnsouledBar.Count(6),
    
    // T80->T90 weapon upgrade
    // Items.GreaterEnsouledBar.Count(10),
    
    // T70->T80 armour upgrade
    // Items.GreaterEnsouledCloth.Count(7),
    // Items.GreaterEnsouledThread.Count(5),
    
    // T80->T90 armour upgrade
    // Items.GreaterEnsouledCloth.Count(13),
    // Items.GreaterEnsouledThread.Count(10),
    
    // 4000 souls (for T80 talents)
    Items.Soul.Count(8500),
    
    // 27000 souls (for T90 talents)
    // Items.Soul.Count(36000),
});

// actual code below here

var initialNormalised = initial.ToDictionary();
var goalNormalised = goal.ToDictionary();

var rituals = Rituals.All;
rituals = RitualEnumerator.ApplyAlterations(rituals, 80).ToList();

var timer = new Stopwatch();
timer.Start();
var solution = RitualSolver.Solve(rituals, initialNormalised, goalNormalised);
timer.Stop();

Console.Clear();
Console.WriteLine($"Solved after {timer.Elapsed}");
Console.WriteLine();

var solutionTime = 0d;
var solutionDeltas = new ConcurrentDictionary<Item, double>();
var solutionExperience = 0d;

var sortedSolution = RitualSorting.Topological(solution);

foreach (var (ritual, count) in sortedSolution)
{
    solutionTime += count * ritual.Duration;
    solutionExperience += count * ritual.Experience;

    foreach (var item in Items.All)
    {
        var itemDelta = ritual.Delta(item) * count;
        solutionDeltas.AddOrUpdate(item, static (_, x) => x, static (_, x, y) => x + y, itemDelta);
    }
    
    Console.WriteLine($"{count,3} * {ritual.Name}");
}

Console.WriteLine();
Console.WriteLine($"Duration: {TimeSpan.FromSeconds(solutionTime)}");
Console.WriteLine($"Experience: {solutionExperience:N0}");
// Console.WriteLine();
//
// foreach (var (item, count) in solutionDeltas.OrderBy(x => x.Value))
// {
//     if (count != 0)
//     {
//         Console.WriteLine($"{Math.Ceiling(count),5} * {item.Name}");
//     }
// }