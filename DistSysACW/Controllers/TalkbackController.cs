using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using DistSysACW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistSysACW.Controllers
{
    public class TalkBackController : BaseController
    {
        /// <summary>
        /// Constructs a TalkBack controller, taking the UserContext through dependency injection
        /// </summary>
        /// <param name="context">DbContext set as a service in Startup.cs and dependency injected</param>
        public TalkBackController(Models.UserContext context) : base(context) 
        {
            /*
            UserDatabaseAccess.addUser("joe", "user");
            UserDatabaseAccess.deleteUserByUserNameAndAPIKey("3aef3d55-4fe7-4b82-b124-c3f56722519d", "joe");
            UserDatabaseAccess.deleteUserByAPIKey("50b3f403-2910-4dbe-9b32-fba5d8b63ae7");
            UserDatabaseAccess.checkUserByAPIKeyAndUserName("4df2f1d8-31ef-4a63-b426-ff2300fa022d", "joe");
            UserDatabaseAccess.checkUserByAPIKey("4df2f1d8-31ef-4a63-b426-ff2300fa022d");
            UserDatabaseAccess.deleteUserByUsername("joe");
            UserDatabaseAccess.displayUserObject("4df2f1d8-31ef-4a63-b426-ff2300fa022ds");
            */
            

        }


        [ActionName("Hello")]
        public string Get()
        {
            #region TASK1
            // TODO: add api/talkback/hello response
            #endregion
            return ("Hello World");
        }

        [ActionName("Sort")]
        public IActionResult Get([FromQuery]int[] integers)
        {
            #region TASK1
            // TODO: 
            // sort the integers into ascending order
            // send the integers back as the api/talkback/sort response
            #endregion
            try
            {
                Array.Sort(integers);
                return Ok(integers);
            }
            catch (Exception)
            {
               return BadRequest("BAD REQUEST");
            }
            
        }

    }
}
