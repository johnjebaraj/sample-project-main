using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Net.Http
{
    public static class ResponseMessageExtensions
    {
        public static void HttpResponseMessage(this HttpResponseMessage message, HttpStatusCode statusCode,string reasonMsg)
        {
            message = new HttpResponseMessage(statusCode);
            message.Content = new StringContent(reasonMsg, Encoding.UTF8, "application/json");
        }

    }
}