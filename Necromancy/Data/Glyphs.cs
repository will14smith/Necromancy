using System.Reflection;
using Necromancy.Models;

namespace Necromancy.Data;

public class Glyphs
{
    public static readonly Glyph Attraction1 = new("Attraction I", 61, 3, new []{ Items.Ectoplasm.Count(1), Items.RegularInk.Count(2), Items.BasicInk.Count(2) });
    public static readonly Glyph Attraction2 = new("Attraction II", 95, 6, new []{ Items.Ectoplasm.Count(1), Items.PowerfulInk.Count(2), Items.GreaterInk.Count(1) });
    public static readonly Glyph Attraction3 = new("Attraction III", 107, 9, new []{ Items.Ectoplasm.Count(2), Items.PowerfulInk.Count(2), Items.GreaterInk.Count(2) });
    
    public static readonly Glyph Change1 = new("Change I", 20, 6, new []{ Items.RegularInk.Count(2), Items.BasicInk.Count(1) });
    public static readonly Glyph Change2 = new("Change II", 60, 12, new []{ Items.GreaterInk.Count(2), Items.RegularInk.Count(1) });
    public static readonly Glyph Change3 = new("Change III", 95, 18, new []{ Items.PowerfulInk.Count(2), Items.GreaterInk.Count(1) });
    
    public static readonly Glyph Commune1 = new("Commune I", 1, 6, new []{ Items.BasicInk.Count(4) });
    public static readonly Glyph Commune2 = new("Commune II", 60, 12, new []{ Items.GreaterInk.Count(4) });
    public static readonly Glyph Commune3 = new("Commune III", 90, 18, new []{ Items.PowerfulInk.Count(4) });
    
    public static readonly Glyph Elemental1 = new("Elemental I", 1, 6, new []{ Items.BasicInk.Count(3) });
    public static readonly Glyph Elemental2 = new("Elemental II", 60, 12, new []{ Items.RegularInk.Count(3), Items.BasicInk.Count(2) });
    public static readonly Glyph Elemental3 = new("Elemental III", 90, 18, new []{ Items.GreaterInk.Count(3), Items.RegularInk.Count(2) });
    
    public static readonly Glyph Multiply1 = new("Multiply I", 30, 3, new []{ Items.RegularInk.Count(1), Items.BasicInk.Count(1) }, OutputPercentBoost: 20);
    public static readonly Glyph Multiply2 = new("Multiply II", 66, 6, new []{ Items.RegularInk.Count(2), Items.BasicInk.Count(2) }, OutputPercentBoost: 40);
    public static readonly Glyph Multiply3 = new("Multiply III", 103, 9, new []{ Items.PowerfulInk.Count(2), Items.GreaterInk.Count(2) }, OutputPercentBoost: 60);
    
    public static readonly Glyph Protection1 = new("Protection I", 36, 3, new []{ Items.Ectoplasm.Count(1), Items.RegularInk.Count(2), Items.BasicInk.Count(1) });
    public static readonly Glyph Protection2 = new("Protection II", 69, 6, new []{ Items.Ectoplasm.Count(1), Items.RegularInk.Count(2), Items.BasicInk.Count(2) });
    public static readonly Glyph Protection3 = new("Protection III", 101, 9, new []{ Items.Ectoplasm.Count(2), Items.PowerfulInk.Count(2), Items.GreaterInk.Count(2) });
    
    public static readonly Glyph Reagent1 = new("Reagent I", 5, 6, new []{ Items.BasicInk.Count(2) });
    public static readonly Glyph Reagent2 = new("Reagent II", 60, 12, new []{ Items.RegularInk.Count(2), Items.BasicInk.Count(3) });
    public static readonly Glyph Reagent3 = new("Reagent III", 90, 18, new []{ Items.GreaterInk.Count(2), Items.RegularInk.Count(3) });
    
    public static readonly Glyph Speed1 = new("Speed I", 45, 3, new []{ Items.Ectoplasm.Count(1), Items.RegularInk.Count(2), Items.BasicInk.Count(1) }, SpeedPercentBoost: 5);
    public static readonly Glyph Speed2 = new("Speed II", 81, 6, new []{ Items.Ectoplasm.Count(1), Items.GreaterInk.Count(2), Items.RegularInk.Count(2) }, SpeedPercentBoost: 10);
    public static readonly Glyph Speed3 = new("Speed III", 102, 9, new []{ Items.Ectoplasm.Count(2), Items.PowerfulInk.Count(2), Items.GreaterInk.Count(2) }, SpeedPercentBoost: 15);
    
    public static IReadOnlyCollection<Glyph> All => typeof(Glyphs).GetFields(BindingFlags.Public | BindingFlags.Static).Where(x => x.Name != nameof(All)).Select(x => (Glyph) x.GetValue(null)!).ToList();
}