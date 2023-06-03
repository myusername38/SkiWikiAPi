using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi.Services
{
    public class LevelService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public LevelService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Level>> GetAllLevelsAsync()
        {
            return await _dbContext.Levels.ToListAsync();
        }

        public async Task<Level> GetLevelByIdAsync(int id)
        {
            return await _dbContext.Levels.FindAsync(id);
        }

        public async Task<Level> CreateLevelAsync(LevelDto levelDto)
        {
            var level = _mapper.Map<Level>(levelDto);
            _dbContext.Levels.Add(level);
            await _dbContext.SaveChangesAsync();
            return level;
        }

        public async Task<Level> UpdateLevelAsync(int id, LevelDto levelDto)
        {
            var existingLevel = await _dbContext.Levels.FindAsync(id);
            if (existingLevel == null)
            {
                // Level not found, handle accordingly (throw exception or return null)
                return null;
            }

            _mapper.Map(levelDto, existingLevel);
            await _dbContext.SaveChangesAsync();
            return existingLevel;
        }

        public async Task DeleteLevelAsync(int id)
        {
            var level = await _dbContext.Levels.FindAsync(id);
            if (level != null)
            {
                _dbContext.Levels.Remove(level);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

