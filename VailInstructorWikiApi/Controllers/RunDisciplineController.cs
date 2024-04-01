using System;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Services;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.DTOs.RequestDtos;
using VailInstructorWikiApi.DTOs.ResponseDtos;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Controllers
{
    [ApiController]
    [Route("api/rundisciplines")]
    public class RunDisciplineController : ControllerBase
    {
        private readonly RunDisciplineService _runDisciplineService;
        private readonly DrillService _drillService;
        private readonly RunDisciplineDrillService _runDisciplineDrillService;

        public RunDisciplineController(RunDisciplineService runDisciplineService, DrillService drillService, RunDisciplineDrillService runDisciplineDrillService)
        {
            _runDisciplineService = runDisciplineService;
            _drillService = drillService;
            _runDisciplineDrillService = runDisciplineDrillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RunDiscipline>>> GetAllRunDisciplines()
        {
            var runDisciplines = await _runDisciplineService.GetAllRunDisciplines();
            return Ok(runDisciplines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RunDisciplineWithDrillsDto>> GetRunDisciplineById(int id)
        {
            var runDiscipline = await _runDisciplineService.GetRunDisciplineById(id);
            if (runDiscipline == null)
            {
                return NotFound();
            }

            var linkedDrills = await _runDisciplineDrillService.GetLinkedDrills(id);

            var runDisciplineDto = new RunDisciplineWithDrillsDto
            {
                RunDiscipline = runDiscipline,
                Drills = linkedDrills,

            };

            return Ok(runDisciplineDto);
        }

        [HttpGet("get-run-discipline-by-runId-discipline/{runId}/{discipline}")]
        public async Task<ActionResult<RunDisciplineWithDrillsDto>> GetRunDisciplineById(int runId, Discipline discpline)
        {
            var runDiscipline = await _runDisciplineService.GetRunDisciplineByRunAndDiscipline(runId, discpline);
            if (runDiscipline == null)
            {
                return NotFound();
            }

            var linkedDrills = await _runDisciplineDrillService.GetLinkedDrills(runDiscipline.Id);

            var runDisciplineDto = new RunDisciplineWithDrillsDto
            {
                RunDiscipline = runDiscipline,
                Drills = linkedDrills,

            };

            return Ok(runDisciplineDto);
        }

        [HttpPost]
        public async Task<ActionResult<RunDiscipline>> CreateRunDiscipline(RunDisciplineDto runDisciplineDto)
        {
            var runDiscipline = await _runDisciplineService.CreateRunDiscipline(runDisciplineDto);
            return CreatedAtAction(nameof(GetRunDisciplineById), new { id = runDiscipline.Id }, runDiscipline);
        }

        

        [HttpPut("{id}")]
        public async Task<ActionResult<RunDiscipline>> UpdateRunDiscipline(int id, RunDisciplineDto runDisciplineDto)
        {
            var updatedRunDiscipline = await _runDisciplineService.UpdateRunDiscipline(id, runDisciplineDto);
            if (updatedRunDiscipline == null)
            {
                return NotFound();
            }

            return Ok(updatedRunDiscipline);
        }


        [HttpPost("add-drill")]
        public async Task<IActionResult> AddDrillToRunDiscipline([FromBody] AddDrillToRunDisciplineDto request)
        {
            var runDiscipline = await _runDisciplineService.GetRunDisciplineById(request.RunDisciplineId);
            if (runDiscipline == null)
            {
                return NotFound("RunDiscipline not found");
            }

            var drill = await _drillService.GetDrillById(request.DrillId);
            if (drill == null)
            {
                return NotFound("Drill not found");
            }

            await _runDisciplineDrillService.CreateLink(request.RunDisciplineId, request.DrillId);
            return Ok();
        }

        [HttpDelete("remove-drill/{runDisciplineId}/{drillId}")]
        public async Task<IActionResult> RemoveDrillFromRunDiscipline(int runDisciplineId, int drillId)
        {
            await _runDisciplineDrillService.RemoveLink(runDisciplineId, drillId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRunDiscipline(int id)
        {
            var deleted = await _runDisciplineService.DeleteRunDiscipline(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}









