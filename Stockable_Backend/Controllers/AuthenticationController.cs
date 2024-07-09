using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.Repository;
using System.Web;

namespace Stockable_Backend.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserClaimsPrincipalFactory<User> _claimsPrincipalFactory;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager, RoleManager<Role> roleManager, IUserClaimsPrincipalFactory<User> claimsPrincipalFactory, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserViewModal user)
        {
            var newUser = await _userManager.FindByEmailAsync(user.emailaddress);

            if (newUser == null)
            {
                try
                {
                    if (await _roleManager.RoleExistsAsync(user.role))
                    {
                        newUser = new User
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = user.emailaddress,
                            Email = user.emailaddress,
                            userFirstName = user.userFirstName,
                            userLastName = user.userLastName,
                            userType = user.userType,
                            PhoneNumber = user.phoneNumber,
                        };

                        var result = await _userManager.CreateAsync(newUser, user.password);



                        await _userManager.AddToRoleAsync(newUser, user.role); // Assign "Inputted role from frontend" role to the user. DO NOT PUT BEFORE USER CREATION

                        // sendconfirmation token
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        
                        var encodedToken = HttpUtility.UrlEncode(token); // Encode before sending in the URL

                        MailData mailData = new()
                        {
                            toEmailAddress = user.emailaddress,
                            subject = "Stockable Email Confirmation",

                            //using route paramters
                            //messageBody = $"Please confirm your email by <a href=\"http://localhost:4200/confirm-email/{user.Id}/{encodedToken}\">clicking here</a>.",

                            //using query paramters
                            messageBody = $"Please confirm your email by <a href=\"http://localhost:4200/confirm-email?userId={newUser.Id}&token={encodedToken}\">clicking here</a>.\r\n",
                        };

                        var httpClient = new HttpClient();
                        var response = await httpClient.PostAsJsonAsync("http://localhost:58886/api/Mail/SendMail", mailData);

                        //if successful do something
                        if (result.Succeeded)
                        {
                            return StatusCode(StatusCodes.Status200OK, new { UserId = newUser.Id });
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "User Role " + user.role + " not found. Please choose from either SUPER_ADMIN, ADMIN, CLIENT_USER, HUB_USER, CLIENT_ADMIN, TECHNICIAN, INVENTORY_CLERK");
                    }

                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status409Conflict, newUser.UserName + " already exists");
            }
        }

        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.userId);

            try
            {
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User does not exist");
                }
                else
                {
                    var result = await _userManager.ConfirmEmailAsync(user, model.token);
                    if (result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status200OK, "Email Confirmed");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Email could not be confirmed");
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured: " + e);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginUserViewModel luvm)
        {
            var user = await _userManager.FindByNameAsync(luvm.emailaddress);
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Email address has not been confirmed.");
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, luvm.password))
            {
                try
                {
                    //var principal = await _claimsPrincipalFactory.CreateAsync(user);
                    //await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
                    return GenerateJWTToken(user);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
                }
            }
            else
            {
                return NotFound("invalid email or password");
            }

            //var loggedInUser = new UserViewModel { EmailAddress = uvm.EmailAddress, Password = uvm.Password };

            //return Ok(loggedInUser);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<object>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userViewModel = new
                {
                    user.Id,
                    user.UserName,
                    user.userLastName,
                    user.userFirstName,
                    user.userType,
                    user.Email,
                    user.PhoneNumber,
                    user.profilePicture,
                    Roles = roles
                };
                userViewModels.Add(userViewModel);
            }

            return Ok(userViewModels);
        }

        [HttpGet]
        [Route("GetUser/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = _userManager.GetRolesAsync(user).Result;

            if (user != null)
            {
                var userModel = new UserViewModal
                {
                    Id = user.Id,
                    userFirstName = user.userFirstName,
                    userLastName = user.userLastName,
                    userType = user.userType,
                    userName = user.UserName,
                    phoneNumber = user.PhoneNumber,
                    email = user.Email,
                    profilePicture = user.profilePicture,
                    role = role[0]
                };

                return Ok(userModel);
            }

            return NotFound("User not found");
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete user. Please contact support.");
            }
        }

        [HttpPut("UpdateUser/{userIdOrEmail}")]
        public async Task<IActionResult> UpdateUser(string userIdOrEmail, UpdateUserViewModel model)
        {
            var user = await GetUserByIdOrEmail(userIdOrEmail);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update user's email if provided
            if (!string.IsNullOrWhiteSpace(model.email) && model.email != user.Email)
            {
                var newUser = await _userManager.FindByEmailAsync(model.email);
                if (newUser == null)
                {
                    user.Email = model.email;
                    user.UserName = model.email;
                    var emailResult = await _userManager.UpdateAsync(user);

                    if (!emailResult.Succeeded)
                    {
                        return StatusCode(500, "Failed to update email");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, newUser.UserName + " already exists");
                }
            }

            // Update user's password if provided
            if (!string.IsNullOrWhiteSpace(model.newPassword))
            {
                // Compare current password with the password stored in the database
                var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, model.currentPassword);
                if (!isCurrentPasswordValid)
                {
                    return StatusCode(500, "Old password is incorrect. Please try again");
                }

                if (model.newPassword != model.confirmPassword)
                {
                    return StatusCode(500, "Passwords do not match");
                }

                if (model.currentPassword != model.newPassword)
                {
                    var passwordResult = await _userManager.ChangePasswordAsync(user, model.currentPassword, model.newPassword);

                    if (!passwordResult.Succeeded)
                    {
                        return StatusCode(500, "Failed to update password");
                    }
                }
                else
                {
                    return StatusCode(500, "New password should not match the old password");
                }
            }

            // Update user's role if provided
            if (!string.IsNullOrWhiteSpace(model.role))
            {
                var existingRoles = await _userManager.GetRolesAsync(user);
                var roleResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);

                if (!roleResult.Succeeded)
                {
                    return StatusCode(500, "Failed to remove existing roles");
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, model.role);

                if (!addRoleResult.Succeeded)
                {
                    return StatusCode(500, "Failed to add new role");
                }
            }

            // Update user's first name if provided
            if (!string.IsNullOrWhiteSpace(model.userFirstName))
            {
                user.userFirstName = model.userFirstName;
            }

            // Update user's last name if provided
            if (!string.IsNullOrWhiteSpace(model.userLastName))
            {
                user.userLastName = model.userLastName;
            }

            // Update user's user type if provided
            if (!string.IsNullOrWhiteSpace(model.userType))
            {
                user.userType = model.userType;
            }

            // Update user's phone number if provided
            if (!string.IsNullOrWhiteSpace(model.phoneNumber))
            {
                user.PhoneNumber = model.phoneNumber;
            }

            // Update user's profile picture if provided
            if (!string.IsNullOrWhiteSpace(model.profilePicture))
            {
                user.profilePicture = model.profilePicture;
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return StatusCode(500, "Failed to update user details");
            }

            return Ok("User updated successfully");
        }

        //purely for image removals
        [HttpPut]
        [Route("RemoveProfilePicture/{userIdOrEmail}")]
        public async Task<IActionResult> RemoveProfilePicture(string userIdOrEmail, UpdateUserViewModel model)
        {
            var user = await GetUserByIdOrEmail(userIdOrEmail);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update user's email if provided
            if (!string.IsNullOrWhiteSpace(model.email) && model.email != user.Email)
            {
                var newUser = await _userManager.FindByEmailAsync(model.email);
                if (newUser == null)
                {
                    user.Email = model.email;
                    user.UserName = model.email;
                    var emailResult = await _userManager.UpdateAsync(user);

                    if (!emailResult.Succeeded)
                    {
                        return StatusCode(500, "Failed to update email");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, newUser.UserName + " already exists");
                }

            }

            // Update user's password if provided
            if (!string.IsNullOrWhiteSpace(model.newPassword))
            {
                if (model.currentPassword != model.newPassword)
                {
                    var passwordResult = await _userManager.ChangePasswordAsync(user, model.currentPassword, model.newPassword);

                    if (!passwordResult.Succeeded)
                    {
                        return StatusCode(500, "Failed to update password");
                    }
                }
                else
                {
                    return StatusCode(500, "New password should not match the old password");
                }
            }

            // Update user's role if provided
            if (!string.IsNullOrWhiteSpace(model.role))
            {
                var existingRoles = await _userManager.GetRolesAsync(user);
                var roleResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);

                if (!roleResult.Succeeded)
                {
                    return StatusCode(500, "Failed to remove existing roles");
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, model.role);

                if (!addRoleResult.Succeeded)
                {
                    return StatusCode(500, "Failed to add new role");
                }
            }

            // Update user's first name if provided
            if (!string.IsNullOrWhiteSpace(model.userFirstName))
            {
                user.userFirstName = model.userFirstName;
            }

            // Update user's last name if provided
            if (!string.IsNullOrWhiteSpace(model.userLastName))
            {
                user.userLastName = model.userLastName;
            }

            // Update user's user type if provided
            if (!string.IsNullOrWhiteSpace(model.userType))
            {
                user.userType = model.userType;
            }

            // Update user's phone number if provided
            if (!string.IsNullOrWhiteSpace(model.phoneNumber))
            {
                user.PhoneNumber = model.phoneNumber;
            }

            // Update user's profile picture. only used for removing picture
            if (string.IsNullOrWhiteSpace(model.profilePicture))
            {
                user.profilePicture = null;
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return StatusCode(500, "Failed to update user details");
            }

            return Ok("User updated successfully");
        }

        // purely for image uploads
        [HttpPut("UpdateUserAccount/{userIdOrEmail}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateUserAccount(string userIdOrEmail, [FromForm] IFormCollection formData)
        {
            try
            {
                var user = await GetUserByIdOrEmail(userIdOrEmail);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var formCollection = await Request.ReadFormAsync();

                if (formCollection?.Files.Count != 0)
                {
                    var file = formCollection?.Files.First();

                    if (file?.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string base64 = Convert.ToBase64String(fileBytes);

                            // Update user's profile picture if provided
                            if (!string.IsNullOrWhiteSpace(base64))
                            //if (!string.IsNullOrWhiteSpace(model.profilePicture))
                            {
                                user.profilePicture = base64;
                                //user.profilePicture = model.profilePicture;
                            }

                            var updateResult = await _userManager.UpdateAsync(user);

                            if (!updateResult.Succeeded)
                            {
                                return StatusCode(500, "Failed to update user details");
                            }
                        }
                    }
                }

                /*                // Update user's email if provided
                                if (!string.IsNullOrWhiteSpace(formData["email"]) && formData["email"] != user.Email)
                                {
                                    var newUser = await _userManager.FindByEmailAsync(formData["email"]);
                                    if (newUser == null)
                                    {
                                        user.Email = formData["email"];
                                        user.UserName = formData["email"];

                                        var emailResult = await _userManager.UpdateAsync(user);

                                        if (!emailResult.Succeeded)
                                        {
                                            return StatusCode(500, "Failed to update email");
                                        }
                                    }
                                    else
                                    {
                                        return StatusCode(StatusCodes.Status409Conflict, newUser.UserName + " already exists");
                                    }

                                }

                                // Update user's password if provided
                                if (!string.IsNullOrWhiteSpace(formData["newPassword"]))
                                {
                                    if (formData["confirmPassword"] != formData["newPassword"])
                                    {
                                        var passwordResult = await _userManager.ChangePasswordAsync(user, formData["newPassword"], formData["newPassword"]);

                                        if (!passwordResult.Succeeded)
                                        {
                                            return StatusCode(500, "Failed to update password");
                                        }
                                    }
                                    else
                                    {
                                        return StatusCode(500, "New password should not match the old password");
                                    }
                                }

                                // Update user's role if provided
                                if (!string.IsNullOrWhiteSpace(formData["role"]))
                                {
                                    var existingRoles = await _userManager.GetRolesAsync(user);
                                    var roleResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);

                                    if (!roleResult.Succeeded)
                                    {
                                        return StatusCode(500, "Failed to remove existing roles");
                                    }*/

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        private async Task<User> GetUserByIdOrEmail(string userIdOrEmail)
        {
            User user;

            if (Guid.TryParse(userIdOrEmail, out var userId))
            {
                user = await _userManager.FindByIdAsync(userId.ToString());
            }
            else
            {
                user = await _userManager.FindByEmailAsync(userIdOrEmail);
            }

            return user;
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgetPasswordViewModal model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.email);
                if (user == null)
                {
                    return BadRequest("User does not exist or email is incorrect.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Token generation failed.");
                }
                var encodedToken = HttpUtility.UrlEncode(token); // Encode before sending in the URL

                MailData mailData = new()
                {
                    toEmailAddress = model.email,
                    subject = "Stockable Reset Password",

                    //using route paramters
                    //messageBody = $"Please reset your password by <a href=\"http://localhost:4200/reset-password/{user.Id}/{encodedToken}\">clicking here</a>.",

                    //using query paramters
                    messageBody = $"Please reset your password by <a href=\"http://localhost:4200/reset-password?userId={user.Id}&token={encodedToken}\">clicking here</a>.\r\n",
                };

                var httpClient = new HttpClient();
                var response = await httpClient.PostAsJsonAsync("http://localhost:58886/api/Mail/SendMail", mailData);

                if (response.IsSuccessStatusCode)
                {
                    return Ok("Password reset email sent successfully.");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to send the password reset email.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong: " + e);
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            //returns are all in different formats but im tired and did this at 1am
            try
            {
                var user = await _userManager.FindByIdAsync(model.userId);

                if (user == null)
                {
                    return NotFound("User does not exist or cannot be found");
                }
                else
                {
                    if (model.newPassword != model.confirmPassword)
                    {
                        return StatusCode(500, "Passwords do not match");
                    }

                    // Compare current password with the password stored in the database
                    var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, model.newPassword);
                    if (isCurrentPasswordValid)
                    {
                        return StatusCode(500, "New password cannot be the same as old password");
                    }

                    // Decode before validation but decoding causes errors as it changes the sysmbols in the token which cause a mismatch
                    //var decodedToken = HttpUtility.UrlDecode(model.token); 
                    //var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.newPassword);

                    var result = await _userManager.ResetPasswordAsync(user, model.token, model.newPassword);

                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        return StatusCode(StatusCodes.Status500InternalServerError, "Password could not be reset: " + errors);
                    }

                    return Ok("Password reset succesfully");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong: " + e.Message);
            }
        }

        [HttpGet]
        private ActionResult GenerateJWTToken(User user)
        {
            // Get user roles
            var roles = _userManager.GetRolesAsync(user).Result;

            // Create JWT Token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(3)
            );

            return Created("", new
            {
                id = user.Id,
                user = user.UserName,
                firstName = user.userFirstName,
                lastName = user.userLastName,
                type = user.userType,
                phoneNumber = user.PhoneNumber,
                profilePicture = user.profilePicture,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = roles
            });
        }
    }
}