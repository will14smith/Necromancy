using Necromancy.Data;
using Necromancy.Models;

namespace Necromancy;

public class RitualEnumerator
{
    public static IEnumerable<Ritual> ApplyAlterations(IEnumerable<Ritual> rituals, int level)
    {
        var spaces = level switch
        {
            >= 90 => 11,
            >= 60 => 7,
            _ => 5,
        };

        var alterations = Glyphs.All
            .Where(x => x.IsAlteration && x.Level <= level)
            .ToList();
        
        foreach (var ritual in rituals)
        {
            yield return ritual;
            
            if (ritual.Duration == 0)
            {
                // not a real ritual
                continue;
            }
            
            var usedGlyphs = ritual.Glyphs.Sum(x => x.Amount);
            var freeSpaces = spaces - usedGlyphs;
            if (freeSpaces <= 0)
            {
                continue;
            }
            
            // TODO handle all combinations
            foreach (var alteration in alterations)
            {
                yield return ritual with
                {
                    Name = ritual.Name + $" (+ {freeSpaces} {alteration.Name})",
                    Glyphs = ritual.Glyphs.Append(alteration.Count(freeSpaces)).ToList()
                };
            }
        }
    }

    public static IEnumerable<Ritual> ApplyGlyphDuration(IEnumerable<Ritual> rituals) => rituals.SelectMany(ApplyGlyphDuration);
    public static IEnumerable<Ritual> ApplyGlyphDuration(Ritual ritual)
    {
        // just assume all are in-sync and fresh at the start of a ritual
        // not assuming that would be... fun (:
        var lcm = ritual.Glyphs.Select(x => x.Glyph.Duration).Aggregate(1, LeastCommonMultiple);

        for (var i = 1; i <= lcm; i++)
        {
            var glyphs = new List<GlyphCount>();
            foreach (var (glyph, count) in ritual.Glyphs)
            {
                var multiple = (int)Math.Ceiling(i / (double)glyph.Duration);
                if (glyph.IsAlteration)
                {
                    // to avoid rounding issues we pre-apply the alterations
                    glyphs.Add(glyph.WithoutAlteration.Count(multiple * count));
                }
                else
                {
                    glyphs.Add(glyph.Count(multiple * count));
                }
            }
            
            var outputMultiplier = 1d;
            outputMultiplier += ritual.Glyphs.Sum(x => x.OutputPercentBoost) / 100d;
            
            var inputs = ritual.Inputs.Select(x => x.Item.Count(i * x.Amount)).ToList();
            var outputs = ritual.Outputs.Select(x => x.Item.Count(i * (int)Math.Floor(x.Amount * outputMultiplier))).ToList();
            
            yield return new Ritual(
                $"{i} * {ritual.Name}",
                ritual.Level,
                i * ritual.Duration,
                i * ritual.Experience,
                glyphs,
                inputs,
                outputs
            );
        }
    }

    private static int LeastCommonMultiple(int a, int b) => Math.Abs(a * b) / GreatestCommonDivisor(a, b);
    private static int GreatestCommonDivisor(int a, int b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }
        
        return a;
    }
}