using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;

namespace FSTime.Application.Employees.Queries;

public static class GetAllEmployees
{
    public record Query(Guid companyId) : IQuery<List<EmployeeResponse>>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : IQueryHandler<Query, List<EmployeeResponse>>
    {
        public async Task<ErrorOr<List<EmployeeResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var employees = await employeeRepository.GetEmployees(request.companyId);

            // var ret = employees.Select(x =>
            // {
            //     var employeeResponse = new EmployeeResponse
            //     {
            //         Id = x.Id,
            //         CompanyId = x.CompanyId,
            //         FirstName = x.FirstName,
            //         LastName = x.LastName,
            //         MiddleName = x.MiddleName,
            //         EmployeeCode = x.EmployeeCode,
            //         EntryDate = x.EntryDate,
            //         User = x.User != null
            //             ? new UserResponse
            //             {
            //                 Id = x.User.Id,
            //                 UserName = x.User.UserName,
            //                 Email = x.User.Email,
            //                 Verified = x.User.Verified
            //             }
            //             : null,
            //         Supervisor = x.Supervisor != null
            //             ? new EmployeeResponse
            //             {
            //                 Id = x.Supervisor.Id,
            //                 CompanyId = x.Supervisor.CompanyId,
            //                 FirstName = x.Supervisor.FirstName,
            //                 LastName = x.Supervisor.LastName,
            //                 MiddleName = x.Supervisor.MiddleName,
            //                 EmployeeCode = x.Supervisor.EmployeeCode,
            //                 EntryDate = x.Supervisor.EntryDate,
            //                 IsHead = x.Supervisor.IsHead
            //             }
            //             : null,
            //         IsHead = x.IsHead
            //     };

            var employeesList = employees.Select(x => x.ToEmployeeResponse()).ToList();

            return employeesList;
            // }).ToList();

            // return ret;
        }
    }
}