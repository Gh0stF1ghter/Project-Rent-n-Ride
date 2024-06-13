﻿using DAL.Enums;

namespace API.ViewModels;

public record VehicleViewModel(
    Guid Id,
    string PlateNumber,
    int Odo,
    decimal RentCost,
    VehicleType VehicleType,
    VehicleState VehicleState,
    FuelType FuelType,
    CarModelViewModel? Model,
    ClientViewModel? Client
    );