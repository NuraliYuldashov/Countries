using Countries.BusinessLogicLayer;
using Countries.BusinessLogicLayer.DTOs.CountryDtos;
using Countries.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(ICountryInterface countryInterface) : ControllerBase
{
    private readonly ICountryInterface _countryInterface = countryInterface;


    [HttpGet("{lang}")]
    public async Task<IActionResult> Get(string lang)
    {
        Language language = Language.uz;
        try
        {
            language = (Language)Enum.Parse(typeof(Language), lang.ToLower());
        }
        catch (Exception)
        {
            return BadRequest("Language is not supported!");
        }

        var list = await _countryInterface.GetAll(language);
        return Ok(list);
    }

    [HttpGet("{id}/{lang}")]
    public async Task<IActionResult> Get(int id, string lang)
    {
        Language language = Language.uz;
        try
        {
            language = (Language)Enum.Parse(typeof(Language), lang.ToLower());
        }
        catch (Exception)
        {
            return BadRequest("Language is not supported!");
        }

        var country = await _countryInterface.GetById(id, language);
        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddCountryDto dto)
    {
        await _countryInterface.Add(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateCountryDto dto)
    {
        await _countryInterface.Update(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _countryInterface.Delete(id);
        return Ok();
    }
}
