using Countries.BusinessLogicLayer.DTOs;
using Countries.BusinessLogicLayer.DTOs.CountryDtos;
using Countries.BusinessLogicLayer.Interfaces;
using Countries.DataLayer;
using Countries.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Countries.BusinessLogicLayer.Service;

public class CountryService(AppDbContext dbContext,
                            IRedisService redisService) : ICountryInterface
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IRedisService _redisService = redisService;
    private const string CACHE_KEY = "countries"; 

    public async Task Add(AddCountryDto dto)
    {
        var country = (Country)dto;
        await _dbContext.Countries.AddAsync(country);
        await _dbContext.SaveChangesAsync();
        await _redisService.RemoveAsync(CACHE_KEY);
    }

    public async Task Delete(int id)
    {
        var country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);
        _dbContext.Countries.Remove(country ?? 
            throw new Exception(nameof(country)));
        await _dbContext.SaveChangesAsync();
        await _redisService.RemoveAsync(CACHE_KEY);
    }

    public async Task<string> GetAll(Language language)
    {
        var data = await _redisService.GetAsync(CACHE_KEY);
        if (data != null)
        {
            var dtoList = JsonConvert.DeserializeObject<List<Country>>(data)
                                       .Select(c => c.ToDto(language))
                                       .ToList();
            var jsonData = JsonConvert.SerializeObject(dtoList, Formatting.Indented);
            return jsonData;
        }

        var list = await _dbContext.Countries.ToListAsync();

        await _redisService.SetAsync(CACHE_KEY, JsonConvert.SerializeObject(list));
        
        var dtos = list.Select(c => c.ToDto(language))
                                     .ToList();

        var json = JsonConvert.SerializeObject(dtos, Formatting.Indented);
        return json;
    }

    public async Task<string> GetById(int id, Language language)
    {
        var data = await GetAll(language);
        var dto = JsonConvert.DeserializeObject<List<CountryDto>>(data)
                                            .FirstOrDefault(c => c.Id == id);
        return JsonConvert.SerializeObject(dto, Formatting.Indented);
        
    }

    public async Task Update(UpdateCountryDto countryDto)
    {
        _dbContext.Countries.Update((Country)countryDto);
        await _dbContext.SaveChangesAsync();
        await _redisService.RemoveAsync(CACHE_KEY);
    }
}
