using Countries.BusinessLogicLayer.DTOs;
using Countries.BusinessLogicLayer.DTOs.CountryDtos;
using Countries.BusinessLogicLayer.Interfaces;
using Countries.DataLayer;
using Countries.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Countries.BusinessLogicLayer.Service;

public class CountryService(AppDbContext dbContext) : ICountryInterface
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Add(AddCountryDto dto)
    {
        var country = (Country)dto;
        await _dbContext.Countries.AddAsync(country);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);
        _dbContext.Countries.Remove(country ?? 
            throw new Exception(nameof(country)));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CountryDto>> GetAll(Language language)
    {
        var list = await _dbContext.Countries.ToListAsync();
        var dtos = list.Select(c => c.ToDto(language))
                                     .ToList();
        return dtos;
    }

    public async Task<CountryDto> GetById(int id, Language language)
    {
        var country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);

        if (country == null)
        {
            throw new Exception(nameof(country));
        }

        return country.ToDto(language);
    }

    public async Task Update(UpdateCountryDto countryDto)
    {
        _dbContext.Countries.Update((Country)countryDto);
        await _dbContext.SaveChangesAsync();
    }
}
