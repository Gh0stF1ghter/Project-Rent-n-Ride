﻿using DAL.Enums;

namespace BLL.Models;

public record VehicleModel
{
    public Guid Id { get; set; }

    public string PlateNumber { get; set; } = string.Empty;
    public int Odo { get; set; }
    public double RentCost { get; set; }

    public VehicleType VehicleType { get; set; }
    public VehicleState VehicleState { get; set; }
    public FuelType FuelType { get; set; }

    public Guid ModelId { get; set; }
    public ModelNameModel? Model { get; set; }

    public Guid ClientId { get; set; }
    public ClientModel? Client { get; set; }
}
