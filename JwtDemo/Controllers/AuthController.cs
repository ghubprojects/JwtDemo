using JwtDemo.Authentication;
using JwtDemo.Entities;
using JwtDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IUserRepository userRepository, IJwtProvider jwtProvider) : ControllerBase {
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    [HttpPost("/login")]
    public IActionResult Login(string email) {
        var user = _userRepository.GetByEmail(email);

        if (user is null) {
            return BadRequest("User not found");
        }

        var accessToken = _jwtProvider.GenerateToken(user);
        return Ok(accessToken);
    }

    [HttpPost("/register")]
    public IActionResult Register(string email, string firstName, string lastName) {
        if (!_userRepository.IsEmailUnique(email)) {
            return BadRequest("Email is already in use");
        }

        var user = new User {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
        _userRepository.Add(user);

        return Ok(user.Id);
    }
}
