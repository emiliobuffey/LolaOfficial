using System;
using LolaOfficial.Server.Models;
using LolaOfficial.Shared;


namespace Server.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserContext _userContext;

//constructor
        public UserController(UserContext userContext){
            _userContext = userContext;
        }
//##############################################################################################

// ADMIN METHODS

//##############################################################################################
// get all users
// retrieves the information from the server. Parameters will be appended in the query string. 
        [HttpGet]
        public ActionResult<List<User>> GetAction()
        {
            return _userContext.Users.ToList();
        }

//##############################################################################################
//get a single user
        [HttpGet("{id}")] // parameter
        public ActionResult<User> GetActionById(int id)
        {
            // find user by id if user.id == to id
            return _userContext.Users.FirstOrDefault(user => user.Id == id);
        }
//##############################################################################################
// creates a new resource (user)

        [HttpPost("")]
        public ActionResult<User> PostAction(User user)
        {
            // adding new user to the user object
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
            return user;
        }
//##############################################################################################
// update an existing resource (user)

        [HttpPut("{id}")]
        public ActionResult<User> PutAction(int id, User user)
        {
            User newUser = _userContext.Users.FirstOrDefault(user => user.Id == id);
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            _userContext.SaveChanges();
            return newUser;
        }
//##############################################################################################
// delete user

        [HttpDelete("{id}")]
        public void DeleteAction(int id) 
        {
            User oldUser = _userContext.Users.FirstOrDefault(user => user.Id == id);
            _userContext.Users.Remove(oldUser);
            _userContext.SaveChanges();
        }
//##############################################################################################

//AUTHENTICATION METHODS

//##############################################################################################
//User login, used on client side in pages-login.razor when login button is hit

        [HttpPost("loginuser")] //parameter
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            // checks if user is valid
            User loggedInUser = await _userContext.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();


            if (loggedInUser != null)
            {
                //create a claim
                var claim = new Claim(ClaimTypes.Name, loggedInUser.Email);    

                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
                //create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //Sign In User
                await HttpContext.SignInAsync(claimsPrincipal);
            }
            else
            {
                                return BadRequest();
            }
            return await Task.FromResult(loggedInUser);
        }
//##############################################################################################
//get user

        [HttpGet("getcurrentuser")] 
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new User();

            if (User.Identity.IsAuthenticated)
            {
                currentUser.Email = User.FindFirstValue(ClaimTypes.Name);
            }

            return await Task.FromResult(currentUser);
        }
        
//##############################################################################################
//Logout user

        [HttpGet("logoutuser")]
        public async Task<ActionResult<String>> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return "Success";
        } 
    }
}