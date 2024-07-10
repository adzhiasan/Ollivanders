using Microsoft.AspNetCore.Mvc;
using Ollivanders.Services.Controllers.Dtos;
using Ollivanders.Services.Database;

namespace Ollivanders.Services.Controllers;

public sealed class CollectionMagicWandController : ControllerBase
{
    private readonly CollectionMagicWandRepository _collectionMagicWandRepository;
    private readonly MagesRepository _magesRepository;

    public CollectionMagicWandController(
        CollectionMagicWandRepository collectionMagicWandRepository,
        MagesRepository magesRepository)
    {
        _collectionMagicWandRepository = collectionMagicWandRepository;
        _magesRepository = magesRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMagicWandById(int id)
    {
        var magicWand = await _collectionMagicWandRepository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Collection magic wand with this id={id} not found.");

        return Ok(magicWand.ToMagicWandResponseDto());
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateMagicWand([FromBody] CollectionMagicWandRequestDto dto)
    {
        var magicWand = dto.ToCollectionMagicWand();
        var result = await _collectionMagicWandRepository.CreateAsync(magicWand);

        return Ok(result);
    }

    [HttpGet("price/{id}")]
    public async Task<IActionResult> GetMagicWandPriceById(int id)
    {
        var magicWand = await _collectionMagicWandRepository.TryGetByIdAsync(id);
        if (magicWand is null)
            return BadRequest($"Collection magic wand with this id={id} not found.");

        var result = magicWand.BasePrice.Value;

        return Ok(result);
    }

    [HttpPut("sell-collection")]
    public async Task<IActionResult> SellCollectionMagicWandById([FromQuery] int magicWandId, [FromQuery] int mageId)
    {
        var magicWand = await _collectionMagicWandRepository.TryGetByIdAsync(magicWandId);
        if (magicWand is null)
            return BadRequest($"Collection magic wand with this id={magicWandId} not found.");

        var mage = await _magesRepository.TryGetByIdAsync(mageId);
        if (mage is null)
            return BadRequest($"Mage with this id={mageId} not found.");

        if (IsAdult(mage.DateOfBirth))
            return BadRequest("Younger than 18 years.");

        // some sell logic could be.
        await _collectionMagicWandRepository.RemoveAsync(magicWand);
        
        return Ok($"Collection magic wand was sold for {magicWand.BasePrice.Value} galleons.");

        
        bool IsAdult(DateOnly dateOfBirth) => dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18);
    }
}