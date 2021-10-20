using System;
using System.IO;
using System.Threading.Tasks;
using DataModel.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlueCrestRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        [HttpGet("query/all")]
        public async Task<ActionResult<Request>> GetData()
        {
            var request = new Request { RequestId = "empty" };

            try
            {
                await Task.Factory.StartNew(() =>
                {
                    using var stream = new StreamReader("Data/testResult.json");
                    var content = stream.ReadToEnd();

                    request = JsonConvert.DeserializeObject<Request>(content);
                });

                return new ActionResult<Request>(request);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}