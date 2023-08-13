namespace Necromancy.Models;

public record GlyphCount(Glyph Glyph, int Amount)
{
    public double Delta(Item item) => Glyph.Delta(item) * Amount;
    public int OutputPercentBoost => Glyph.OutputPercentBoost * Amount;
    public int SpeedPercentBoost => Glyph.SpeedPercentBoost * Amount;
}