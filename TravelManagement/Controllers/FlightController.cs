using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/FlightApi")]
    [ApiController]
    public class FlightController : Controller
    {
        private readonly IFlightRepository dbflight;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;
        protected readonly ApiResponse response;
        public FlightController(IFlightRepository _dbflight, IMapper _mapper, ApplicationDbContext _db)
        {
            dbflight = _dbflight;
            mapper = _mapper;
            this.response = new();
            db = _db;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetFlight()
        {
            try
            {
                IEnumerable<Flight> FlightDTOList = await dbflight.GetAllAsync();
                IEnumerable<FlightDTO> FlightDtoList = mapper.Map<IEnumerable<FlightDTO>>(FlightDTOList);
                response.Result = FlightDTOList;
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
        [HttpGet("id", Name = "GetFlight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetFlight(int id)
        {
            try
            {
                
                if (id == 0)
                {
                    return BadRequest();
                }

                var airline = await dbflight.GetAsync(u => u.Id == id);
                if (airline== null)
                {
                    return NotFound();
                }
                response.Result = mapper.Map<FlightDTO>(airline);
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
        public async Task<ActionResult<ApiResponse>> CreateFlight([FromBody] FlightCreateDTO CreateDto)
        {
            if (!IsAirlineCodeValid(CreateDto.AirlineCode))
            {
                ModelState.AddModelError("AirlineCode", "Invalid airline code.");
                return BadRequest(ModelState);
            }
            try
            {
                if (await dbflight.GetAsync(u => u.FlightName.ToLower() == CreateDto.FlightName.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Flight Already Exists");
                    return BadRequest(ModelState);
                }
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }Flight flight = mapper.Map<Flight>(CreateDto);
                
                await dbflight.CreateAsync(flight);
                response.Result = mapper.Map<FlightDTO>(flight);
                response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetFlight", new { id = flight.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }

        private bool IsAirlineCodeValid(string airlineCode)
        {
            
            
                return db.Airlines.Any(a => a.AirlineCode == airlineCode);
            
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}", Name = "DeleteFlight")]
        public async Task<ActionResult<ApiResponse>> DeleteFlight(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var flight = await dbflight.GetAsync(v => v.Id == id);
                if (flight == null)
                {
                    return NotFound();
                }
                await dbflight.RemoveAsync(flight);
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
        [HttpPut("{id:int}", Name = "UpdateFlight")]
        public async Task<ActionResult<ApiResponse>> UpdateFlight(int id, [FromBody] FlightUpdateDTO updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                var villa = await dbflight.GetAsync(v => v.Id == id, tracked: true);
                if (villa == null)
                {
                    // Handle the case when the journey with the given id doesn't exist
                    return NotFound();
                }
                db.Entry(villa).State = EntityState.Detached;

                Flight model = mapper.Map<Flight>(updateDto);
                db.Entry(model).State = EntityState.Modified;

                await dbflight.UpdateAsync(model);
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
