using Ollivanders.Services.Controllers;

namespace Ollivanders;

public sealed class CoreFactory
{
    public WandCore CreateCore(ICoreDto coreDto)
    {
        var visitor = new CoreMappingVisitor();

        return coreDto.Accept(visitor);
    }

    private class CoreMappingVisitor : ICoreDtoVisitor<WandCore>
    {
        public WandCore Visit(UnicornHornCoreDto core)
        {
            return new UnicornWandCore(core.Age);
        }

        public WandCore Visit(DragonVeinCoreDto core)
        {
            return new DragonVeinWandCore(core.DragonSpecies);
        }

        public WandCore Visit(PhoenixFeatherCoreDto core)
        {
            return new PhoenixFeatherWandCore();
        }
    }
}