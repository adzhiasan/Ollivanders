using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Ollivanders.Repair;

namespace Ollivanders.Services.Controllers;

public class MagicWandController : ControllerBase
{
    private readonly MagicWandRepository _magicWandRepository;

    public MagicWandToRepairController(MagicWandRepository magicWandRepository)
    {
        _magicWandRepository = magicWandRepository;
    }

    [HttpPost("repair")]
    public async Task<IActionResult> RepairMagicWandPartAsync([FromBody] MagicWandDto repairRequestDto)
    {
        var magicWandToRepair = await _magicWandRepository.TryGetByIdAsync(repairRequestDto.Id);
        try
        {
            RepairHelper.Repair(magicWandToRepair, repairRequestDto.Part);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }

        await _magicWandRepository.SaveAsync();

        return Ok();
    }
}


public class MagicWandDto
{
    public int Id { get; set; }
    public WandPart Part { get; set; }
}