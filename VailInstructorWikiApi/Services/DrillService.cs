using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi.Services
{
    public class DrillService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DrillService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Drill> GetDrillById(int id)
        {
            var drill = await _dbContext.Drills
                .FirstOrDefaultAsync(d => d.Id == id);

            return drill;
        }

        public async Task<IEnumerable<Drill>> GetAllDrills()
        {
            var drills = await _dbContext.Drills
                .ToListAsync();
            return drills;
        }

        public async Task<Drill> CreateDrill(DrillDto drillDto)
        {
            var drill = _mapper.Map<Drill>(drillDto);
            _dbContext.Drills.Add(drill);
            await _dbContext.SaveChangesAsync();
            return drill;
        }

        public async Task<Drill> UpdateDrill(int id, DrillDto drillDto)
        {
            var drill = await _dbContext.Drills.FindAsync(id);
            if (drill == null)
            {
                return null; // or throw an exception, handle as per your requirement
            }

            _mapper.Map(drillDto, drill);

            await _dbContext.SaveChangesAsync();

            return drill;
        }

        public async Task<bool> DeleteDrill(int id)
        {
            var drill = await _dbContext.Drills.FindAsync(id);
            if (drill == null)
            {
                return false; // or throw an exception, handle as per your requirement
            }

            _dbContext.Drills.Remove(drill);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

