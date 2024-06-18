using System.Net;

namespace WebAppDemo.DTOs.Resposes
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public object data { get; set; }

       /// <summary>

      /// </summary>
       ///     <param name = "StatusCode" > </param >
        ///    < param name = "Data" > </param >


            public void CreateResponse(HttpStatusCode statusCode, object data)
            {
                 status_code =( int)statusCode;
                this.data = data;

            }
    }
}
