﻿namespace DAL.Entities;

public class Client
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public IEnumerable<VehicleClientHistory>? VehicleClientHistory { get; set; }
}