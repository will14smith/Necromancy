namespace Necromancy.Models;

public record Ritual(string Name, int Level, int Duration, int Experience, IReadOnlyCollection<GlyphCount> Glyphs, IReadOnlyCollection<ItemCount> Inputs, IReadOnlyCollection<ItemCount> Outputs)
{
    public double Delta(Item item)
    {
        var outputMultiplier = 1d;
        outputMultiplier += Glyphs.Sum(x => x.OutputPercentBoost) / 100d;
        
        var delta = 0d;

        delta += Glyphs.Sum(x => x.Delta(item));
        delta -= Inputs.Sum(x => x.Delta(item));
        delta += Math.Floor(Outputs.Sum(x => x.Delta(item)) * outputMultiplier);

        return delta;
    }

    public int EffectiveDuration
    {
        get
        {
            var speedMultiplier = 1d;
            speedMultiplier += Math.Min(Glyphs.Sum(x => x.SpeedPercentBoost), 50) / 100d;

            return (int)Math.Ceiling(Duration * speedMultiplier);
        }
    }
}