using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Data;

namespace DistSysACW.Models
{
    public class User
    {

        #region Task2
        // TODO: Create a User Class for use with Entity Framework
        // Note that you can use the [key] attribute to set your ApiKey Guid as the primary key 
        #endregion
        [Key]
        public string ApiKey { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public User() { }

    }


    #region Task13?
    // TODO: You may find it useful to add code here for Logging
    #endregion

    public class UserDatabaseAccess
    {

        #region Task3 
        // TODO: Make methods which allow us to read from/write to the database 
        #endregion


        //
        public static string addUser(string user, string role)
        {
            using (var ctx = new UserContext())
            {
                User newuser = new User()
                {
                    UserName = user,
                    Role = role
                };
                ctx.Users.Add(newuser);
                ctx.SaveChanges();
                return "API key is " + newuser.ApiKey;
            }
        }
        //Check if a user with a given ApiKey string exists in the database, returning true or false.
        public static bool checkUserByAPIKey(string apikey)
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    var user = ctx.Users.FirstOrDefault(x => x.ApiKey == apikey);
                    if (user != null)
                    {
                        
                        return true;

                    }
                    
                    return false;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }


        //Check if a user with a given ApiKey string exists in the database, returning the User object.
        public static string displayUserObject(string apikey)  
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    var user = ctx.Users.FirstOrDefault(x => x.ApiKey == apikey);
                    
                    if (user != null )
                    {
                        
                        return "This user exists - " + apikey + ", " + user.UserName;

                    }
                       
                        return "This user does not exist";


                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    return "This user does not exist";
                }


        }
        //Check if a user with a given ApiKey and UserName exists in the database, returning true or false.
        public static bool checkUserByAPIKeyAndUserName(string apikey, string username)
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    User user = new User { ApiKey = apikey, UserName = username };
                    var temp = ctx.Users.Find(user.ApiKey);
                    if ((temp.ApiKey == apikey) && (temp.UserName == username))
                    {
                        return true;
                    }
                    else { return false; }

                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }
        //Delete a user with a given ApiKey and Username from the database.
        public static void deleteUserByUserNameAndAPIKey(string apikey, string username)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var user = new User { ApiKey = apikey, UserName = username };
                    ctx.Users.Attach(user);
                    ctx.Users.Remove(user);
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                }

            }
        }
        //Delete a user with a given ApiKey from the database.
        public static void deleteUserByAPIKey(string apikey)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var user = new User { ApiKey = apikey };
                    ctx.Users.Attach(user);
                    ctx.Users.Remove(user);
                    ctx.SaveChanges();
                }

                catch (Exception)
                {

                }
            }

        }
        //Delete a user with a given username from the database.
        public static void deleteUserByUsername(string username)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var user = new User { UserName = username };
                    ctx.Users.Attach(user);
                    ctx.Users.Remove(user);
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static User addUserPost(string username)
        {
            

            using (var ctx = new UserContext())
            {
                
                var users = new User { UserName = username };
                    if (ctx.Users.Count() == 0)
                    {
                        users.Role = "Admin";
                        ctx.Users.Add(users);

                    }
                    else
                    {
                        users.Role = "User";
                        ctx.Users.Add(users);


                    }
                
                    ctx.SaveChanges();
                    return users;
                
               
                

            }
        }
        
        public static bool CheckUserByUsername(string username)
        {
            
           try
           {
                using (var ctx = new UserContext())
                {
                    var user = ctx.Users.FirstOrDefault(x => x.UserName == username);
                    if (user != null)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }


           }
           catch (DBConcurrencyException)
           {
               return false;
           }

            


        }
        
        public static string changeRole(User user)
        {
            
            using(var ctx = new UserContext())
            {
                if(user.Role != "Admin" && user.Role != "User")
                {
                    return "NOT DONE: Role does not exist";
                }
                try
                {
                    User users = ctx.Users.SingleOrDefault(u => u.UserName == user.UserName);
                    if(users == null)
                    {
                        
                        return "NOT DONE: Username does not exist";
                    }
                    users.Role = user.Role;
                    ctx.Users.Update(users);
                    ctx.SaveChanges();
                    return "DONE";

                }
                catch (Exception)
                {
                    return "NOT DONE: An error occured";
                }
            }
        }
        
        

                   
    

        
    }
    
}
        
            
        
    




