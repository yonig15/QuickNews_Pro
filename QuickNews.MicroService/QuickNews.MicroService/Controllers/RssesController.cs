using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickNews.Entities;
using QuickNews.MicroService.VModel;
using QuickNews.Model;

namespace QuickNews.MicroService.Controllers
{
    [ApiController]
    [Route("api/rsses")]
    public class RssesController : ControllerBase
    {
        private Mapper mapper;

        public RssesController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<M_Rss, ReturnRssRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllRsses()
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ RssesController \ GetAllRsses  ran Successfully - ");

				List<M_Rss> rsses = MainManager.Instance.rssService.GetAllRsses();
                List<ReturnRssRequest> returnRsses = mapper.Map<List<ReturnRssRequest>>(rsses);
                return Ok(returnRsses);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpGet("get/{id}")]
        public IActionResult GetRssById(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ RssesController \ GetRssById  ran Successfully - ");

				M_Rss rss = MainManager.Instance.rssService.GetRssById(id);
                if (rss == null)
                {
                    return NotFound();
                }
                ReturnRssRequest returnRss = mapper.Map<ReturnRssRequest>(rss);
                return Ok(returnRss);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddRss(AddRssRequest addRssRequest)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ RssesController \ AddRss  ran Successfully - ");

				MainManager.Instance.rssService.AddNewRss(addRssRequest.Url, addRssRequest.CategoryId, addRssRequest.WebSiteId);
                return Ok();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public IActionResult UpdateRss(int id, UpdateRssRequest updateRssRequest)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ RssesController \ UpdateRss  ran Successfully - ");

				MainManager.Instance.rssService.UpdateRssById(id, updateRssRequest.Url, updateRssRequest.CategoryId, updateRssRequest.WebSiteId);
                return NoContent();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpDelete("remove/{id}")]
        public IActionResult DeleteRss(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ RssesController \ DeleteRss  ran Successfully - ");

				MainManager.Instance.rssService.DeleteRssById(id);
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
