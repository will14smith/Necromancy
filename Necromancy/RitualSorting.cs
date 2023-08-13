using Necromancy.Models;

namespace Necromancy;

public static class RitualSorting
{
    public static IEnumerable<(Ritual Ritual, int Count)> Topological(IReadOnlyDictionary<Ritual, int> rituals)
    {
        var sorted = new List<(Ritual Ritual, int Count)>();
        var permMarked = new HashSet<Ritual>();

        var ritualsByInput = rituals
            .SelectMany(x => x.Key.Inputs.Select(input => (Input: input.Item, Ritual: x.Key))
                .Union(x.Key.Glyphs.SelectMany(g => g.Glyph.Items.Select(i => (Input: i.Item, Ritual: x.Key)))))
            .GroupBy(x => x.Input, x => x.Ritual)
            .ToDictionary(x => x.Key, x => x.ToList());
        
        while (permMarked.Count < rituals.Count)
        {
            var n = rituals.First(x => !permMarked.Contains(x.Key));
            Visit(n.Key);
        }

        return sorted;
        
        void Visit(Ritual ritual)
        {
            if (permMarked.Contains(ritual))
            {
                return;
            }
            permMarked.Add(ritual);

            foreach (var m in OutboundEdges(ritualsByInput, ritual))
            {
                Visit(m);
            }

            sorted.Insert(0, (ritual, rituals[ritual]));
        }
    }

    private static IEnumerable<Ritual> OutboundEdges(IReadOnlyDictionary<Item, List<Ritual>> ritualsByInput, Ritual ritual)
    {
        var edges = new HashSet<Ritual>();
        
        foreach (var output in ritual.Outputs)
        {
            if (ritualsByInput.TryGetValue(output.Item, out var others))
            {
                edges.UnionWith(others);
            }
        }

        return edges;
    }
}