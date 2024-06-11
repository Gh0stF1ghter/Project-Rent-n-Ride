﻿namespace BLL.Models;

public class ClientModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public IEnumerable<VchModel>? VehicleClientHistories { get; set; }
}