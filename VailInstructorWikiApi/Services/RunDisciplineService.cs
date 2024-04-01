using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Enums;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi.Services
{
	public class RunDisciplineService
	{
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RunDisciplineService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RunDiscipline> GetRunDisciplineById(int id)
        {
            var runDiscipline = await _dbContext.RunDisciplines
                .Include(rd => rd.Level)
                .FirstOrDefaultAsync(rd => rd.Id == id);
            return runDiscipline;
        }

        public async Task<IEnumerable<RunDiscipline>> GetAllRunDisciplines()
        {
            var runDisciplines = await _dbContext.RunDisciplines
                .Include(rd => rd.Level)
                .ToListAsync();
            return runDisciplines;
        }

        public async Task<RunDiscipline> GetRunDisciplineByRunAndDiscipline(int runId, Discipline discpline) {
            var runDiscipline = await _dbContext.RunDisciplines
                .Where(rd => rd.Discipline == discpline)
                .Include(rd => rd.Level)
                .FirstOrDefaultAsync();
            return runDiscipline;
               
        }

        public async Task<RunDiscipline> CreateRunDiscipline(RunDisciplineDto runDisciplineDto)
        {
            var runDiscipline = _mapper.Map<RunDiscipline>(runDisciplineDto);

            _dbContext.RunDisciplines.Add(runDiscipline);
            await _dbContext.SaveChangesAsync();

            return runDiscipline;
        }

        public async Task<RunDiscipline> UpdateRunDiscipline(int id, RunDisciplineDto runDisciplineDto)
        {
            var runDiscipline = await _dbContext.RunDisciplines.FindAsync(id);
            if (runDiscipline == null)
            {
                return null; // or throw an exception, handle as per your requirement
            }

            _mapper.Map(runDisciplineDto, runDiscipline);

            await _dbContext.SaveChangesAsync();

            return runDiscipline;
        }

        public async Task<bool> DeleteRunDiscipline(int id)
        {
            var runDiscipline = await _dbContext.RunDisciplines.FindAsync(id);
            if (runDiscipline == null)
            {
                return false; // or throw an exception, handle as per your requirement
            }

            _dbContext.RunDisciplines.Remove(runDiscipline);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

