using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickNews.Entities;
using QuickNews.MicroService.VModel;
using QuickNews.Model;

namespace QuickNews.MicroService.Controllers
{
    [ApiController]
    [Route("api/websites")]
    public class WebSitesController : ControllerBase
    {
        private Mapper mapper;

        public WebSitesController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<M_WebSite, ReturnWebSiteRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllWebSite()
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ WebSitesController \ GetAllWebSite  ran Successfully - ");

				List<M_WebSite> websites = MainManager.Instance.websiteService.GetAllWebSites();
                List<ReturnWebSiteRequest> returnWebsites = mapper.Map<List<ReturnWebSiteRequest>>(websites);
                return Ok(returnWebsites);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpGet("get/{id}")]
        public IActionResult GetWebSiteById(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ WebSitesController \ GetWebSiteById  ran Successfully - ");

				M_WebSite webSite = MainManager.Instance.websiteService.GetWebSiteById(id);
                if (webSite == null)
                {
                    return NotFound();
                }
                ReturnWebSiteRequest returnWebsite = mapper.Map<ReturnWebSiteRequest>(webSite);
                return Ok(returnWebsite);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public IActionResult AddWebSite(string name)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ WebSitesController \ AddWebSite  ran Successfully - ");

				MainManager.Instance.websiteService.AddNewWebSite(name);
                return Ok();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public IActionResult UpdateWebSite(int id, string name)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ WebSitesController \ UpdateWebSite  ran Successfully - ");

				MainManager.Instance.websiteService.UpdateWebSiteById(id, name);
                return NoContent();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpDelete("remove/{id}")]
        public IActionResult DeleteWebSite(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ WebSitesController \ DeleteWebSite  ran Successfully - ");

				MainManager.Instance.websiteService.DeleteWebSiteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }
    }
}
