﻿namespace DAL.Entities;

public class VehicleClientHistory
{
    public Guid Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
}