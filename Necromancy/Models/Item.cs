namespace Necromancy.Models;

public record Item(string Name)
{
    public ItemCount Count(int count) => new(this, count);
}