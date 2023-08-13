namespace Necromancy.Models;

public record ItemCount(Item Item, int Amount)
{
    public double Delta(Item item) => Item == item ? Amount : 0;
}