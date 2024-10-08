﻿using Azure.Core;
using CDNPortalTutorial.Data;
using CDNPortalTutorial.Features.Users.Commands.CreateUser;
using CDNPortalTutorial.Model.Dto;
using CDNPortalTutorial.Model.Entities;
using CDNPortalTutorial.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace CDNPortalTutorial.Controllers
{
    // localhost:xxxx/api/v1/User
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IValidator<UpdateUserDto> _updateUserValidator;

        public UserController(UserService userService,
            IValidator<UpdateUserDto> updateUserValidator)
        {
            _userService = userService;
            _updateUserValidator = updateUserValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var user = await _userService.GetUserAsync(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand data)
        {
           var createdUser = await _userService.CreateUserAsync(data);
           return Ok(createdUser);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto data)
        {
            // Validate the incoming request
            var validationResult = await _updateUserValidator.ValidateAsync(data);
            // Show error
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            var user = await _userService.UpdateUserAsync(id, data);
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id) {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted");
        }
    }
}
