using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MicroserviceAPI.Models;
using MicroserviceAPI.Model;

namespace MicroserviceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        IApplicationRepository ApplicationRepository;
        private readonly ApplicationDBContext _context;

        // To  Consume Buka Sms API
        private string BASE_URL = "https://api.onbuka.com/";
        public CustomerController(ApplicationDBContext context, IApplicationRepository TBank)
        {
            _context = context;

            ApplicationRepository = TBank;
        }

        // Onboard a customer: The endpoint should take phone Number, email, password, state of residence, and LGA.
        [HttpPost]
        [Route("Api/SMS")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]
        public JsonResult SMS()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            var values = new List<KeyValuePair<string, string>>();
            var OTPCode = GenerateNumber();
            values.Add(new KeyValuePair<string, string>("number", "08134814044"));
            values.Add(new KeyValuePair<string, string>("senderid", "kazeem"));
            values.Add(new KeyValuePair<string, string>("OTPCode", OTPCode));
            var content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync("v3/sendSms", content).Result;
            var result = "";
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;

            }

            var newjsonResult = new JsonResult(result);
            newjsonResult.StatusCode = 200;
            newjsonResult.Value = 20;
            return newjsonResult;
        }

        // verified via OTP before onboarding is said to be completed
        [HttpPut]
        [Route("Api/SMS/{Id}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]
        public JsonResult UpdateSMS(int Id, [FromBody] Customer Customer)
        {
           
            if (Id != 0)
            {
                return new JsonResult("Invalid record");
            }

            var customer = ApplicationRepository.Customers.FirstOrDefault(e => e.CustomerId == Id);
            if (customer == null)
            {
                return new JsonResult("Invalid record");
            }

            Customer.PhoneNo = Customer.PhoneNo;
            Customer.EmailAddress = Customer.EmailAddress;
            Customer.Password = Customer.Password;
            Customer.State = Customer.State;
            Customer.Lga = Customer.Lga;
            Customer.OTPCode = Customer.OTPCode;
            ApplicationRepository.AddCustomer(customer);
            
            
            return new JsonResult("Successfull");
        }

        private JsonResult BadRequest(object p)
        {
            throw new NotImplementedException();
        }

        //GET : Customer/Controller
        //   Endpoint to return all existing customers previously onboarded
        [HttpGet]
        [Route("/Api/AllCustomers")]
        [ProducesResponseType(200, Type = typeof(List<Customer>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]
       
        public JsonResult GetAllCustomers()
        {
            return new JsonResult (ApplicationRepository.Customers.ToListAsync());
          //  return null;
        }

        //   Endpoint to return customer by Id
        [HttpGet]
        [Route("Api/Customer/{Id}")]

        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]
        public  IEnumerable<Customer> Get(long Id)
       // public JsonResult GetCustomer(long Id)
        {
            try
            {
                var customer = ApplicationRepository.Customers.Where(x => x.CustomerId.Equals(Id));

                if (customer == null)
                {

                    return null;
                }

                return (customer);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Get:  All States
        [HttpGet]
        [Route("/api/Customer/GetStates")]
        public IEnumerable<State> GetStates()
        {
            return State.GetAllStates();
        }


        //Get:  All Lgas
        [HttpGet]
        [Route("/api/Customer/GetLga/{StateId?}")]
        public IEnumerable<Lga> GetLgas(int StateId = 1)
        {
            List<Lga> listLga = Lga.GetAllLgas();
            return listLga.Where(item => item.LgaId == StateId);
        }

        //Generate a random number
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size    
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password    
        private string GenerateNumber()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        // Generate MD5
        private static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                //lower  
                byte2String += targetData[i].ToString("x2");
                //upper  
                //byte2String += targetData[i].ToString("X2");  
            }
            return byte2String;
        }
    } 

}

   




