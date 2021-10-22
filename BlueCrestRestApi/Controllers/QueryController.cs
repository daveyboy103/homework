using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BlueCrestHomework.Extensions;
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
                await Task.Factory.StartNew(() => { request = BuildRequest(); });

                return new ActionResult<Request>(request);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private static Request BuildRequest()
        {
            Request request;
            using var stream = new StreamReader("Data/testResult.json");
            var content = stream.ReadToEnd();

            request = JsonConvert.DeserializeObject<Request>(content);
            return request;
        }

        /// <summary>
        /// This method would get the request pre-processed into an enumerable of RowMeasureItems as an alternative
        /// to getting the deserialized request object and then converting to enumerable at the other end.
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/all/enumerable")]
        public async Task<ActionResult<IEnumerable<RowMeasureItem>>> GetEnumerable()
        {
            var request = new Request { RequestId = "empty" };

            try
            {
                await Task.Factory.StartNew(() => { request = BuildRequest(); });

                return new ActionResult<IEnumerable<RowMeasureItem>>(request.ToEnumerableOfMeasures());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}