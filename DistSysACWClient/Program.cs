using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DistSysACWClient
{
    #region Task 10 and beyond
    class Client
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            
                RunAsync().Wait();
                Console.ReadKey();
                

    }
        
        
        private static StringContent data;
        private static string input = "";
        private static string ApiKey = null;
        private static string username = "";

        static async Task RunAsync()
        {
            
           
            if (client.BaseAddress == null)
            {
                Console.WriteLine("--------Waiting for connection--------");
                client.BaseAddress = new Uri("https://localhost:5001/");
            }
            


            
            
            if(input == "")
            {
                Console.WriteLine("What method would you like?");
                Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                Console.WriteLine("-------------------------------------");
                input = Console.ReadLine();
            }
            
            
            string HttpMethodChoice = "";
            string[] x = input.Split(' ');
            Console.WriteLine("...please wait...");

            try
            {
                if (input == "TalkBack Hello")
                {
                    

                    try
                    {
                        Task<string> task = GetStringAsync("api/talkback/hello");
                        if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                        {
                            Console.WriteLine(task.Result);
                        }
                        else
                        {
                            Console.WriteLine("Request Timed Out");
                        }
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    RunAsync().Wait();


                }

                
                if (x[1] == "Sort" && x[0] == "TalkBack")
                {
                    
                    HttpMethodChoice = "talkback/sort";
                    char[] charsToTrim = { '[', ']' };
                    string cleanString = x[2].Trim(charsToTrim);
                    string[] integers = cleanString.Split(',');
                    for (int i = 0; i < integers.Length; i++)
                    {
                        if (i == 0)
                        {

                            HttpMethodChoice += "integers=" + integers[i];
                        }
                        HttpMethodChoice += "&integers=" + integers[i];
                    }
                    try
                    {
                        Task<string> task = GetStringAsync("api/talkback/sort?" + HttpMethodChoice);
                        if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                        {
                            Console.WriteLine(task.Result);
                        }
                        else
                        {
                            Console.WriteLine("Request Timed Out");
                        }
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    RunAsync().Wait();


                }
                
                if (x[1] == "Get" && x[0] == "User")
                {
                    HttpMethodChoice = "?username=" +x[2];
                    try
                    {
                        Task<string> task = GetStringAsync("api/user/new" + HttpMethodChoice);
                        if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                        {
                            Console.WriteLine(task.Result);
                        }
                        else
                        {
                            Console.WriteLine("Request Timed Out");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    RunAsync().Wait();
                }
                if (x[0] == "User" && x[1] == "Post")
                {

                    HttpMethodChoice = "User/New";
                    
                    username = x[2];
                    var jsonBody = JsonConvert.SerializeObject(username);
                    data = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    try
                    { 
                        Task<string> task = PostStringAsync("api/user/new", data);
                        if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                        {
                            ApiKey = task.Result;
                            Console.WriteLine(task.Result);
                            
                        }
                        else
                        {
                            Console.WriteLine("Request Timed Out");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    RunAsync().Wait();
                }
                if(x[0] == "User" && x[1] == "Delete")
                {
                    if(username == null || ApiKey == null)
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                    }
                    HttpMethodChoice = "/api/user/removeuser?username="+username;
                    try
                    {
                        Task<string> task = DeleteStringAsync(HttpMethodChoice);
                        if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                        {
                            
                            Console.WriteLine(task.Result);
                        }
                        else
                        {
                            Console.WriteLine("Request Timed Out");
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    client.DefaultRequestHeaders.Remove("ApiKey");
                    ApiKey = null;
                    RunAsync().Wait();
                }
                if(x[0] == "User" && x[1] == "Set")
                {
                    try
                    {
                        username = x[2];
                        ApiKey = x[3];
                        Console.WriteLine("Stored");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    RunAsync().Wait();
                }
               
                if (x[0] == "Protected" && x[1] == "Hello")
                {
                    try
                    {
                        if (ApiKey == null)
                        {
                            Console.WriteLine("You need to do a User Post or User Set first");
                        }
                        else
                        {
                            try
                            {
                                client.DefaultRequestHeaders.Add("ApiKey", ApiKey);
                                HttpMethodChoice = "api/protected/hello";
                                Task<string> task = GetStringAsync(HttpMethodChoice);
                                if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                                {
                                    Console.WriteLine(task.Result);
                                }
                                else
                                {
                                    Console.WriteLine("Request Timed Out");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.GetBaseException().Message);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().Message);
                    }
                   
                    
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    client.DefaultRequestHeaders.Remove("ApiKey");
                    RunAsync().Wait();
                    
                }
                if (x[0] == "Protected" && x[1] == "SHA1")
                {
                    try
                    {
                        if (ApiKey == null)
                        {
                            Console.WriteLine("You need to do a User Post or User Set first");
                        }
                        else
                        {
                            try
                            {
                                client.DefaultRequestHeaders.Add("ApiKey", ApiKey);
                                HttpMethodChoice = "api/protected/sha1?message=" + x[2];
                                Task<string> task = GetStringAsync(HttpMethodChoice);
                                if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                                {
                                    Console.WriteLine(task.Result);
                                }
                                else
                                {
                                    Console.WriteLine("Request Timed Out");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.GetBaseException().Message);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    

                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    client.DefaultRequestHeaders.Remove("ApiKey");
                    RunAsync().Wait();

                }
                if (x[0] == "Protected" && x[1] == "SHA256")
                {
                    try
                    {
                        if (ApiKey == null)
                        {
                            Console.WriteLine("You need to do a User Post or User Set first");
                        }
                        else
                        {
                            try
                            {
                                client.DefaultRequestHeaders.Add("ApiKey", ApiKey);
                                HttpMethodChoice = "api/protected/sha256?message=" + x[2];
                                Task<string> task = GetStringAsync(HttpMethodChoice);
                                if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                                {
                                    Console.WriteLine(task.Result);
                                }
                                else
                                {
                                    Console.WriteLine("Request Timed Out");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.GetBaseException().Message);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().Message);
                    }
                    

                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine("• TalkBack Hello \r\n" +
                              "• TalkBack Sort \r\n" +
                              "• User Get (Name) \r\n" +
                              "• User Post (Name) \r\n" +
                              "• User Set (Name) (ApiKey) \r\n" +
                              "• User Delete \r\n" +
                              "• Protected Hello \r\n" +
                              "• Protected SHA1 (string) \r\n" +
                              "• Protected SHA256 (string) ");
                    Console.WriteLine("-------------------------------------");
                    input = Console.ReadLine();
                    if (input == "Exit")
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    client.DefaultRequestHeaders.Remove("ApiKey");
                    RunAsync().Wait();

                }
                Console.Clear();
                Console.WriteLine("Enter valid input");
                input = "";
                RunAsync().Wait();
            }
            
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Enter valid input");
                input = "";
                RunAsync().Wait();
            }






        }
        static async Task<string> GetStringAsync(string path)
        {
            string responseString = "";
            HttpResponseMessage responseMessage = await client.GetAsync(path);
            responseString = await responseMessage.Content.ReadAsStringAsync();
            return responseString;
        }
        static async Task<string> PostStringAsync(string path, StringContent data)
        {
            string responseString;
            HttpResponseMessage responseMessage = await client.PostAsync(path, data);
            responseString = await responseMessage.Content.ReadAsStringAsync();
            return responseString;
        }
        static async Task<string> DeleteStringAsync(string path)
        {
            string responseString;
            client.DefaultRequestHeaders.Add("ApiKey", ApiKey);
            HttpResponseMessage responseMessage = await client.DeleteAsync(path);
            responseString = await responseMessage.Content.ReadAsStringAsync();
            return responseString;
        }


    }



        
}
    #endregion

