using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShorteningAPI.DataTransferObjects;
using URLShorteningAPI.Models;

namespace URLShorteningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlContext _context;

        private static Random random = new Random();

        public UrlController(UrlContext context)
        {
            _context = context;
        }

        // GET: api/Url
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrlModel>>> GetUrlItems()
        {
            return await _context.UrlItems.ToListAsync();
        }

        // GET: api/Url/5

        [HttpGet("{id}")]
        public async Task<ActionResult<UrlModel>> GetUrlModel(long id)
        {
            var urlModel = await _context.UrlItems.FindAsync(id);

            if (urlModel == null)
            {
                return NotFound();
            }

            return urlModel;
        }

        // POST: api/Url
        [HttpPost]
        public async Task<ActionResult<DTOUrl>> PostUrlModel(DTOUrl dtoUrl)
        {
            try
            {
                if (isValid(dtoUrl))
                {
                    UrlModel urlModel = new UrlModel()
                    {
                        Id = generateNewId(),
                        Url = dtoUrl.Url,
                        ShortenedUrl = dtoUrl.IsAutoGenerate ? generateShortedUrl(dtoUrl.Url) : dtoUrl.ShortUrl
                    };

                    _context.UrlItems.Add(urlModel);
                    await _context.SaveChangesAsync();

                    //return CreatedAtAction("GetUrlModel", new { id = urlModel.Id }, urlModel);
                    return CreatedAtAction(nameof(GetUrlModel), new { id = urlModel.Id }, urlModel);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        private bool UrlModelExists(long id)
        {
            return _context.UrlItems.Any(e => e.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoUrl"></param>
        /// <returns></returns>
        private bool isValid(DTOUrl dtoUrl)
        {
            if (dtoUrl == null || string.IsNullOrEmpty(dtoUrl.Url))
            {
                throw new Exception("URL is null");
            }

            if (!dtoUrl.IsAutoGenerate && string.IsNullOrEmpty(dtoUrl.ShortUrl))
            {
                throw new Exception("Short URL is null");
            }

            if (string.IsNullOrEmpty(dtoUrl.Url) || (!dtoUrl.Url.StartsWith("https://") && !dtoUrl.Url.StartsWith("http://")) || dtoUrl.Url == "https://" || dtoUrl.Url == "http://")
            {
                throw new Exception("Invalid URL");
            }

            var uri = new Uri(dtoUrl.Url);

            if (!dtoUrl.IsAutoGenerate && !string.IsNullOrEmpty(dtoUrl.ShortUrl))
            {

                var sortedUri = new Uri(dtoUrl.ShortUrl);

                if (sortedUri.AbsolutePath.Length > 8)
                {
                    throw new Exception("Short URL can not longer than 6 charakters.");
                }

                if (_context.UrlItems.Any(p => p.ShortenedUrl == dtoUrl.ShortUrl))
                {
                    throw new Exception("That Short URL already created.");
                }

            }
              


            return true;
        }

        private long generateNewId()
        {
            if (_context.UrlItems == null || !_context.UrlItems.Any())
                return 1;

            return _context.UrlItems.Max(e => e.Id) + 1;
        }

        private string generateShortedUrl(string url)
        {
            var uri = new Uri(url);
            string host = uri.Host;

            if (host.StartsWith("www.")) 
            {
                host = host.Replace("www.", "");
            }

            if(host.EndsWith(".com"))
            {
                host = host.Replace(".com", "");
            }


            return uri.Scheme + "//" + host + "/" + randomString(6) + "/";
        }

        private static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
