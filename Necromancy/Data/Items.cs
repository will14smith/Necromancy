using System.Reflection;
using Necromancy.Models;

namespace Necromancy.Data;

public class Items
{
    public static readonly Item Ectoplasm = new("Ectoplasm");
    
    public static readonly Item BasicInk = new("Basic ink");
    public static readonly Item RegularInk = new("Regular ink");
    public static readonly Item GreaterInk = new("Greater ink");
    public static readonly Item PowerfulInk = new("Powerful ink");
    public static readonly Item WeakNecroplasm = new("Weak necroplasm");
    public static readonly Item LesserNecroplasm = new("Lesser necroplasm");
    public static readonly Item GreaterNecroplasm = new("Greater necroplasm");
    public static readonly Item PowerfulNecroplasm = new("Powerful necroplasm");
    public static readonly Item VialOfWater = new("Vial of water");
    public static readonly Item Ashes = new("Ashes");
    
    public static readonly Item Soul = new("Soul");
    public static readonly Item DragonkinBones = new("Dragonkin Bones");
    public static readonly Item DinosaurBones = new("Dinosaur Bones");

    public static readonly Item LesserEnsouledBar = new("Lesser ensouled bar");
    public static readonly Item LesserEnsouledCloth = new("Lesser ensouled cloth");
    public static readonly Item LesserEnsouledThread = new("Lesser ensouled thread");
    public static readonly Item EnsouledBar = new("Ensouled bar");
    public static readonly Item EnsouledCloth = new("Ensouled cloth");
    public static readonly Item EnsouledThread = new("Ensouled thread");
    public static readonly Item GreaterEnsouledBar = new("Greater ensouled bar");
    public static readonly Item GreaterEnsouledCloth = new("Greater ensouled cloth");
    public static readonly Item GreaterEnsouledThread = new("Greater ensouled thread");
    
    public static readonly Item LesserUnensouledBar = new("Lesser unensouled bar");
    public static readonly Item UnensouledBar = new("Unensouled bar");
    public static readonly Item GreaterUnensouledBar = new("Greater unensouled bar");
    public static readonly Item SpiderSilkItem = new Item("Spider silk item");
    public static readonly Item MysticItem = new Item("Mystic item");
    public static readonly Item SubjugationItem = new Item("Subjugation item");
    public static readonly Item Thread = new Item("Thread");
    public static readonly Item SeasonalWool = new Item("Seasonal wool");
    public static readonly Item AlgarumThread = new Item("Algarum thread");
    
    public static IReadOnlyCollection<Item> All => typeof(Items).GetFields(BindingFlags.Public | BindingFlags.Static).Where(x => x.Name != nameof(All)).Select(x => (Item) x.GetValue(null)!).ToList();
}