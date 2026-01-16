using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PakClassified;
using PakClassified.Entities.UserEntities;
//using Project.Context.Entites.UserEntites;
using Project.Context.Handlers.Auth;
using Project.Models.UserModels.Auth;
using PakClassified.Models.User;

//using Project.Models.UserModels.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] PakClassified.Models.User.ForgotPasswordRequest request)
        {
            try
            {
                using (var context = new PakClassifiedContext())
                {
                    var user = context.Users
                        .FirstOrDefault(u => u.Email == request.Email && !u.IsDeleted);

                    if (user == null)
                        return NotFound(new { message = "User not found" });

                    return Ok(new
                    {
                        securityQuestion = user.SecurityQuestion,
                        email = user.Email
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost("VerifySecurityAnswer")]
        public IActionResult VerifySecurityAnswer([FromBody] VerifySecurityAnswerRequest request)
        {
            try
            {
                using (var context = new PakClassifiedContext())
                {
                    var user = context.Users
                        .FirstOrDefault(u => u.Email == request.Email && !u.IsDeleted);

                    if (user == null)
                        return NotFound(new { message = "User not found" });

                    // Case insensitive comparison
                    bool isVerified = string.Equals(
                        user.SecurityAnswer?.Trim(),
                        request.SecurityAnswer?.Trim(),
                        StringComparison.OrdinalIgnoreCase
                    );

                    if (!isVerified)
                        return BadRequest(new { message = "Incorrect security answer" });

                    return Ok(new
                    {
                        isVerified = true,
                        message = "Security answer verified successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] PakClassified.Models.User.ResetPasswordRequest request)
        {
            try
            {
                using (var context = new PakClassifiedContext())
                {
                    var user = context.Users
                        .FirstOrDefault(u => u.Email == request.Email && !u.IsDeleted);

                    if (user == null)
                        return NotFound(new { message = "User not found" });

                    // Hash new password
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                    user.ModifiedDate = DateTime.Now;

                    context.Update(user);
                    context.SaveChanges();

                    return Ok(new { message = "Password reset successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel request)
        {
            // Checks if the provided model (request) is valid based on data annotations in the Login model.
            if (ModelState.IsValid)
            {
                // Searches for a user in a predefined user store that matches both username and password.
                //User service...
                User? user = new AuthServices().GetUserByEmail(request.Email, request.Password);
                // Checks if the user object is null, which means no matching user was found.
                if (user == null)
                {
                    // Returns a 401 Unauthorized response with a custom message.
                    return Unauthorized("Invalid user credentials.");
                }
                //Sanitize the User Model Role to doesnot contain Users
                user.Role.Users = null;
                // Calls a method to generate a JWT token for the authenticated user.
                var token = IssueToken(user);
                // Returns a 200 OK response, encapsulating the JWT token in an anonymous object.
                return Ok(new { Token = token, Payload = user });
            }
            // If the model state is not valid, returns a 400 Bad Request response with a custom message.
            return BadRequest("Invalid Request Body");
        }
        private string IssueToken(User user)
        {
            // Creates a new symmetric security key from the JWT key specified in the app configuration.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // Sets up the signing credentials using the above security key and specifying the HMAC SHA256 algorithm.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Defines a set of claims to be included in the token.
            var claims = new List<Claim>
            {
                // Custom claim using the user's ID.
                new Claim("Myapp_User_Id", user.Id.ToString()),
                // Standard claim for user identifier, using email.
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                // Standard claim for user's email.
                new Claim(ClaimTypes.Email, user.Email),
                // Standard JWT claim for subject, using user ID.
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            // Adds a role claim for each role associated with the user.
            //user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Name)));


            // Creates a new JWT token with specified parameters including issuer, audience, claims, expiration time, and signing credentials.
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration set to 1 hour from the current time.
                signingCredentials: credentials);
            // Serializes the JWT token to a string and returns it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
