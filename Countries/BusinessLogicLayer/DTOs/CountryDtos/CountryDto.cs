﻿namespace Countries.BusinessLogicLayer.DTOs.CountryDtos;

public class CountryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;
}
