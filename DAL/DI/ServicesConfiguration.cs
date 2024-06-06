﻿using DAL.Context;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.DI;

public static class ServicesConfiguration
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<AgencyDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DBConnection")));

    public static void AddReposDependencies(this IServiceCollection services)
    {
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
        services.AddTransient<IVehicleClientHistoryRepository, VehicleClientHistoryRepository>();
        services.AddTransient<IVehicleModelRepository, VehicleModelRepository>();
        services.AddTransient<IVehicleRepository, VehicleRepository>();
    }
}