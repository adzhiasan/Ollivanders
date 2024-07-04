namespace Ollivanders;

public class MageMagicWand(int mageId, int magicWandId)
{
    public int MageId { get; set; } = mageId;
    public Mage Mage { get; set; } = null!;

    public int MagicWandId { get; set; } = magicWandId;
    public MagicWand MagicWand { get; set; } = null!;
}