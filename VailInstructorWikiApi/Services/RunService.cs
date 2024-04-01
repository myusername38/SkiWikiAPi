using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Enums;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi.Services
{
    public class RunService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RunService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Run> GetRunById(int id)
        {
            var run = await _dbContext.Runs.Include(r => r.RunDisciplines).FirstOrDefaultAsync(r => r.Id == id);
            return run;
        }

        public async Task<IEnumerable<Run>> GetAllRuns()
        {
            var runs = await _dbContext.Runs.Include(r => r.RunDisciplines).ToListAsync();
            return runs;
        }

        public async Task<IEnumerable<Run>> GetRunsByAreaId(int areaId)
        {
            var runs = await _dbContext.Runs.Where(r => r.AreaId == areaId).ToListAsync();
            return runs;
        }

        public async Task<Run> CreateRun(RunDto runDto)
        {
            var run = _mapper.Map<Run>(runDto);
            _dbContext.Runs.Add(run);
            await _dbContext.SaveChangesAsync();
            return run;
        }

        public async Task<Run> UpdateRun(int id, RunDto updatedRun)
        {
            var run = await _dbContext.Runs.FindAsync(id);
            if (run == null)
            {
                return null; // or throw an exception, handle as per your requirement
            }

            _mapper.Map(updatedRun, run);

            await _dbContext.SaveChangesAsync();

            return run;
        }

        public async Task<bool> DeleteRun(int id)
        {
            var run = await _dbContext.Runs.FindAsync(id);
            if (run == null)
            {
                return false; // or throw an exception, handle as per your requirement
            }

            _dbContext.Runs.Remove(run);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

