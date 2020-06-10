using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DistSysACW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistSysACW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProtectedController : BaseController
    {
        public ProtectedController(Models.UserContext context) : base(context)
        {

        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [ActionName("sha1")]
        public IActionResult protectedSHA1([FromQuery] string message)
        {
            if (message != null)
            {

                byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(message);
                byte[] sha1ByteMessage;

                SHA1 sha1Provider = new SHA1CryptoServiceProvider();

                sha1ByteMessage = sha1Provider.ComputeHash(asciiByteMessage);

                string ByteArrayToHexString(byte[] byteArray)
                {
                    string hexString = "";
                    if (null != byteArray)
                    {
                        foreach (byte b in byteArray)
                        {
                            hexString += b.ToString("x2");
                        }


                    }
                    
                    return hexString;
                }


                return Ok(ByteArrayToHexString(sha1ByteMessage));
            }
            return BadRequest("Bad Request");


        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [ActionName("sha256")]
        public IActionResult protectedSHA256([FromQuery] string message)
        {

            if (message != null)

            {


                byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(message);

                byte[] sha256ByteMessage;

                SHA256 sha256Provider = new SHA256CryptoServiceProvider();

                sha256ByteMessage = sha256Provider.ComputeHash(asciiByteMessage);
                string ByteArrayToHexString(byte[] byteArray)
                {
                    string hexString = "";
                    if (null != byteArray)
                    {
                        foreach (byte b in byteArray)
                        {
                            hexString += b.ToString("x2");
                        }


                    }

                    return hexString;
                }


                return Ok(ByteArrayToHexString(sha256ByteMessage));

            }
            return BadRequest("Bad Request");



        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [ActionName("hello")]
        public IActionResult protectedHello([FromHeader] string ApiKey)
        {
            using (var ctx = new UserContext())
            {
                try
                {

                    
                        var user = ctx.Users.FirstOrDefault(x => x.ApiKey == ApiKey);

                        if (user != null)
                        {

                            return Ok("Hello " + user.UserName);

                        }

                        return BadRequest();


                    


                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
               
            }
        }
    }
}
