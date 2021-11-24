using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyWebAPI.Data;
using FamilyWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyWebAPI.Controllers
{
    [ApiController]
    [Route("Families")]
    public class FamilyController : ControllerBase
    {
        private IFamilyDataService _familyDataService;

        public FamilyController(IFamilyDataService familyDataService)
        {
            _familyDataService = familyDataService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Family>>> GetFamilies(){
            try
            {
                var families = await _familyDataService.GetFamiliesAsync();
                return Ok(families);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        // [HttpPost]
        // public async Task<ActionResult<Family>> AddFamily([FromBody] Family family)
        // {
        //     try
        //     {
        //         var added = await _familyDataService.AddFamilyAsync(family);
        //         return Created($"/{added.StreetName} {added.HouseNumber}", added);
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         return StatusCode(500, e.Message);
        //     }
        // }
        
        [HttpGet]
        [Route("[controller]")]
        public async Task<ActionResult<Family>> GetFamily([FromQuery] string streetName, [FromQuery] int houseNumber){
            try
            {
                var family = await _familyDataService.GetFamilyAsync(streetName,houseNumber);
                return Ok(family);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        [Route("adult")]
        public async Task<ActionResult<Adult>> AddAdult([FromQuery] string streetName, [FromQuery] int houseNumber,[FromBody] Adult adult)
        {
            try
            {
                var added = await _familyDataService.AddAdultAsync(streetName,houseNumber,adult);
                return Created($"/{added.Id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("adult/{id:int}")]
        public async Task<ActionResult> RemoveAdult([FromRoute] int id)
        {
            try
            {
                var deletedAdultId = await _familyDataService.RemoveAdultAsync(id);
                return Ok($"Adult with id: {deletedAdultId} deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}