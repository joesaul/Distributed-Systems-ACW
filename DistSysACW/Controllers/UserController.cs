using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DistSysACW.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Newtonsoft.Json.Linq;

namespace DistSysACW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(Models.UserContext context) : base(context)
        {

        }

        
        [HttpGet]
        [ActionName("New")]
        public IActionResult checkUserGet([FromQuery] string username)
        {  
            using (var ctx = new UserContext())
            {
                try
                {
                    
                    if (UserDatabaseAccess.CheckUserByUsername(username) == true)
                    {
                        return Ok("True - User Does Exist! Did you mean to do a POST to create a new user? ");
                    }

                    else
                    {
                        return Ok("False - User Does Not Exist! Did you mean to do a POST to create a new user? ");
                    }


                }
                catch (DBConcurrencyException)
                {
                    return Ok("False - User Does Not Exist! Did you mean to do a POST to create a new user? ");
                }

            }
        }
        
        [HttpPost]
        [ActionName("New")]
        public IActionResult addUserPost([FromBody] string username)
        {
            
            username.Trim('"');
            if (UserDatabaseAccess.CheckUserByUsername(username) == true)
            {
                 return StatusCode(403, "Oops.This username is already in use. Please try again with a new username.");
            }
            if (username == "")
            {
                return BadRequest("Oops.Make sure your body contains a string with your username and your Content - Type is Content - Type:application / json");
            }
            User user = UserDatabaseAccess.addUserPost(username);
            
            return Ok(user.ApiKey);
            

        }
        [Authorize(Roles = "Admin,User")]
        [HttpDelete]
        [ActionName("RemoveUser")]
        public IActionResult removeUserDelete([FromQuery] string username, [FromHeader] string ApiKey)
        {
            if (UserDatabaseAccess.checkUserByAPIKey(ApiKey)==true)
            {
                if(UserDatabaseAccess.checkUserByAPIKeyAndUserName(ApiKey, username)==true)
                {
                    UserDatabaseAccess.deleteUserByUserNameAndAPIKey(ApiKey, username);
                    return Ok(true); 
                }
                
                return Ok(false);
            }
            
            return Ok(false);

        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("ChangeRole")]
        public IActionResult changeRole([FromBody] User user)
        {
            int code = 400;
            string message = UserDatabaseAccess.changeRole(user);
            if(message == "DONE")
            {
                code = 200;
            }
            return StatusCode(code, message);
        }
        

        
    }
}
