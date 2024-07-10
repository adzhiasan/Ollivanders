using Microsoft.AspNetCore.Mvc;
using Ollivanders.Repair;

namespace Ollivanders.Services.Controllers;

public class RepairRequestsController : ControllerBase
{
    private readonly RepairRequestRepository _repairRequestRepository;

    public RepairRequestsController(RepairRequestRepository repairRequestRepository)
    {
        _repairRequestRepository = repairRequestRepository;
    }

    [HttpPost("repair")]
    public async Task<IActionResult> RepairMagicWandPartAsync([FromBody] RepairRequestDto repairRequestDto)
    {
        var wasPartRepaired = (await _repairRequestRepository
                .TryGetByMagicWandIdAsync(repairRequestDto.MagicWandId))
            .Any(r => r.Part == repairRequestDto.Part);
        if (wasPartRepaired)
            return BadRequest("This part of magic wand was repaired in the past.");

        // some repair logic
        await _repairRequestRepository.CreateAsync(repairRequestDto.ToRepairRequest());

        return Ok();
    }
}

public class RepairRequestDto
{
    public int MagicWandId { get; set; }
    public WandPart Part { get; set; }

    public RepairRequest ToRepairRequest()
    {
        return new RepairRequest
        {
            MagicWandId = MagicWandId,
            Part = Part
        };
    }
}