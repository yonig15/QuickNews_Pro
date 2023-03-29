using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickNews.Entities;
using QuickNews.MicroService.VModel;
using QuickNews.Model;

namespace QuickNews.MicroService.Controllers
{
    [ApiController]
    [Route("api/newsitems")]
    public class NewsItemsContoller : ControllerBase
    {
        private Mapper mapper;

        public NewsItemsContoller()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<M_NewsItem, ReturnNewsItemRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllNewsItems()
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ GetAllNewsItems  ran Successfully - ");

				List<M_NewsItem> newsItems = MainManager.Instance.newsItemService.GetAllNewsItems();
                List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems);
                return Ok(returnNewsItems);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpGet("getbytopic")]
        public IActionResult GetAllNewsItemsByTopic(string topic)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ GetAllNewsItemsByTopic  ran Successfully - ");

				if (!string.IsNullOrEmpty(topic))
                {
                    List<M_NewsItem> newsItems = MainManager.Instance.newsItemService.GetAllNewsItemsByTopic(topic);
                    if (newsItems == null && !newsItems.Any())
                    {
                        return BadRequest("There are not any news from this topic");
                    }
                    List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems.OrderByDescending(n => n.PublishDate).Take(10).ToList());
                    return Ok(returnNewsItems);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpGet("gettrending")]
        public IActionResult GetTrendingNewsItems(string userId)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ GetTrendingNewsItems  ran Successfully - ");

				List<M_NewsItem> newsItems = MainManager.Instance.newsItemService.GetTrendingNewsItems(userId);
                List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems);
                return Ok(returnNewsItems);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpGet("getcurious")]
        public IActionResult GetCuriousNewsItems(string userId)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ GetCuriousNewsItems  ran Successfully - ");

				List<M_NewsItem> newsItems = MainManager.Instance.newsItemService.GetCuriousNewsItems(userId);
                List<ReturnNewsItemRequest> returnNewsItems = mapper.Map<List<ReturnNewsItemRequest>>(newsItems);
                return Ok(returnNewsItems);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpGet("get/{id}")]
        public IActionResult GetNewsItemById(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ GetNewsItemById  ran Successfully - ");

				M_NewsItem newsItem = MainManager.Instance.newsItemService.GetNewsItemById(id);
                if (newsItem == null)
                {
                    return NotFound();
                }
                ReturnNewsItemRequest returnCategory = mapper.Map<ReturnNewsItemRequest>(newsItem);
                return Ok(returnCategory);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public IActionResult AddNewsItem(AddNewsItemRequest addNewsItemRequest)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ AddNewsItem  ran Successfully - ");

				MainManager.Instance.newsItemService.AddNewNewsItem(addNewsItemRequest.ItemId, addNewsItemRequest.Title, addNewsItemRequest.Description, addNewsItemRequest.Link, addNewsItemRequest.ImageUrl, addNewsItemRequest.PublishDate, addNewsItemRequest.CategoryId, addNewsItemRequest.WebSiteId);
                return Ok();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public IActionResult UpdateNewsItem(string id, int ClickCount)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ UpdateNewsItem  ran Successfully - ");

				MainManager.Instance.newsItemService.UpdateNewsItemById(id, ClickCount);
                return NoContent();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpDelete("remove/{id}")]
        public IActionResult DeleteNewsItem(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ NewsItemsContoller \ DeleteNewsItem  ran Successfully - ");

				MainManager.Instance.newsItemService.DeleteNewsItemById(id);
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
