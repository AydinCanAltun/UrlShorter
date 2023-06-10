using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json;

namespace WebApp.Models {
    public class Response {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Response<T> : Response { 
        public T Result { get; set; }

        public Response ToResponse() {
            Response response = new Response();
            response.IsSuccess = this.IsSuccess;
            response.Message = this.Message;
            return response;
        }
    }

}
