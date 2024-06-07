﻿using DAL.Entities;
using System.Linq.Expressions;

namespace DAL.Repositories.Interfaces;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> GetRangeAsync(int page, int pageSize, CancellationToken cancellationToken);

    Task<Manufacturer?> GetByIdAsync(Guid id, bool trackingChanges, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(Expression<Func<Manufacturer, bool>> predicate, CancellationToken cancellationToken);

    Task AddAsync(Manufacturer manufacturer, CancellationToken cancellationToken);

    Task UpdateAsync(Manufacturer newManufacturer, CancellationToken cancellationToken);

    Task RemoveAsync(Manufacturer manufacturer, CancellationToken cancellationToken);
}