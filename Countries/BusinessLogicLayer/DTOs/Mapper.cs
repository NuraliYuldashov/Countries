using Countries.BusinessLogicLayer.DTOs.CountryDtos;
using Countries.DataLayer.Entities;

namespace Countries.BusinessLogicLayer.DTOs;

public static class Mapper
{
    public static CountryDto ToDto(this Country country,
                                        Language language)
        => new()
        {
            Id = country.Id,
            Code = country.Code,
            FlagUrl = country.FlagUrl,
            Name = language switch
            {
                Language.uz => country.NameUz,
                Language.ru => country.NameRu,
                Language.en => country.NameEn,
                _ => country.NameUz,
            },
            Capital = language switch
            {
                Language.uz => country.CapitalUz,
                Language.ru => country.NameRu,
                Language.en => country.CapitalEn,
                _ => country.CapitalUz,
            }
        };
}
