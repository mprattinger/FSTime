using FSTime.Application.Common.Interfaces;
using FSTime.Domain.WorkScheduleAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class WorkScheduleRepository(FSTimeDbContext context) : IWorkScheduleRepository
{
    public Task<List<WorkSchedule>> GetAll(Guid companyId)
    {
        return context.WorkSchedules.Where(x => x.CompanyId == companyId).ToListAsync();
    }

    public async Task<WorkSchedule?> Get(Guid id)
    {
        return await context.WorkSchedules.FindAsync(id);
    }

    public async Task<WorkSchedule> Create(WorkSchedule workSchedule)
    {
        await context.WorkSchedules.AddAsync(workSchedule);
        await context.SaveChangesAsync();
        return workSchedule;
    }

    public async Task<WorkSchedule> Update(WorkSchedule workSchedule)
    {
        context.WorkSchedules.Update(workSchedule);
        await context.SaveChangesAsync();
        return workSchedule;
    }
}