using System;
using LolaOfficial.Server.Models;
using LolaOfficial.Shared;


namespace Server.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserContext _userContext;

        //constructor
        public UserController(UserContext userContext){
            _userContext = userContext;
        }

        // get all users
        // retrieves the information from the server. Parameters will be appended in the query string. 
        [HttpGet]
        public ActionResult<List<User>> GetAction()
        {
            return _userContext.Users.ToList();
        }

        //get a single user
        [HttpPost("{id}")]
        public ActionResult<User> GetActionById(int id)
        {
            // find user by id if user.id == to id
            return _userContext.Users.FirstOrDefault(user => user.Id == id);
        }

        // creates a new resource (user)
        [HttpPost("")]
        public ActionResult<User> PostAction(User user)
        {
            // adding new user to the user object
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
            return user;
        }

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

        // delete user
        [HttpDelete("{id}")]
        public void DeleteAction(int id) 
        {
            User oldUser = _userContext.Users.FirstOrDefault(user => user.Id == id);
            _userContext.Users.Remove(oldUser);
        }
    }
}