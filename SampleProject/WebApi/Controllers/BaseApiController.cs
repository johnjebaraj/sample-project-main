using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public HttpResponseMessage Found(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        public HttpResponseMessage Found()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DoesNotExist()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
        }
        public HttpResponseMessage AlreadyExist(string reasonMsg)
        {
            /*var response = this.AlreadyExist();
            response.Content = new StringContent(reasonMsg, Encoding.UTF8, "application/json");*/
            return ControllerContext.Request.CreateResponse(HttpStatusCode.Conflict, reasonMsg);
            //return response;
        }
        public HttpResponseMessage InvalidData(string reasonMsg)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, reasonMsg);
        }

    }
}