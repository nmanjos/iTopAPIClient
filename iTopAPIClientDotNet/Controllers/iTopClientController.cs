using iTopAPIClientDotNet.API;
using iTopAPIClientDotNet.API.Request;
using iTopAPIClientDotNet.API.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace iTopAPIClientDotNet.Controllers
{
    public  class iTopClientController : ApiController
    {
        // POST api/iTopClient
        public async Task<ResponseMessage> CreateIncident([FromBody]string value)
        {
            return  await iTopAPIClient.iTopAPICall( JsonConvert.DeserializeObject<RequestMessage>(value));
        }
    }
}
