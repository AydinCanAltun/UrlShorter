using WebApp.Models;

namespace WebApp.Helpers {
    public static class UrlHelper {
        private static readonly long beginginTicks = new DateTime(2016, 6, 14).Ticks;
        public static Response Validate(string url) {
            Response response = new Response();
            try {
                Uri uri = new Uri(url);
                response.IsSuccess = true;
            }
            catch(Exception ex) {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public static string GenerateUniqueUrl() {
            return (DateTime.Now.Ticks - beginginTicks).ToString("x");
        }

    }
}
