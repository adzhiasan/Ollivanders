using System.Text.Json.Serialization;

namespace Ollivanders.Services.Controllers;

[JsonDerivedType(typeof(UnicornHornCoreDto), UnicornHornCoreDto.Type)]
[JsonDerivedType(typeof(DragonVeinCoreDto), DragonVeinCoreDto.Type)]
[JsonDerivedType(typeof(PhoenixFeatherCoreDto), PhoenixFeatherCoreDto.Type)]
public interface ICoreDto
{
    TResult Accept<TResult>(ICoreDtoVisitor<TResult> v);
}

public record UnicornHornCoreDto : ICoreDto
{
    public const string Type = "UnicornHorn";

    public int Age { get; set; }

    public TResult Accept<TResult>(ICoreDtoVisitor<TResult> v) => v.Visit(this);
}

public record DragonVeinCoreDto : ICoreDto
{
    public const string Type = "DragonVein";

    public DragonSpecies DragonSpecies { get; set; }

    public TResult Accept<TResult>(ICoreDtoVisitor<TResult> v) => v.Visit(this);
}

public record PhoenixFeatherCoreDto : ICoreDto
{
    public const string Type = "PhoenixFeather";

    public TResult Accept<TResult>(ICoreDtoVisitor<TResult> v) => v.Visit(this);
}