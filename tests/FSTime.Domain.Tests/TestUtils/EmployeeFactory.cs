using FSTime.Domain.EmployeeAggregate;

namespace FSTime.Domain.Tests.TestUtils;

public class EmployeeFactory
{
  public static Employee CreateActiveEmployee()
  {
    var e = new Employee(new DateOnly(2021, 1, 1), 25);
    e.AddWorkSchedule(Guid.NewGuid());
    return e;
  }

  public static Employee CreateInactiveEmployee()
  {
    var e = new Employee(new DateOnly(2021, 1, 1), 25);
    return e;
  }
}