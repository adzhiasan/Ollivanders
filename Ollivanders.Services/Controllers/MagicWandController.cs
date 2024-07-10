using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Ollivanders.Services.Database;

namespace Ollivanders.Services.Controllers;

public sealed class MagicWandController : ControllerBase
{
    private readonly MagicWandRepository _magicWandRepository;
    private readonly MagesRepository _magesRepository;
    private readonly MageMagicWandRepository _mageMagicWandRepository;

    public MagicWandController(MagicWandRepository repository, MagesRepository magesRepository,
        MageMagicWandRepository mageMagicWandRepository)
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
            throw new ValidationException($"Magic wand with this id={magicWand.Id} not found.");

        var mage = await _magesRepository.TryGetByIdAsync(mageId);
        if (mage is null)
            throw new ValidationException($"Mage with this id={magicWand.Id} not found.");

        MagicWand soldMagicWand;
        try
        {
            soldMagicWand = SellHelper.SellMagicWand(magicWand, mage);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }

        await _magicWandRepository.UpdateAsync(soldMagicWand);

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