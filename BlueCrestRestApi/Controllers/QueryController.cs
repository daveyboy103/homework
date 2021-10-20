using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DataModel;
using DataModel.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Request = DataModel.Dtos.Request;

namespace BlueCrestRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        [HttpGet("query/all")]
        public async Task<ActionResult<Request>> GetData()
        {
            Request request = new Request{RequestId = "empty"};

            try
            {
                await Task.Factory.StartNew(() =>
                {
                    using var stream = new StreamReader("Data/testResult.json");
                    string content = stream.ReadToEnd();

                    JsonDocument doc = JsonDocument.Parse(content);

                    request = JsonConvert.DeserializeObject<Request>(content);
                });
            
                return  new ActionResult<Request>(request);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}