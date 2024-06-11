﻿using DAL.Entities;
using DAL.Enums;

namespace BLL.Models;

public record VehicleModel(
    Guid Id,
    string PlateNumber,
    int Odo,
    double RentCost,
    VehicleType VehicleType,
    VehicleState VehicleState,
    FuelType FuelType,
    ModelName? Model,
    Client? Client
    );