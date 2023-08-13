using System.Reflection;
using Necromancy.Models;

namespace Necromancy.Data;

public class Rituals
{
    public static readonly Ritual LesserNecroplasm = new("Lesser necroplasm", 5, 33, 175,
        new[] { Glyphs.Elemental1.Count(1), Glyphs.Reagent1.Count(2) }, 
        new[] { Items.WeakNecroplasm.Count(200) },
        new[] { Items.LesserNecroplasm.Count(100), Items.Ectoplasm.Count(3) });
    public static readonly Ritual GreaterNecroplasm = new("Greater necroplasm", 60, 58, 740,
        new[] { Glyphs.Elemental1.Count(1), Glyphs.Elemental2.Count(2), Glyphs.Reagent2.Count(2) }, 
        new[] { Items.LesserNecroplasm.Count(200) },
        new[] { Items.GreaterNecroplasm.Count(100), Items.Ectoplasm.Count(7) });
    public static readonly Ritual PowerfulNecroplasm = new("Powerful necroplasm", 90, 99, 3300, // TODO duration
        new[] { Glyphs.Elemental2.Count(2), Glyphs.Elemental3.Count(4), Glyphs.Reagent3.Count(2) }, 
        new[] { Items.GreaterNecroplasm.Count(200) },
        new[] { Items.PowerfulNecroplasm.Count(100), Items.Ectoplasm.Count(15) });
    
    // TODO essence rituals
    
    public static readonly Ritual GreaterCommunionDragonkinBones = new("Greater communion (Dragonkin bones)", 60, 58, 740,
        new[] { Glyphs.Elemental2.Count(2), Glyphs.Commune1.Count(1), Glyphs.Commune2.Count(2) }, 
        new[] { Items.DragonkinBones.Count(1) },
        new[] { Items.Soul.Count(20), Items.Ectoplasm.Count(7) });
    public static readonly Ritual PowerfulCommunionDinosaurBones = new("Powerful communion (Dinosaur bones)", 90, 99, 3300, // TODO duration
        new[] { Glyphs.Elemental3.Count(4), Glyphs.Commune2.Count(2), Glyphs.Commune3.Count(2) }, 
        new[] { Items.DinosaurBones.Count(1) },
        new[] { Items.Soul.Count(30), Items.Ectoplasm.Count(15) });

    public static readonly Ritual LesserEnsoulMaterialBar = new("Lesser ensoul material (Bar)", 20, 39, 225,
        new[] { Glyphs.Elemental1.Count(2), Glyphs.Change1.Count(1) }, 
        new[] { Items.LesserUnensouledBar.Count(1) },
        new[] { Items.LesserEnsouledBar.Count(1), Items.Ectoplasm.Count(3) });
    public static readonly Ritual LesserEnsoulMaterialCloth = new("Lesser ensoul material (Cloth)", 20, 39, 225,
        new[] { Glyphs.Elemental1.Count(2), Glyphs.Change1.Count(1) }, 
        new[] { Items.SpiderSilkItem.Count(1) },
        new[] { Items.LesserEnsouledCloth.Count(1), Items.Ectoplasm.Count(3) });
    public static readonly Ritual LesserEnsoulMaterialThread = new("Lesser ensoul material (Thread)", 20, 39, 225,
        new[] { Glyphs.Elemental1.Count(2), Glyphs.Change1.Count(1) }, 
        new[] { Items.Thread.Count(1) },
        new[] { Items.LesserEnsouledThread.Count(1), Items.Ectoplasm.Count(3) });
    public static readonly Ritual EnsoulMaterialBar = new("Ensoul material (Bar)", 60, 58, 740,
        new[] { Glyphs.Elemental1.Count(1), Glyphs.Elemental2.Count(2), Glyphs.Change2.Count(2) }, 
        new[] { Items.UnensouledBar.Count(1) },
        new[] { Items.EnsouledBar.Count(1), Items.Ectoplasm.Count(7) });
    public static readonly Ritual EnsoulMaterialCloth = new("Ensoul material (Cloth)", 60, 58, 740,
        new[] { Glyphs.Elemental1.Count(1), Glyphs.Elemental2.Count(2), Glyphs.Change2.Count(2) }, 
        new[] { Items.MysticItem.Count(1) },
        new[] { Items.EnsouledCloth.Count(1), Items.Ectoplasm.Count(7) });
    public static readonly Ritual EnsoulMaterialThread = new("Ensoul material (Thread)", 60, 58, 740,
        new[] { Glyphs.Elemental1.Count(1), Glyphs.Elemental2.Count(2), Glyphs.Change2.Count(2) }, 
        new[] { Items.SeasonalWool.Count(1) },
        new[] { Items.EnsouledThread.Count(1), Items.Ectoplasm.Count(7) });
    public static readonly Ritual GreaterEnsoulMaterialBar = new("Greater ensoul material (Bar)", 80, 70, 1776,
        new[] { Glyphs.Elemental2.Count(3), Glyphs.Change2.Count(2) }, 
        new[] { Items.GreaterUnensouledBar.Count(1) },
        new[] { Items.GreaterEnsouledBar.Count(1), Items.Ectoplasm.Count(7) });
    public static readonly Ritual GreaterEnsoulMaterialCloth = new("Greater ensoul material (Cloth)", 80, 70, 1776,
        new[] { Glyphs.Elemental2.Count(3), Glyphs.Change2.Count(2) }, 
        new[] { Items.SubjugationItem.Count(1) },
        new[] { Items.GreaterEnsouledCloth.Count(1), Items.Ectoplasm.Count(7) });
    public static readonly Ritual GreaterEnsoulMaterialThread = new("Greater ensoul material (Thread)", 80, 70, 1776,
        new[] { Glyphs.Elemental2.Count(3), Glyphs.Change2.Count(2) }, 
        new[] { Items.AlgarumThread.Count(1) },
        new[] { Items.GreaterEnsouledThread.Count(1), Items.Ectoplasm.Count(7) });
    
    // fake "rituals" but makes things simpler
    public static readonly Ritual RegularInk = new("Regular ink", 20, 0, 21, Array.Empty<GlyphCount>(), 
        new[] { Items.LesserNecroplasm.Count(20), Items.VialOfWater.Count(1), Items.Ashes.Count(1) },
        new[] { Items.RegularInk.Count(1) });
    public static readonly Ritual GreaterInk = new("Greater ink", 60, 0, 44, Array.Empty<GlyphCount>(), 
        new[] { Items.GreaterNecroplasm.Count(20), Items.VialOfWater.Count(1), Items.Ashes.Count(1) },
        new[] { Items.GreaterInk.Count(1) });
    public static readonly Ritual PowerfulInk = new("Powerful ink", 90, 0, 60, Array.Empty<GlyphCount>(), 
        new[] { Items.PowerfulNecroplasm.Count(20), Items.VialOfWater.Count(1), Items.Ashes.Count(1) },
        new[] { Items.PowerfulInk.Count(1) });
    
    public static IReadOnlyCollection<Ritual> All => typeof(Rituals).GetFields(BindingFlags.Public | BindingFlags.Static).Where(x => x.Name != nameof(All)).Select(x => (Ritual) x.GetValue(null)!).ToList();
}