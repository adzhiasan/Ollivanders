using Microsoft.AspNetCore.Mvc;
using Ollivanders.Services.Database;

namespace Ollivanders.Services.Controllers;

public sealed class MagicWandController : ControllerBase
{
    private readonly MagicWandRepository _repository;

    public MagicWandController(MagicWandRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMagicWandById(int id)
    {
        var magicWand = await _repository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Magic wand with this id={id} not found.");

        return Ok(magicWand);
    }

    [HttpGet("price/{id}")]
    public async Task<IActionResult> GetMagicWandPriceById(int id)
    {
        var magicWand = await _repository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Magic wand with this id={id} not found.");

        var result = magicWand.GetPrice();

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateMagicWand([FromBody] MagicWandCreationDto dto)
    {
        var magicWand = dto.ToMagicWand();
        var result = await _repository.CreateAsync(magicWand);

        return Ok(result);
    }
}