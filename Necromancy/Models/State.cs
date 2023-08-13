namespace Necromancy.Models;

public record State(IReadOnlyCollection<ItemCount> Items)
{
    public IReadOnlyDictionary<Item, int> ToDictionary() => Items
        .GroupBy(x => x.Item, x => x.Amount, (item, counts) => item.Count(counts.Sum()))
        .ToDictionary(x => x.Item, x => x.Amount);
}