using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi.Services
{
    public class SkillService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SkillService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Skill> CreateSkill(SkillDto skillDto)
        {
            var skill = _mapper.Map<Skill>(skillDto);

            _dbContext.Skills.Add(skill);
            await _dbContext.SaveChangesAsync();

            return skill;
        }

        public async Task<Skill> GetSkill(int id)
        {
            return await _dbContext.Skills.FindAsync(id);
        }

        public async Task<List<Skill>> GetAllSkills()
        {
            return await _dbContext.Skills.ToListAsync();
        }

        public async Task<Skill> UpdateSkill(int id, SkillDto skillDto)
        {
            var skill = await _dbContext.Skills.FindAsync(id);

            if (skill == null)
                return null;

            _mapper.Map(skillDto, skill);

            await _dbContext.SaveChangesAsync();

            return skill;
        }

        public async Task<bool> DeleteSkill(int id)
        {
            var skill = await _dbContext.Skills.FindAsync(id);

            if (skill == null)
                return false;

            _dbContext.Skills.Remove(skill);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

