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
    public class BankController : ControllerBase
    {
        IApplicationRepository ApplicationRepository;
        private readonly ApplicationDBContext _context;

          //To  Consume ALAT API Portal
       private string BASE_URL = "https://wema-alatdev-apimgt.developer.azure-api.net/";
        public BankController(ApplicationDBContext context, IApplicationRepository TBank)
        {
            _context = context;

            ApplicationRepository = TBank;
        }
         
        // Consume and return endpoint of existing Bank;
        [HttpPost]
        [Route("Api/GetBank")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]
        public JsonResult GetBank()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            var values = new List<KeyValuePair<string, string>>();
            var OTPCode = GenerateNumber();
            
            var content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync("product#product=alat-tesh-test", content).Result;
            var result = "";
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }

            return new JsonResult(result);
        }


        // verified via OTP 
        [HttpPut]
        [Route("Api/PUTBBAnk/{Id}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]
        public JsonResult PUTBank(int Id, [FromBody] Bank Bank)
        {
            if (Id != 0)
            {
                return new JsonResult("");
            }

            var bank = ApplicationRepository.Banks.FirstOrDefault(e => e.BankId == Id);
            if (bank == null)
            {
                return new JsonResult("Record Not Found");
            }

            ApplicationRepository.Save();
            {
                return new JsonResult("Record Not Found");
            }

            return new JsonResult(Bank);
        }
        //GET : Bank/Controller
        //   Endpoint to get all banks 
        [HttpGet]
        [Route("Api/GetAllBanks")]
        [ProducesResponseType(200, Type = typeof(List<Customer>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]

        public JsonResult GetAllBanks()
        {
            return new JsonResult(ApplicationRepository.Banks.ToListAsync());

        }


        //   Endpoint to return customer by Id
        [HttpGet]
       [Route("Api/GetBank/{Id}")]

        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType(typeof(Customer))]
        public JsonResult GetBank(long Id)
        {
            try
            {
                var Bank = ApplicationRepository.Banks.Where(x => x.BankId.Equals(Id));

                if (Bank == null)
                {

                    return new JsonResult("Record Not Found");
                }

                return new JsonResult(Bank);
            }
            catch (Exception e)
            {
                return new JsonResult("bank");
            }
        }

        //Generate a random number
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
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

                byte2String += targetData[i].ToString("x2");


            }
            return byte2String;
        }


    }

}






