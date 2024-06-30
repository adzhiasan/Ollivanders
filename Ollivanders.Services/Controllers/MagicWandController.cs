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

        return Ok(magicWand.ToMagicWandResponseDto());
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateMagicWand([FromBody] MagicWandRequestDto dto)
    {
        var magicWand = await _repository.CreateAsync(dto.ToMagicWand());

        return Ok(magicWand.ToMagicWandResponseDto());
    }
}