namespace Ollivanders.Services.Controllers;

public sealed class MagicWandCreationDto
{
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public string Wood { get; init; }
    public ICoreDto Core { get; init; }

    public MagicWand ToMagicWand()
    {
        var factory = new CoreFactory();
        var core = factory.CreateCore(Core);    
        
        return new MagicWand(Length, FlexibilityFactor, Wood,  core);
    }
}