using System.ComponentModel.DataAnnotations.Schema;

namespace Ollivanders;

public class MagicWand
{
    public MagicWand(
        int length,
        double flexibilityFactor,
        WandWood wood,
        WandCore core,
        List<int> previousOwners,
        Price? basePrice)
    {
        if (length is < 18 or > 50)
            throw new ArgumentException(nameof(length));
        if (flexibilityFactor is < 0.01 or > 0.2)
            throw new ArgumentException(nameof(flexibilityFactor));
        if (previousOwners.Count == 0 && core is null)
            throw new ArgumentException("Not collection wands must have core");
        if (previousOwners.Count == 0 && basePrice is null)
            throw new ArgumentException("Collection wand must have base price");

        Length = length;
        FlexibilityFactor = flexibilityFactor;
        Wood = wood;
        Core = core;
        MageIds = previousOwners;
        BasePrice = basePrice;
    }

    public int Id { get; set; }
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public WandWood Wood { get; init; }
    public WandCore? Core { get; init; }
    public List<Mage> Mages { get; set; }
    public List<int> MageIds { get; set; }
    public bool IsSold { get; set; }
    public Price? BasePrice { get; set; }

    public double GetPrice()
    {
        return BasePrice?.Value ?? (Core!.GetPrice() + Wood.GetWoodPrice()).Value;
    }
}




// 1. Для кого идейно эта программа: это онлайн-магазин, или же приложение для работника магазина?
// 2. Нужно ли хранить информацию о проданных палочках? Или удалять её?
// 3. Мы хотим иметь возможность изменять базовую цену компонентов и увеличивающие коэффициенты?
// 4. Цена палочки фиксирована, то есть высчитана на момент поставки в магазин, или же актуальна в любой момент времени?
// 5. В каком виде мы получаем информацию о том, совершеннолетний ли покупатель: мы ему на слово верим (то число годиков, которые ему есть) или же у нас есть в базе данных информация о наших покупателях, куда мы можем заглянуть и получить ответ?
// 6. Как должны выглядеть бывшие владельцы палочки? Это текстовое описание, или же список бывших владельцов, которые покупали палочки в магазине Олливандера?