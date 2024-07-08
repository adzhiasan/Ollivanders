namespace Ollivanders;

public class MagicWandRepairInfo
{
    public int MagicWandId { get; set; }
    public CoreRepairInfo Core { get; set; }
    public WoodRepairInfo Wood { get; set; }
}

public abstract class PartRepairInfo
{
    protected abstract int Limit { get; }

    public uint RepairsCount { get; set; }

    private bool CanBeRepaired() => RepairsCount < Limit;

    public virtual bool TryRepair()
    {
        if (CanBeRepaired())
        {
            RepairsCount++;
            return true;
        }

        return false;
    }
}

public class CoreRepairInfo : PartRepairInfo
{
    protected override int Limit => 1;

    public override bool TryRepair()
    {
        Console.WriteLine("Core repaired :neko-cat-celebrating:");

        return base.TryRepair();
    }
}

public class WoodRepairInfo : PartRepairInfo
{
    protected override int Limit => 1;

    public override bool TryRepair()
    {
        Console.WriteLine("Wood repaired :tada:");

        return base.TryRepair();
    }
}