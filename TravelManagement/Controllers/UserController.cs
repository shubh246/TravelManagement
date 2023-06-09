﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelManagement.Models;
using TravelManagement.Models.Dto;
using TravelManagement.Repository;

namespace TravelManagement.Controllers
{
    [Route("api/TravelApi")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository dbuser;
        private readonly IMapper mapper;
        protected readonly ApiResponse response;
        public UserController(IUserRepository _dbuser, IMapper _mapper)
        {
            dbuser = _dbuser;
            mapper = _mapper;
            this.response = new();
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetUser()
        {
            try
            {
                IEnumerable<User> UserDTOList = await dbuser.GetAllAsync();
                IEnumerable<UserDTO> VillaDtoList = mapper.Map<IEnumerable<UserDTO>>(UserDTOList);
                response.Result = UserDTOList;
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
        [HttpGet("id", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetUser(int id)
        {
            try
            {
                
                if (id == 0)
                {
                    return BadRequest();
                }

                var user = await dbuser.GetAsync(u => u.Id == id);
                if (user== null)
                {
                    return NotFound();
                }
                response.Result = mapper.Map<UserDTO>(user);
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
        public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] UserCreateDTO CreateDto)
        {
            try
            {
                if (await dbuser.GetAsync(u => u.Name.ToLower() == CreateDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "User Already Exists");
                    return BadRequest(ModelState);
                }
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                User user = mapper.Map<User>(CreateDto);
                
                await dbuser.CreateAsync(user);
                response.Result = mapper.Map<UserDTO>(user);
                response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetUser", new { id = user.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var user = await dbuser.GetAsync(v => v.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                await dbuser.RemoveAsync(user);
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
        [HttpPut("{id:int}", Name = "UpdateUser")]
        public async Task<ActionResult<ApiResponse>> UpdateUser(int id, [FromBody] UserUpdateDTO updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                var villa = await dbuser.GetAsync(v => v.Id == id, tracked: false);
                
                User model = mapper.Map<User>(updateDto);
                
                await dbuser.UpdateAsync(model);
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
