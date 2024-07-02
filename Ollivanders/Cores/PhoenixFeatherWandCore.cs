namespace Ollivanders;

public record PhoenixFeatherWandCore : WandCore
{
    public override string Name => CoreNames.PhoenixFeather;

    public override Price GetPrice() => new(4d);
}