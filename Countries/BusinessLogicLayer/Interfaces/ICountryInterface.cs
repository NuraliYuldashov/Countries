using Countries.BusinessLogicLayer.DTOs.CountryDtos;

namespace Countries.BusinessLogicLayer.Interfaces;

public interface ICountryInterface
{
    Task<List<CountryDto>> GetAll(Language language);
    Task<CountryDto> GetById(int id, Language language);
    Task Add(AddCountryDto dto);
    Task Update(UpdateCountryDto countryDto);
    Task Delete(int id);
}
