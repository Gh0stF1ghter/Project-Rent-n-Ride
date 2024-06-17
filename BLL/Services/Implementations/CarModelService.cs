using BLL.Models;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Mapster;
using BLL.Exceptions;
using BLL.Exceptions.ExceptionMessages;

namespace BLL.Services.Implementations;

public class CarModelService(ICarModelRepository repository) : ICarModelService
{
    public async Task<IEnumerable<CarModel>> GetRangeAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var modelNames = await repository.GetRangeAsync(page, pageSize, cancellationToken);

        var modelNameModels = modelNames.Adapt<IEnumerable<CarModel>>();

        return modelNameModels;
    }

    public async Task<CarModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var modelName = await repository.GetByIdAsync(id, cancellationToken);

        var modelNameModel = modelName.Adapt<CarModel>();

        return modelNameModel;
    }

    public async Task<CarModel> AddAsync(CarModel modelNameModel, CancellationToken cancellationToken)
    {
        var modelName = modelNameModel.Adapt<CarModelEntity>();

        await repository.AddAsync(modelName, cancellationToken);

        var newModelNameModel = modelName.Adapt<CarModel>();

        return newModelNameModel;
    }

    public async Task<CarModel> UpdateAsync(CarModel newModelNameModel, CancellationToken cancellationToken)
    {
        var modelName = await repository.GetByIdAsync(newModelNameModel.Id, cancellationToken)
            ?? throw new NotFoundException(ExceptionMessages.NotFound(nameof(CarModelEntity), newModelNameModel.Id));

        newModelNameModel.Adapt(modelName);

        await repository.UpdateAsync(modelName, cancellationToken);

        var modeNameToReturn = modelName.Adapt<CarModel>();

        return modeNameToReturn;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var modelName = await repository.GetByIdAsync(id, cancellationToken) 
            ?? throw new NotFoundException(ExceptionMessages.NotFound(nameof(CarModelEntity), id));

        await repository.RemoveAsync(modelName, cancellationToken);
    }
}