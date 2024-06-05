﻿namespace DAL.Entities;

public class Manufacturer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<Model> Models { get; set; } = [];
}