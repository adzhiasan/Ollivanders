namespace Ollivanders.Services.Controllers;

public class MagicWand
{
    public int Id { get; set; }
    public ElementRepairInfo Core { get; set; }
    public ElementRepairInfo Wood { get; set; }
}

public class ElementRepairInfo
{
    public bool IsRepaired { get; set; }
}

public enum WandPart
{
    Wood,
    Core
}