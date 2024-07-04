using Microsoft.AspNetCore.Mvc;
using Ollivanders.Services.Database;

namespace Ollivanders.Services.Controllers;

public sealed class MagicWandController : ControllerBase
{
    private readonly MagicWandRepository _magicWandRepository;
    private readonly MagesRepository _magesRepository;
    private readonly MageMagicWandRepository _mageMagicWandRepository;

    public MagicWandController(MagicWandRepository repository, MagesRepository magesRepository, MageMagicWandRepository mageMagicWandRepository)
    {
        _magicWandRepository = repository;
        _magesRepository = magesRepository;
        _mageMagicWandRepository = mageMagicWandRepository;
    }
    
    [HttpPut("sell")]
    public async Task<IActionResult> SellMagicWandById([FromQuery] int magicWandId, [FromQuery] int mageId)
    {
        var magicWand = await _magicWandRepository.TryGetByIdAsync(magicWandId);
        if (magicWand is null)
            return BadRequest($"Mage with this id={magicWandId} not found.");
        if (magicWand.IsSold)
            return BadRequest($"Magic wand with this id={magicWandId} already sold.");
        var mage = await _magesRepository.TryGetByIdAsync(mageId);
        var mages = _mageMagicWandRepository.GetMageIdsByMagicWandId(magicWandId);
        if (mages.Any() && mage.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18))
            return BadRequest($"Younger than 18 years.");
        
        magicWand.IsSold = true;
        await _mageMagicWandRepository.UpdateAsync(new MageMagicWand(mageId, magicWandId));

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMagicWandById(int id)
    {
        var magicWand = await _magicWandRepository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Magic wand with this id={id} not found.");

        return Ok(magicWand.ToMagicWandResponseDto());
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


    [HttpPost("create")]
    public async Task<IActionResult> CreateMagicWand([FromBody] MagicWandRequestDto dto)
    {
        var magicWand = dto.ToMagicWand();
        var result = await _magicWandRepository.CreateAsync(magicWand);
        await _mageMagicWandRepository.CreateRangeAsync(
            dto.PreviousOwners.Select(po => new MageMagicWand(po, result.Id)).ToList());

        return Ok(result);
    }
}