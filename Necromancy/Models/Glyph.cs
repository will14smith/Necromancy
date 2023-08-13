namespace Necromancy.Models;

public record Glyph(string Name, int Level, int Duration, IReadOnlyCollection<ItemCount> Items, int OutputPercentBoost = 0, int SpeedPercentBoost = 0)
{
    public bool IsAlteration => OutputPercentBoost != 0 || SpeedPercentBoost != 0;
    public Glyph WithoutAlteration => this with { OutputPercentBoost = 0, SpeedPercentBoost = 0 };
    
    public GlyphCount Count(int count) => new(this, count);
    public double Delta(Item item)
    {
        return -Items.Sum(x => x.Delta(item));
    }
}