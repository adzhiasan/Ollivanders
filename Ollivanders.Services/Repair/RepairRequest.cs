using Microsoft.AspNetCore.Mvc;

namespace Ollivanders.Repair;

public class RepairRequest
{
    // составной ключ по идентификатору палочки и парту
    public int MagicWandId { get; set; }
    public WandPart Part { get; set; }
}

public enum WandPart
{
    Wood,
    Core
}

