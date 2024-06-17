﻿using BLL.Exceptions;
using BLL.Models;
using BLL.Services.Implementations;
using DAL.Entities;
using FluentAssertions;
using Mapster;
using Tests.DataGeneration;
using Tests.Mocks;

namespace Tests.ServicesTests;

public class ClientServiceTests
{
    private readonly ClientRepositoryMock _repositoryMock = new();

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
        var service = new ClientService(_repositoryMock.Object);

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
        var service = new ClientService(_repositoryMock.Object);

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
        var service = new ClientService(_repositoryMock.Object);

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
        var service = new ClientService(_repositoryMock.Object);

        //Act
        var response = await service.UpdateAsync(correctClientModel, default);

        //Assert
        response.Should().BeEquivalentTo(correctClientModel);
    }

    [Fact]
    public async Task UpdateAsync_InvalidId_ThrowsNotFoundException()
    {
        //Arrange
        _repositoryMock.GetById(null);

        var correctClientModel = _clients[1].Adapt<ClientModel>();
        var service = new ClientService(_repositoryMock.Object);

        //Act
        var response = async () => await service.UpdateAsync(correctClientModel, default);

        //Assert
        await response.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_InvalidId_ThrowsNotFoundException()
    {
        //Arrange
        _repositoryMock.GetById(null);

        var service = new ClientService(_repositoryMock.Object);

        //Act
        var response = async () => await service.DeleteAsync(Guid.NewGuid(), default);

        //Assert
        await response.Should().ThrowAsync<NotFoundException>();
    }
}