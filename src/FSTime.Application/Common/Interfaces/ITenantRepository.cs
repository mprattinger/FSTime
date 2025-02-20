﻿using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ITenantRepository
{
    Task<Tenant> CreateTenant(Tenant tenant, TenantRole role);
}
