using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickNews.Entities;
using QuickNews.MicroService.VModel;
using QuickNews.Model;

namespace QuickNews.MicroService.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private Mapper mapper;

        public CategoriesController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<M_Category, ReturnCategoryRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllCategories()
        {
            try
            {
                MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ CategoriesController \ GetAllCategories  ran Successfully - ");
                List<M_Category> categories = MainManager.Instance.CategoryService.GetAllCategories();
                List<ReturnCategoryRequest> returnCategories = mapper.Map<List<ReturnCategoryRequest>>(categories);
                return Ok(returnCategories);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
				return BadRequest(ex.Message);
            }
        }


        [HttpGet("get/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ CategoriesController \ GetCategoryById  ran Successfully - ");

				M_Category Category = MainManager.Instance.CategoryService.GetCategoryById(id);
                if (Category == null)
                {
                    return NotFound();
                }
                ReturnCategoryRequest returnCategory = mapper.Map<ReturnCategoryRequest>(Category);
                return Ok(returnCategory);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
				return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public IActionResult AddCategory(string addCategoryRequest)
        {
            try
            {
				//MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ CategoriesController \ AddCategory  ran Successfully - ");

				MainManager.Instance.CategoryService.AddNewCategory(addCategoryRequest);
                return Ok();
            }
            catch (Exception ex)
            {
				//MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public IActionResult UpdateCategory(int id, string CategoryUpdate)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ CategoriesController \ UpdateCategory  ran Successfully - ");

				MainManager.Instance.CategoryService.UpdateCategoryById(id, CategoryUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpDelete("remove/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ CategoriesController \ DeleteCategory  ran Successfully - ");

				MainManager.Instance.CategoryService.DeleteCategoryById(id);
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
