using System;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.DTOs;

namespace VailInstructorWikiApi.Services
{
    public class AreaService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AreaService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Area> GetAreaById(int id)
        {
            var area = await _dbContext.Areas.FindAsync(id);
            if (area == null)
            {
                return null; // or throw an exception, handle as per your requirement
            }

            return area;
        }

        public async Task<IEnumerable<Area>> GetAllAreas()
        {
            var areas = await _dbContext.Areas.ToListAsync();
            return areas;
        }

        public async Task<Area> CreateArea(AreaDto AreaDto)
        {
            var area = _mapper.Map<Area>(AreaDto);

            _dbContext.Areas.Add(area);
            await _dbContext.SaveChangesAsync();

            return area;
        }

        public async Task<Area> UpdateArea(int id, AreaDto AreaDto)
        {
            var area = await _dbContext.Areas.FindAsync(id);
            if (area == null)
            {
                return null; // or throw an exception, handle as per your requirement
            }

            _mapper.Map(AreaDto, area);
            await _dbContext.SaveChangesAsync();

            return area;
        }

        public async Task<bool> DeleteArea(int id)
        {
            var area = await _dbContext.Areas.FindAsync(id);
            if (area == null)
            {
                return false; // or throw an exception, handle as per your requirement
            }
            _dbContext.Areas.Remove(area);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}