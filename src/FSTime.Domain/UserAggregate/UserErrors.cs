using System;
using ErrorOr;

namespace FSTime.Domain.UserAggregate;

public static class UserErrors
{
  public static Error Already_has_role(string role) => Error.Conflict("USER.ALREADY_HAS_ROLE", $"Role {role} already exists");

  public static Error Invalid_role(string role) => Error.Conflict("USER.INVALID_ROLE", $"Role {role} is invalid");
}
