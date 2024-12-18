﻿using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence.Location;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.Location
{
    public class AddressRepository : Repository<Domain.Entities.Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
