using BLL.Models;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Mapster;

namespace BLL.Services.Implementations;

public class VehicleService(IVehicleRepository repository) : IVehicleService
{
    public async Task<IEnumerable<VehicleModel>> GetRangeAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var vehicles = await repository.GetRangeAsync(page, pageSize, cancellationToken);

        var vehicleModels = vehicles.Adapt<IEnumerable<VehicleModel>>();

        return vehicleModels;
    }

    public async Task<VehicleModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var vehicle = await repository.GetByIdAsync(id, false, cancellationToken);

        var vehicleModel = vehicle.Adapt<VehicleModel>();

        return vehicleModel;
    }

    public async Task<VehicleModel> AddAsync(VehicleModel vehicleModel, CancellationToken cancellationToken)
    {
        var vehicle = vehicleModel.Adapt<Vehicle>();

        await repository.AddAsync(vehicle, cancellationToken);

        var newVehicleModel = vehicle.Adapt<VehicleModel>();

        return newVehicleModel;
    }

    public async Task<VehicleModel> UpdateAsync(VehicleModel newVehicleModel, CancellationToken cancellationToken)
    {
        var vehicle = await repository.GetByIdAsync(newVehicleModel.Id, true, cancellationToken);

        newVehicleModel.Adapt(vehicle);

        await repository.UpdateAsync(vehicle, cancellationToken);

        var vehicleToUpdate = VehicleMapper.Map(vehicle);

        return vehicleToUpdate;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var vehicle = await repository.GetByIdAsync(id, true, cancellationToken);

        await repository.RemoveAsync(vehicle, cancellationToken);
    }
}