using System;
using FSTime.Domain.Tests.TestUtils;

namespace FSTime.Domain.Tests;

public class EmployeeTests
{
  [Fact]
  public void IsActive_WhenWorkScheduleIdIsEmpty_ReturnsFalse()
  {
    // Arrange
    var employee = EmployeeFactory.CreateInactiveEmployee();

    // Act
    var result = employee.IsActive();

    // Assert
    Assert.False(result);
  }

  [Fact]
  public void IsActive_WhenWorkScheduleIdIsNoEmpty_ReturnsTrue()
  {
    // Arrange
    var employee = EmployeeFactory.CreateActiveEmployee();

    // Act
    var result = employee.IsActive();

    // Assert
    Assert.True(result);
  }
}