﻿using DAL.Entities;

namespace BLL.Models;

public record ClientModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public IEnumerable<VehicleClientHistory>? VehicleClientHistories { get; set; }
}