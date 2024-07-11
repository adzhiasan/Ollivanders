using System.ComponentModel.DataAnnotations;

namespace Ollivanders.Services.Controllers;

public static class RepairHelper
{
    public static void Repair(MagicWand magicWandToRepair, WandPart wandPartToRepair)
    {
        var partToRepair = DeterminePartToRepair(magicWandToRepair, wandPartToRepair);
        if (partToRepair.IsRepaired)
            throw new ValidationException("This part of magic wand was repaired in the past.");
        
        // some repair logic
        partToRepair.IsRepaired = true;
    }
    
    private static ElementRepairInfo DeterminePartToRepair(MagicWand magicWandToRepair, WandPart wandPartToRepair)
    {
        return wandPartToRepair switch
        {
            WandPart.Wood => magicWandToRepair.Wood,
            WandPart.Core => magicWandToRepair.Core,
            _ => throw new ArgumentOutOfRangeException(nameof(wandPartToRepair), wandPartToRepair, null)
        };
    }
}