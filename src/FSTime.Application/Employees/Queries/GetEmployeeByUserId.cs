﻿using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Application.Workschedules;
using FSTime.Contracts.Employees;
using FSTime.Contracts.Users;

namespace FSTime.Application.Employees.Queries;

public static class GetEmployeeByUserId
{
    public record Query(Guid UserId) : IQuery<EmployeeResponse>;

    internal sealed class Handler(IEmployeeRepository employeeRepository) : IQueryHandler<Query, EmployeeResponse>
    {
        public async Task<ErrorOr<EmployeeResponse>> Handle(Query request, CancellationToken cancellationToken = default)
        {
            var employee = await employeeRepository.GetEmployeeByUserId(request.UserId);

            if (employee == null) return EmployeeErrors.Get_EmployeeByUser_NotFound(request.UserId);

            var employeeResponse = new EmployeeResponse
            {
                Id = employee.Id,
                CompanyId = employee.CompanyId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                EmployeeCode = employee.EmployeeCode,
                EntryDate = employee.EntryDate,
                User = employee.User != null
                    ? new UserResponse
                    {
                        Id = employee.User.Id,
                        UserName = employee.User.UserName,
                        Email = employee.User.Email,
                        Verified = employee.User.Verified
                    }
                    : null,
                Supervisor = employee.Supervisor != null
                    ? new EmployeeResponse
                    {
                        Id = employee.Supervisor.Id,
                        CompanyId = employee.Supervisor.CompanyId,
                        FirstName = employee.Supervisor.FirstName,
                        LastName = employee.Supervisor.LastName,
                        MiddleName = employee.Supervisor.MiddleName,
                        EmployeeCode = employee.Supervisor.EmployeeCode,
                        EntryDate = employee.Supervisor.EntryDate,
                        IsHead = employee.Supervisor.IsHead
                    }
                    : null,
                IsHead = employee.IsHead,
                Workschedules = employee.Workschedules.Select(x => x.ToWorkscheduleResponse()).ToList()
            };

            return employeeResponse;
        }
    }
}
