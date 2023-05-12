using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelManagement.Models;
using TravelManagement.Models.Dto;
using TravelManagement.Repository;

namespace TravelManagement.Controllers
{
    [Route("api/AirlineApi")]
    [ApiController]
    public class AirlineController : Controller
    {
        private readonly IAirlineRepository dbairline;
        private readonly IMapper mapper;
        protected readonly ApiResponse response;
        public AirlineController(IAirlineRepository _dbairline, IMapper _mapper)
        {
            dbairline = _dbairline;
            mapper = _mapper;
            this.response = new();
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAirline()
        {
            try
            {
                IEnumerable<Airline> AirlineDTOList = await dbairline.GetAllAsync();
                IEnumerable<AirlineDTO> VillaDtoList = mapper.Map<IEnumerable<AirlineDTO>>(AirlineDTOList);
                response.Result = AirlineDTOList;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            // return Ok( await db.Villas.ToListAsync());
            /*return new List<VillaDto>{
            new VillaDto{Id=1,Name="Shubham"},
           new VillaDto{Id=2,Name="Shubham23"}
        };*/
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [HttpGet("id", Name = "GetAirline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetAirline(int id)
        {
            try
            {
                //logger.Log("Geetting all villas","");
                if (id == 0)
                {
                    return BadRequest();
                }

                var airline = await dbairline.GetAsync(u => u.Id == id);
                if (airline== null)
                {
                    return NotFound();
                }
                response.Result = mapper.Map<AirlineDTO>(airline);
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
        public async Task<ActionResult<ApiResponse>> CreateAirline([FromBody] AirlineCreateDTO CreateDto)
        {
            try
            {
                if (await dbairline.GetAsync(u => u.AirlineName.ToLower() == CreateDto.AirlineName.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Airline Already Exists");
                    return BadRequest(ModelState);
                }
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                Airline airline = mapper.Map<Airline>(CreateDto);
                //if (villaDto.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                //Villa model = new()
                //{
                //    Amenity = villaDto.Amenity, 
                //    Details = villaDto.Details,
                //    //Id=villaDto.Id,
                //    ImageUrl=villaDto.ImageUrl,
                //    Name=villaDto.Name,
                //    Occupancy=villaDto.Occupancy,
                //    Rate=villaDto.Rate,
                //    Sqft=villaDto.Sqft,
                //};
                //await db.Villas.AddAsync(model);
                // db.SaveChanges();
                await dbairline.CreateAsync(airline);
                response.Result = mapper.Map<AirlineDTO>(airline);
                response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetUser", new { id = airline.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [HttpDelete("{id:int}", Name = "DeleteAirline")]
        public async Task<ActionResult<ApiResponse>> DeleteAirline(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var airline = await dbairline.GetAsync(v => v.Id == id);
                if (airline == null)
                {
                    return NotFound();
                }
                await dbairline.RemoveAsync(airline);
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
        [HttpPut("{id:int}", Name = "UpdateAirline")]
        public async Task<ActionResult<ApiResponse>> UpdateUser(int id, [FromBody] AirlineUpdateDTO updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                var villa = await dbairline.GetAsync(v => v.Id == id, tracked: false);
                /*villa.Name=villaDto.Name;   
                villa.Sqft = villaDto.Sqft; 
                villa.Occupancy= villaDto.Occupancy;*/
                Airline model = mapper.Map<Airline>(updateDto);
                //Villa model = new()
                //{
                //    Amenity = villaDto.Amenity,
                //    Details = villaDto.Details,
                //    Id = villaDto.Id,
                //    ImageUrl = villaDto.ImageUrl,
                //    Name = villaDto.Name,
                //    Occupancy = villaDto.Occupancy,
                //    Rate = villaDto.Rate,
                //    Sqft = villaDto.Sqft,
                //};
                await dbairline.UpdateAsync(model);
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
