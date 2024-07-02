namespace Ollivanders.Services.Controllers;

public interface ICoreDtoVisitor<out TResult>
{
    TResult Visit(UnicornHornCoreDto core);
    TResult Visit(DragonVeinCoreDto core);
    TResult Visit(PhoenixFeatherCoreDto core);
}