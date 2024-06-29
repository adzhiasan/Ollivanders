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

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetMagicWandById(int id)
    {
        var magicWand = await _repository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Magicwand with this id={id} not found.");

        return Ok(magicWand);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateMagicWand([FromBody] MagicWandCreationDto dto)
    {
        var magicWand = dto.ToMagicWand();
        await _repository.CreateAsync(magicWand);
    
        return Ok(magicWand.Id);
    }
}