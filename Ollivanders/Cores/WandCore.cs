namespace Ollivanders;

public abstract record WandCore
{
    public abstract string Name { get; }

    public abstract Price GetPrice();
}