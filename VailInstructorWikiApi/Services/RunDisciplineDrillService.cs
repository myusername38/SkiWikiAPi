using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi.Services
{
    public class RunDisciplineDrillService
    {
        private readonly ApplicationDbContext dbContext;

        public RunDisciplineDrillService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateLink(int runDisciplineId, int drillId)
        {
            var runDisciplineDrill = new RunDisciplineDrill
            {
                RunDisciplineId = runDisciplineId,
                DrillId = drillId
            };

            dbContext.Add(runDisciplineDrill);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveLink(int runDisciplineId, int drillId)
        {
            var runDisciplineDrill = await dbContext.RunDisciplineDrills
                .SingleOrDefaultAsync(j => j.RunDisciplineId == runDisciplineId && j.DrillId == drillId);

            if (runDisciplineDrill != null)
            {
                dbContext.Remove(runDisciplineDrill);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Drill>> GetLinkedDrills(int runDisciplineId)
        {
            var runDiscipline = await dbContext.RunDisciplines.Include(rd => rd.RunDisciplineDrills).ThenInclude(rdd => rdd.Drill)
                .SingleOrDefaultAsync(rd => rd.Id == runDisciplineId);

            if (runDiscipline != null)
            {
                var linkedDrills = runDiscipline.RunDisciplineDrills.Select(rdd => rdd.Drill);
                return linkedDrills;
            }

            return null;
        }


    }
}

