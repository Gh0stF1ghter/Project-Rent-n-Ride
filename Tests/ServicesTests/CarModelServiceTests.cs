﻿using BLL.Exceptions;
using BLL.Models;
using BLL.Services.Implementations;
using DAL.Entities;
using FluentAssertions;
using Mapster;
using Newtonsoft.Json;
using System.Text;
using Tests.DataGeneration;
using Tests.Mocks;

namespace Tests.ServicesTests;

public class CarModelServiceTests
{
    private readonly DistributedCacheMock _distributedCacheMock = new();
    private readonly CarModelRepositoryMock _repositoryMock = new();

    private readonly List<CarModelEntity> _models = DataGenerator.AddModelData(5);

    public CarModelServiceTests()
    {
        _repositoryMock.GetRange(_models);
        _repositoryMock.GetById(_models[0]);
        _repositoryMock.IsExists(true);
    }

    [Fact]
    public async Task GetRangeAsync__ReturnsCarModelList()
    {
        //Arrange
        var correctModels = _models.Adapt<IEnumerable<CarModel>>();

        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.GetRangeAsync(1, 1, default);

        //Assert
        response.Should().BeEquivalentTo(correctModels);
    }

    [Fact]
    public async Task GetByIdAsync__ReturnsCarModel()
    {
        //Arrange
        var correctModel = _models[0].Adapt<CarModel>();

        var serializedModel = JsonConvert.SerializeObject(correctModel);
        var cachedModel = Encoding.UTF8.GetBytes(serializedModel);
        _distributedCacheMock.GetDataFromCache(cachedModel);

        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.GetByIdAsync(Guid.NewGuid(), default);

        //Assert
        response.Should().BeEquivalentTo(correctModel);
    }

    [Fact]
    public async Task GetByIdAsync_EmptyCache_ReturnsClientModel()
    {
        //Arrange
        var correctModel = _models[0].Adapt<CarModel>();

        var serializedModel = JsonConvert.SerializeObject(null);
        var cachedModel = Encoding.UTF8.GetBytes(serializedModel);
        _distributedCacheMock.GetDataFromCache(cachedModel);

        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.GetByIdAsync(Guid.NewGuid(), default);

        //Assert
        response.Should().BeEquivalentTo(correctModel);
    }

    [Fact]
    public async Task AddAsync_CarModel_ReturnsCarModel()
    {
        //Arrange
        var correctModel = _models[0].Adapt<CarModel>();
        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.AddAsync(correctModel, default);

        //Assert
        response.Should().BeEquivalentTo(correctModel);
    }

    [Fact]
    public async Task UpdateAsync_CarModel_ReturnsCarModel()
    {
        //Arrange
        var correctUpdatedModel = _models[1].Adapt<CarModel>();
        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.UpdateAsync(correctUpdatedModel, default);

        //Assert
        response.Should().BeEquivalentTo(correctUpdatedModel);
    }

    [Fact]
    public async Task UpdateAsync_CarModel_ThrowsNotFoundException()
    {
        //Arrange
        _repositoryMock.GetById(null);

        var correctUpdatedModel = _models[1].Adapt<CarModel>();
        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = async () => await service.UpdateAsync(correctUpdatedModel, default);

        //Assert
        await response.Should().ThrowAsync<NotFoundException>();
    }


    [Fact]
    public async Task DeleteAsync_CarModelId_()
    {
        //Arrange
        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = async () => await service.DeleteAsync(Guid.NewGuid(), default);

        //Assert
        await response.Should().NotThrowAsync();
    }

    [Fact]
    public async Task DeleteAsync_InvalidId_ThrowsNotFoundException()
    {
        //Arrange
        _repositoryMock.GetById(null);

        var service = new CarModelService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = async () => await service.DeleteAsync(Guid.NewGuid(), default);

        //Assert
        await response.Should().ThrowAsync<NotFoundException>();
    }
}