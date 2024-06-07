﻿using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Mapster;

namespace BLL.Services.Implementations;

public class VehicleClientHistoryService(IVehicleClientHistoryRepository repository) : IVehicleClientHistoryService
{
    public async Task<IEnumerable<VchModel>> GetRangeAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var vehicleClientHistories = await repository.GetRangeAsync(page, pageSize, cancellationToken);

        var vehicleClientHistoryModels = vehicleClientHistories.Adapt<IEnumerable<VchModel>>();

        return vehicleClientHistoryModels;
    }

    public async Task<VchModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var vch = await repository.GetByIdAsync(id, cancellationToken);

        var vchModel = vch.Adapt<VchModel>();

        return vchModel;
    }

    public async Task<VchModel> AddAsync(VchModel vchModel, CancellationToken cancellationToken)
    {
        var vch = vchModel.Adapt<VehicleClientHistory>();

        await repository.AddAsync(vch, cancellationToken);

        var newVchModel = vch.Adapt<VchModel>();

        return newVchModel;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var modelName = await repository.GetByIdAsync(id, cancellationToken);

        repository.RemoveAsync(modelName, cancellationToken);
    }
}