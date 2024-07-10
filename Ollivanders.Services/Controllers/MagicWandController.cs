using Microsoft.AspNetCore.Mvc;
using Ollivanders.Services.Controllers.Dtos;
using Ollivanders.Services.Database;

namespace Ollivanders.Services.Controllers;

public sealed class MagicWandController : ControllerBase
{
    private readonly MagicWandRepository _magicWandRepository;

    public MagicWandController(MagicWandRepository magicWandRepository)
    {
        _magicWandRepository = magicWandRepository;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMagicWandById(int id)
    {
        var magicWand = await _magicWandRepository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Magic wand with this id={id} not found.");

        return Ok(magicWand.ToMagicWandResponseDto());
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateMagicWand([FromBody] MagicWandRequestDto dto)
    {
        var magicWand = dto.ToMagicWand();
        var result = await _magicWandRepository.CreateAsync(magicWand);

        return Ok(result);
    }
    
    [HttpGet("price/{id}")]
    public async Task<IActionResult> GetMagicWandPriceById(int id)
    {
        var magicWand = await _magicWandRepository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Magic wand with this id={id} not found.");

        var result = magicWand.GetPrice();

        return Ok(result);
    }

    [HttpPut("sell/{magicWandId}")]
    public async Task<IActionResult> SellMagicWandById([FromQuery] int magicWandId)
    {
        var magicWand = await _magicWandRepository.TryGetByIdAsync(magicWandId);
        if (magicWand is null)
            return BadRequest($"Magic wand with this id={magicWand.Id} not found.");

        // some sell logic could be.
        await _magicWandRepository.RemoveAsync(magicWand);

        return Ok($"Magic wand was sold for {magicWand.GetPrice()} galleons.");
    }
}