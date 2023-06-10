using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Context;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers {
    public class ShortUrlGeneratorController : Controller {
        private readonly AppDbContext _context;

        public ShortUrlGeneratorController(AppDbContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<string> Create(string url) {
            Response urlValidation = UrlHelper.Validate(url);
            if(!urlValidation.IsSuccess) {
                return urlValidation.ToString();
            }
            Url dbUrl = await _context.Url.Where(x => x.RedirectUrl == url).FirstOrDefaultAsync();
            Response<Url> creationResponse = new Response<Url>();
            try {
                if (dbUrl == null) {
                    Url newUrl = new Url();
                    newUrl.UrlId = UrlHelper.GenerateUniqueUrl();
                    newUrl.RedirectUrl = url;
                    newUrl.CreationDate = DateTime.Now;
                    _context.Url.Add(newUrl);
                    await _context.SaveChangesAsync();
                    creationResponse.Result = newUrl;
                }
                else {
                    creationResponse.Result = dbUrl;
                }
                creationResponse.IsSuccess = true;
            }
            catch(Exception ex) {
                creationResponse.IsSuccess = false;
                creationResponse.Message = ex.Message;
            }
            return creationResponse.ToString();
        }
    }
}
