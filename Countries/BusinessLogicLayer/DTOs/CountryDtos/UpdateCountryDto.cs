using Countries.DataLayer.Entities;

namespace Countries.BusinessLogicLayer.DTOs.CountryDtos;

public class UpdateCountryDto
{
    public int Id { get; set; }
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string CapitalUz { get; set; } = string.Empty;
    public string CapitalRu { get; set; } = string.Empty;
    public string CapitalEn { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;

    public static implicit operator Country(UpdateCountryDto dto)
        => new()
        {
            Id = dto.Id,
            NameEn = dto.NameEn,
            NameUz = dto.NameUz,
            NameRu = dto.NameRu,
            CapitalUz = dto.CapitalUz,
            CapitalRu = dto.CapitalRu,
            CapitalEn = dto.CapitalEn,
            Code = dto.Code,
            FlagUrl = dto.FlagUrl,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDeleted = false
        };
}
