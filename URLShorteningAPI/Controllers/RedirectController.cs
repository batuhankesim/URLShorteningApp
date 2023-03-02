using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RedirectController : ControllerBase
    {
        private readonly UrlContext _context;

        public RedirectController(UrlContext context)
        {
            _context = context;
        }

        // GET: api/Redirect/5
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

        // POST: api/Redirect
        [HttpPost]
        public async Task<ActionResult<DTOUrl>> PostUrlModel(DTOUrl dtoUrl)
        {

            try
            {
                if (isValid(dtoUrl))
                {
                    var urlModel = await _context.UrlItems.FirstAsync(p => p.ShortenedUrl == dtoUrl.ShortUrl);

                    if (urlModel == null)
                    {
                        return NotFound();
                    }
                    return CreatedAtAction("GetUrlModel", new { id = urlModel.Id }, urlModel);
                }

                return BadRequest();
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

        private bool isValid(DTOUrl dtoUrl)
        {
            if (dtoUrl == null || string.IsNullOrEmpty(dtoUrl.ShortUrl))
            {
                throw new Exception("ShortUrl can not be empty");
            }

            return true;
        }
    }
}
