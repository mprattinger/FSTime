using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface IWorkScheduleRepository
{
    public Task<List<WorkSchedule>> GetAll(Guid companyId);
    public Task<WorkSchedule?> Get(Guid id);

    public Task<WorkSchedule> Create(WorkSchedule workSchedule);
    public Task<WorkSchedule> Update(WorkSchedule workSchedule);
}