using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickNews.Entities;
using QuickNews.MicroService.VModel;
using QuickNews.Model;

namespace QuickNews.MicroService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private Mapper mapper;

        public UsersController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<M_User, ReturnUserRequest>());
            mapper = new Mapper(config);
        }

        [HttpGet("get")]
        public IActionResult GetAllUsers()
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ UsersController \ GetAllUsers  ran Successfully - ");

				List<M_User> users = MainManager.Instance.userService.GetAllUsers();
                List<ReturnUserRequest> returnUsers = mapper.Map<List<ReturnUserRequest>>(users);
                return Ok(returnUsers);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpGet("get/{id}")]
        public IActionResult GetUserById(string id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ UsersController \ GetUserById  ran Successfully - ");

				M_User user = MainManager.Instance.userService.GetUserById(id);
                if (user == null)
                {
                    return NoContent();
                }
                ReturnUserRequest returnUser = mapper.Map<ReturnUserRequest>(user);
                returnUser.Interests = user.Interests.Select(i => i.Topic).ToArray();
                return Ok(returnUser);
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public IActionResult AddUser(AddUserRequest addUserRequest)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ UsersController \ AddUser  ran Successfully - ");

				MainManager.Instance.userService.AddNewUser(addUserRequest.Id, addUserRequest.Name, addUserRequest.Email, addUserRequest.PhoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(string id, UpdateUserRequest userUpdate)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ UsersController \ UpdateUser  ran Successfully - ");

				MainManager.Instance.userService.UpdateUserById(id, userUpdate.Name, userUpdate.PhoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpPut("updateInterests/{id}")]
        public IActionResult UpdateUserInterests(string id, string[] interests)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ UsersController \ UpdateUserInterests  ran Successfully - ");

				MainManager.Instance.userService.UpdateUserInterestsById(id, interests);
                return Ok();
            }
            catch (Exception ex)
            {
				MainManager.Instance.Log.LogException(@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				return BadRequest(ex.Message);
            }
        }


        [HttpDelete("remove/{id}")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
				MainManager.Instance.Log.LogEvent(@"MicroService.Controllers \ UsersController \ DeleteUser  ran Successfully - ");

				MainManager.Instance.userService.DeleteUserById(id);
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
