using InventorySystem.Api.Models;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Enums;
using InventorySystem.Infrastructure.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InventorySystem.Api.Controllers
{
    public class AuthenticationController : ApiController
    {
        public UserManager<ApplicationUser> _UserManager => HttpContext.Current.GetOwinContext().Get<UserManager<ApplicationUser>>();
        public SignInManager<ApplicationUser, string> _SignInManager => HttpContext.Current.GetOwinContext().Get<SignInManager<ApplicationUser, string>>();

        [Route("api/Authenticate/SignUpUser")]
        [HttpPost]
        public async Task<IHttpActionResult> SignUpCustomer(UserModel model)
        {
            if (model == null)
            {
                return BadRequest("model cannot be null");
            }

            if (model.Password == null)
            {
                return BadRequest("Please input password");
            }

            //check if user exist
            var user = await _UserManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                return BadRequest("Your account with this email already exist");
            }
            var applicant = new ApplicationUser();
            applicant.FirstName = model.FirstName;
            applicant.Lastname = model.LastName;
            applicant.RegistrationDate = DateTime.Now;
            applicant.CompanyName = model.CompanyName;
            applicant.Email = model.Email;
            applicant.UserName = model.Email;
            applicant.PhoneNumber = model.PhoneNumber;
            //create user with password 
            Trace.TraceInformation("creating user");
            var createUser = await _UserManager.CreateAsync(applicant, model.Password);
            List<string> ErrorList = new List<string>();
            try
            {
                if (createUser.Succeeded)
                {
                    //add active status for user
                    applicant.status = Status.Active.ToString();
                    applicant.EmailConfirmed = true;
                    Trace.TraceInformation(applicant.UserName + "" + applicant.Email);
                    //add user to a User role
                    _UserManager.AddToRole(applicant.Id, "Customer");
                    var result = _UserManager.Update(applicant);

                    
                    return Ok(applicant.Id);
                }
                else
                {
                    foreach (var error in createUser.Errors)
                    {
                        ErrorList.Add(error);
                    }
                    Trace.TraceError(JsonConvert.SerializeObject(ErrorList));
                    return BadRequest(JsonConvert.SerializeObject(ErrorList));
                }

            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Authenticate/VerifyEmail")]
        public IHttpActionResult VerifyEmail(string userId)
        {
            try
            {
                AppDbContext db = new AppDbContext();
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    //update EmailConfirmed status to true if user exist
                    user.EmailConfirmed = true;

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return Ok(true);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Authenticate/ResendEmailVerificationLink")]
        public IHttpActionResult ResendEmailVerificationLink(string userId)
        {
            try
            {
                AppDbContext db = new AppDbContext();
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    new MessageController().SendActivationMail(user.Email, "Email Verification", user.Id);
                    Trace.TraceInformation(user.Email, " ", userId);
                    return Ok(true);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Authenticate/SignInUser")]
        [HttpPost]
        public async Task<IHttpActionResult> SignInCustomer(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("model cannot be null");
            }
            try
            {
                var user = await _UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest("invalid credentials");
                }
                if (!await _UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    return BadRequest("User has not confirmed Email");
                }

                var signInStatus = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (signInStatus == SignInStatus.Success && user.status == Status.Active.ToString())
                {
                    LoginResponseModel resp = new LoginResponseModel();
                    resp.Fullname = user.FirstName + " " + user.Lastname;
                    resp.Email = user.Email;
                    IList<string> Roles = _UserManager.GetRoles(user.Id);
                    foreach (var role in Roles)
                    {
                        if (role == "Customer")
                        {
                            resp.Role = role;
                        }

                    }
                    resp.PhoneNo = user.PhoneNumber;
                    resp.UserId = user.Id;
                    Trace.TraceInformation(user.UserName + "" + user.Email + "sigin was successful");
                    return Ok(resp);
                }
                else
                {
                    return BadRequest("Invalid credential");
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                Trace.TraceError(ex.Message);
                return BadRequest("Invalid credentials");
            }
        }
        /// <summary>
        /// this api is called when customer forges his password
        /// </summary>
        [Route("api/Authenticate/ForgottenPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgottenPassword(ForgottenPasswordModel model)
        {
            try
            {
                var user = await _UserManager.FindByNameAsync(model.Email);

                if (user != null)
                {
                    new AuthenticationController().ResendPasswordLink(user.Id);
                    return Ok(user.Id);
                }
                else
                {
                    return BadRequest("User does not exist, please Sign up");
                }
            }

            catch (Exception msg)
            {
                return InternalServerError(msg.InnerException);
            }

        }

        [Route("api/Authenticate/ValidatePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ValidatePassword(PasswordResetModel model)
        {
            if (model == null || model.Password == null || model.UserId == null || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Invalid credentials");
            }
            try
            {

                var user = await _UserManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.PasswordHash = _UserManager.PasswordHasher.HashPassword(model.Password);
                    user.EmailConfirmed = true;
                    var result = await _UserManager.UpdateAsync(user);
                    return Ok(true);
                }
                else
                {
                    return BadRequest("User cannot be found");
                }
            }
            catch (Exception msg)
            {
                return InternalServerError(msg.InnerException);
            }
        }


        /// <summary>
        /// This method is to re-send Password Verification Link to Customer if customer dont get the password initially
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Authenticate/ResendPasswordLink")]
        public IHttpActionResult ResendPasswordLink(string userId)
        {
            try
            {
                AppDbContext db = new AppDbContext();
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    new MessageController().SendPasswordLink(user.Email, "Password Verification", user.Id);
                    Trace.TraceInformation(user.Email, " ", userId);
                    return Ok(true);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/BackOffice/AdminLogin")]
        public async Task<IHttpActionResult> Login(AdminLoginModel model)
        {
            AppDbContext db = new AppDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            userManager.UserLockoutEnabledByDefault = true;
            userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromDays(999999);
            userManager.MaxFailedAccessAttemptsBeforeLockout = 3;
            // allow alphanumeric characters in username

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            //var user = await userManager.FindByNameAsync(model.UserName);
            // find user by username
            var user = await userManager.FindByNameAsync(model.username);
            string message;

            if (user != null)
            {
                var validCredentials = await userManager.FindAsync(user.UserName, model.password);
                // When a user is lockedout, this check is done to ensure that even if the credentials are valid
                // the user can not login until the lockout duration has passed
                if (await userManager.IsLockedOutAsync(user.Id))
                {
                    message = "Your account has been locked. Please contact your super administrator";
                    return BadRequest(message);
                }
                // if user is subject to lockouts and the credentials are invalid
                // record the failure and check if user is lockedout and display message, otherwise,
                // display the number of attempts remaining before lockout
                else if (await userManager.GetLockoutEnabledAsync(user.Id) && validCredentials == null)
                {
                    // Record the failure which also may cause the user to be locked out
                    await userManager.AccessFailedAsync(user.Id);

                    if (userManager.IsLockedOut(user.Id))
                    {
                        message = string.Format("Your account has been locked out due to multiple failed login attempts. Please contact your system administrator");
                        return BadRequest(message);
                    }
                    else
                    {
                        int accessFailedCount = userManager.GetAccessFailedCount(user.Id);
                        int attemptsLeft = 3 - accessFailedCount;
                        message = string.Format("Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);
                        return BadRequest(message);
                    }
                }
                else if (validCredentials == null)
                {
                    message = "Invalid login credentials. Please try again";
                    return BadRequest(message);
                }
                //else if (!user.EmailConfirmed)
                //{
                //    message = "Email has not been verified";
                //    return BadRequest(message);
                //}
                else if (user.status == Status.InActive.ToString())
                {
                    message = "Inactive user profile. Contact Admin";
                    return BadRequest(message);
                }
                else
                {
                    //update last login date
                    user.LastLoginTime = DateTime.Now;
                    //await ctx.SaveChangesAsync();
                    await userManager.ResetAccessFailedCountAsync(user.Id);
                    db.SaveChanges();

                    //get the role
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                    userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    IdentityRole theRole = null;
                    var theUserRoles = userManager.GetRoles(user.Id);
                    foreach (var role in theUserRoles)
                    {
                        if ( (role == "Manager") || (role == "User") )
                        {
                            Trace.TraceInformation(role);
                            theRole = roleManager.FindByName(theUserRoles.FirstOrDefault());

                            var usermodel = new LoginResponseModel()
                            {
                                Fullname = user.Name,
                                Email = user.Email,
                                PhoneNo = user.PhoneNumber,
                                Role = theRole.Name,
                                UserId = user.Id
                            };
                            return Ok(usermodel);
                        }
                        else
                        {
                            return NotFound();
                        }

                    }

                    return BadRequest();

                }
            }
            else
            {
                // user authentication failed
                message = "Invalid credentials. Please try again.";
                return BadRequest(message);
            }
        }

        [HttpPost]
        [Route("api/BackOffice/CreateUser")]
        public IHttpActionResult CreateAdmin(CheckerModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.SuperAdminId) || string.IsNullOrEmpty(model.UserRole))
            {
                return BadRequest("model, Manager , id or UserRole cannot be null");
            }

            AppDbContext db = new AppDbContext();

            //check if admin is making the request
            var admin = db.Users.FirstOrDefault(x => x.Id == model.SuperAdminId);
            IList<string> Roles = _UserManager.GetRoles(admin.Id);
            var role = Roles.FirstOrDefault(x => x == "Manager");
            if (role == "Manager")
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.Lastname = model.LastName;
                user.UserName = model.Email;
                user.Name = model.FirstName + " " + model.LastName;
                user.RegistrationDate = DateTime.Now;

                var createUser = _UserManager.Create(user, model.Password);
                if (createUser.Succeeded)
                {
                    //add active status for user
                    user.status = Status.Active.ToString();
                    //add user to User role
                    _UserManager.AddToRole(user.Id, model.UserRole);
                    //update the user entity ton account for the newly created role
                    _UserManager.Update(user);
                    Trace.TraceInformation(user.UserName + "" + user.Email);
                    return Ok(user.Id);
                }
                else
                {
                    List<string> ErrorList = new List<string>();
                    foreach (var error in createUser.Errors)
                    {
                        ErrorList.Add(error);
                    }
                    Trace.TraceError(JsonConvert.SerializeObject(ErrorList));
                    return BadRequest("A user already exists with this email");
                }
            }
            else
            {
                return BadRequest("User is not Authorized to perform this request");
            }

        }



        [HttpPost]
        [Route("api/BackOffice/DeleteUser")]
        public IHttpActionResult DeleteMakerOrChecker(RemoveCheckerOrMaker model)
        {
            if (model == null || string.IsNullOrEmpty(model.SuperAdminId))
            {
                return BadRequest("model or super Admin Id cannot be null");
            }

            AppDbContext db = new AppDbContext();
            var admin = db.Users.FirstOrDefault(x => x.Id == model.SuperAdminId);
            IList<string> Roles = _UserManager.GetRoles(admin.Id);
            var role = Roles.FirstOrDefault(x => x == "Manager");
            if (role == "Manager")
            {
                //verifiy that email is for a maker or checker
                var makerOrChecker = db.Users.FirstOrDefault(x => x.Email == model.Email);
                if (makerOrChecker != null)
                {
                    var roleObj = _UserManager.GetRoles(makerOrChecker.Id);
                    var makeCheckerRole = roleObj.FirstOrDefault(x => x == "User");
                    if (makeCheckerRole == "User")
                    {
                        //delete maker checker 
                        db.Users.Remove(makerOrChecker);
                        db.SaveChanges();
                        return Ok("deleted succesfully");
                    }
                    else
                    {
                        return BadRequest("user role is not maker or checker");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("User is not Authorized to perform this request");
            }
        }

        [HttpPost]
        [Route("api/BackOffice/UpdateUser")]
        public IHttpActionResult UpdateMakerOrChecker(EditMakerChecker model)
        {
            if (model == null || model.SuperAdminId == null)
            {
                return BadRequest("model or super admin id cannot be null");
            }
            //confirm ID is super admin
            AppDbContext db = new AppDbContext();
            var admin = db.Users.FirstOrDefault(x => x.Id == model.SuperAdminId);
            IList<string> Roles = _UserManager.GetRoles(admin.Id);
            var role = Roles.FirstOrDefault(x => x == "Manager");
            if (role == "Manager")
            {
                //verifiy that email is for a maker or checker
                var makerOrChecker = db.Users.FirstOrDefault(x => x.Id == model.MakerCheckerId);
                if (makerOrChecker != null)
                {
                    var roleObj = _UserManager.GetRoles(makerOrChecker.Id);
                    var makeCheckerRole = roleObj.FirstOrDefault(x => x == "User");
                    if (makeCheckerRole == "User")
                    {
                        //remove old role..
                        _UserManager.RemoveFromRole(makerOrChecker.Id, makeCheckerRole);
                        //add new Role..
                        _UserManager.AddToRole(makerOrChecker.Id, model.UserRole);
                        makerOrChecker.Email = model.Email;
                        makerOrChecker.FirstName = model.FirstName;
                        makerOrChecker.Lastname = model.LastName;
                        makerOrChecker.UserName = model.Email;
                        makerOrChecker.Name = model.FirstName + " " + model.LastName;
                        makerOrChecker.PasswordHash = _UserManager.PasswordHasher.HashPassword(model.Password);
                        makerOrChecker.status = Status.Active.ToString();
                        makerOrChecker.DateAdminUpdated = DateTime.Now;

                        db.Entry(makerOrChecker).State = EntityState.Modified;
                        db.SaveChanges();

                        return Ok("Updated successfully");
                    }
                    else
                    {
                        return BadRequest("user role is not maker or checker");
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return BadRequest("User is not Authorized to perform this request");
            }
        }

        [HttpPost]
        [Route("api/BackOffice/GetUsersByEmail")]
        public IHttpActionResult GetMakerOrCheckerByEmail(string Email)
        {
            var db = new AppDbContext();
            var makerOrChecker = db.Users.FirstOrDefault(x => x.Email == Email);
            if (makerOrChecker != null)
            {
                var roleObj = _UserManager.GetRoles(makerOrChecker.Id);
                var makeCheckerRole = roleObj.FirstOrDefault(x => x == "User");
                if (makeCheckerRole == "User")
                {
                    return Ok(makerOrChecker);
                }
                else
                {
                    return BadRequest("user is not a maker or checker");
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/BackOffice/GetAllAdmins")]
        public IHttpActionResult GetAllAdmins(string ManagerID)
        {
            if (string.IsNullOrEmpty(ManagerID))
            {
                return BadRequest("Super Admin id cannot be null");
            }
            try
            {
                AppDbContext db = new AppDbContext();
                var admin = db.Users.FirstOrDefault(x => x.Id == ManagerID);
                IList<string> Roles = _UserManager.GetRoles(admin.Id);
                var adrole = Roles.FirstOrDefault(x => x == "Manager");
                if (adrole == "Manager")
                {

                    var usersWithRoles = (from user in db.Users
                                          select new
                                          {
                                              DateCreated = user.RegistrationDate,
                                              DateUpdated = user.DateAdminUpdated,
                                              FirstName = user.FirstName,
                                              LastName = user.Lastname,
                                              UserId = user.Id,
                                              Username = user.UserName,
                                              Email = user.Email,
                                              RoleNames = (from userRole in user.Roles
                                                           join role in db.Roles on userRole.RoleId
                                                           equals role.Id
                                                           select role.Name).ToList()
                                          }).ToList().Select(p => new UsersWithRole()

                                          {
                                              DateCreated = p.DateCreated,
                                              DateUpdated = p.DateUpdated,
                                              FirstName = p.FirstName,
                                              LastName = p.LastName,
                                              UserId = p.UserId,
                                              Username = p.Username,
                                              Email = p.Email,
                                              Role = string.Join(",", p.RoleNames)
                                          });

                    var adminroles = usersWithRoles.Where(x => x.Role != "Customer");
                    return Ok(adminroles);
                }
                else
                {
                    return BadRequest("user is not authorized to perform this request");
                }

            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.Message);
                return BadRequest("error when retrieving roles");
            }

        }
    }
}
