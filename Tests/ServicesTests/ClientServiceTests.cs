﻿using BLL.Models;
using BLL.Services.Implementations;
using DAL.Entities;
using FluentAssertions;
using Mapster;
using Newtonsoft.Json;
using System.Text;
using Tests.DataGeneration;
using Tests.Mocks;

namespace Tests.ServicesTests;

public class ClientServiceTests
{
    private readonly ClientRepositoryMock _repositoryMock = new();
    private readonly DistributedCacheMock _distributedCacheMock = new();

    private readonly List<ClientEntity> _clients = DataGenerator.AddClientData(5);

    public ClientServiceTests()
    {
        _repositoryMock.GetRange(_clients);
        _repositoryMock.GetById(_clients[0]);
        _repositoryMock.IsExists(true);
    }

    [Fact]
    public async Task GetRangeAsync__ReturnsClientModelList()
    {
        //Arrange
        var correctClientModels = _clients.Adapt<IEnumerable<ClientModel>>();

        var serializedModel = JsonConvert.SerializeObject(correctClientModels);
        var cachedModel = Encoding.UTF8.GetBytes(serializedModel);
        _distributedCacheMock.GetDataFromCache(cachedModel);

        var service = new ClientService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.GetRangeAsync(1, 1, default);

        //Assert
        response.Should().BeEquivalentTo(correctClientModels);
    }

    [Fact]
    public async Task GetRangeAsync_EmptyCache_ReturnsClientModelList()
    {
        //Arrange
        var correctClientModels = _clients.Adapt<IEnumerable<ClientModel>>();
        var service = new ClientService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.GetRangeAsync(1, 1, default);

        //Assert
        response.Should().BeEquivalentTo(correctClientModels);
    }

    [Fact]
    public async Task GetByIdAsync__ReturnsClientModel()
    {
        //Arrange
        var correctClientModel = _clients[0].Adapt<ClientModel>();

        var serializedModel = JsonConvert.SerializeObject(correctClientModel);
        var cachedModel = Encoding.UTF8.GetBytes(serializedModel);
        _distributedCacheMock.GetDataFromCache(cachedModel);

        var service = new ClientService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.GetByIdAsync(Guid.NewGuid(), default);

        //Assert
        response.Should().BeEquivalentTo(correctClientModel);
    }

    [Fact]
    public async Task GetByIdAsync_EmptyCache_ReturnsClientModel()
    {
        //Arrange
        var correctClientModel = _clients[0].Adapt<ClientModel>();
        var service = new ClientService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.GetByIdAsync(Guid.NewGuid(), default);

        //Assert
        response.Should().BeEquivalentTo(correctClientModel);
    }

    [Fact]
    public async Task AddAsync_ClientModel_ReturnsClientModel()
    {
        //Arrange
        var correctClientModel = _clients[0].Adapt<ClientModel>();
        var service = new ClientService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.AddAsync(correctClientModel, default);

        //Assert
        response.Should().BeEquivalentTo(correctClientModel);
    }

    [Fact]
    public async Task UpdateAsync_ClientModel_ReturnsClientModel()
    {
        //Arrange
        var correctClientModel = _clients[1].Adapt<ClientModel>();
        var service = new ClientService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = await service.UpdateAsync(correctClientModel, default);

        //Assert
        response.Should().BeEquivalentTo(correctClientModel);
    }

    [Fact]
    public async Task DeleteAsync_ClientId_()
    {
        //Arrange
        var service = new ClientService(_repositoryMock.Object, _distributedCacheMock.Object);

        //Act
        var response = async () => await service.DeleteAsync(Guid.NewGuid(), default);

        //Assert
        await response.Should().NotThrowAsync();
    }
}