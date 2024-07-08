using Ollivanders.Services.Controllers;

namespace Ollivanders.Services;

internal static class MagicWandRepairInfoExtensions
{
    public static PartRepairInfo GetPartToRepair(this MagicWandRepairInfo repairInfo, WandPart partToRepair)
    {
        return partToRepair switch
        {
            WandPart.Wood => repairInfo.Wood,
            WandPart.Core => repairInfo.Core,
            _ => throw new ArgumentOutOfRangeException(nameof(partToRepair), partToRepair, null)
        };
    }
}