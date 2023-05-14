using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using TravelManagement.Data;
using TravelManagement.Models;
using TravelManagement.Models.Dto;
using TravelManagement.Repository;

namespace TravelManagement.Controllers
{
    [Route("api/JourneyApi")]
    [ApiController]
    public class JourneyController : Controller
    {
        private readonly IJourneyRepository dbjourney;
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        protected readonly ApiResponse response;
        public JourneyController(IJourneyRepository _dbjourney, IMapper _mapper, ApplicationDbContext _db)
        {
            dbjourney = _dbjourney;
            db = _db;
            mapper = _mapper;
            this.response = new();
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetJourney()
        {
            try
            {
                IEnumerable<Journey> JourneyDTOList = await dbjourney.GetAllAsync();
                IEnumerable<JourneyDTO> JourneyDtoList = mapper.Map<IEnumerable<JourneyDTO>>(JourneyDTOList);
                response.Result = JourneyDTOList;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [HttpGet("id", Name = "GetJourney")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetJourney(int id)
        {
            try
            {
                
                if (id == 0)
                {
                    return BadRequest();
                }

                var journey = await dbjourney.GetAsync(u => u.Id == id);
                if (journey== null)
                {
                    return NotFound();
                }
                response.Result = mapper.Map<JourneyDTO>(journey);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateJourney([FromBody] JourneyCreateDTO CreateDto)
        {
            try
            {
                if (await dbjourney.GetAsync(u => u.FromCity.ToLower() == CreateDto.FromCity.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Journey Already Exists");
                    return BadRequest(ModelState);
                }
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                var airline = db.Airlines.FirstOrDefault(a => a.Id == CreateDto.AirlineId);
                var flight = db.Flights.FirstOrDefault(f => f.Id == CreateDto.FlightId);
                if (airline == null || flight == null) { 
                    return BadRequest("Invalid AirlineId or FlightId");
            
            }
                Journey journey = mapper.Map<Journey>(CreateDto);
                
                await dbjourney.CreateAsync(journey);
                response.Result = mapper.Map<JourneyDTO>(journey);
                response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetJourney", new { id = journey.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}", Name = "DeleteJourney")]
        public async Task<ActionResult<ApiResponse>> DeleteJourney(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var journey = await dbjourney.GetAsync(v => v.Id == id);
                if (journey == null)
                {
                    return NotFound();
                }
                await dbjourney.RemoveAsync(journey);
                //response.Result = mapper.Map<VillaDto>(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [HttpPut("{id:int}", Name = "UpdateJourney")]
        public async Task<ActionResult<ApiResponse>> UpdateJourney(int id, [FromBody] JourneyUpdateDTO updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                var villa = await dbjourney.GetAsync(v => v.Id == id, tracked: true);
                if (villa == null)
                {
                    // Handle the case when the journey with the given id doesn't exist
                    return NotFound();
                }
                db.Entry(villa).State = EntityState.Detached;


                var airline = db.Airlines.FirstOrDefault(a => a.Id == updateDto.AirlineId);
                var flight = db.Flights.FirstOrDefault(f => f.Id == updateDto.FlightId);
                if (airline == null || flight == null)
                {
                    return BadRequest("Invalid AirlineId or FlightId");

                }
                Journey model = mapper.Map<Journey>(updateDto);
                db.Entry(model).State = EntityState.Modified;


                await dbjourney.UpdateAsync(model);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;


        }
    }
}
