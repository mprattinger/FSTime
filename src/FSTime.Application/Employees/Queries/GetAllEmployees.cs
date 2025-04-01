using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Employees;
using FSTime.Contracts.Users;
using MediatR;

namespace FSTime.Application.Employees.Queries;

public static class GetAllEmployees
{
    public record Query(Guid companyId) : IRequest<ErrorOr<List<EmployeeResponse>>>;

    internal sealed class Handler(IEmployeeRepository employeeRepository)
        : IRequestHandler<Query, ErrorOr<List<EmployeeResponse>>>
    {
        public async Task<ErrorOr<List<EmployeeResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await employeeRepository.GetEmployees(request.companyId);

                var ret = employees.Select(x =>
                {
                    var employeeResponse = new EmployeeResponse
                    {
                        Id = x.Id,
                        CompanyId = x.CompanyId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        MiddleName = x.MiddleName,
                        EmployeeCode = x.EmployeeCode,
                        EntryDate = x.EntryDate,
                        User = x.User != null
                            ? new UserResponse
                            {
                                Id = x.User.Id,
                                UserName = x.User.UserName,
                                Email = x.User.Email,
                                Verified = x.User.Verified
                            }
                            : null,
                        Supervisor = x.Supervisor != null
                            ? new EmployeeResponse
                            {
                                Id = x.Supervisor.Id,
                                CompanyId = x.Supervisor.CompanyId,
                                FirstName = x.Supervisor.FirstName,
                                LastName = x.Supervisor.LastName,
                                MiddleName = x.Supervisor.MiddleName,
                                EmployeeCode = x.Supervisor.EmployeeCode,
                                EntryDate = x.Supervisor.EntryDate,
                                IsHead = x.Supervisor.IsHead
                            }
                            : null,
                        IsHead = x.IsHead
                    };
                    return employeeResponse;
                }).ToList();

                return ret;
            }
            catch (Exception e)
            {
                return EmployeeErrors.Get_All_Employees(e.Message);
            }
        }
    }
}