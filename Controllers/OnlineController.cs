using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NistagramOnlineAPI.Service;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.DTO.User;
using NistagramUtils.DTO.WallPost;
using NistagramUtils.Response;

namespace NistagramOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnlineController : ControllerBase
    {
        private readonly IOnlineService _iOnlineService;

        public OnlineController(IOnlineService iOnlineService)
        {
            _iOnlineService = iOnlineService;
        }

        // WALL POSTS //

        [HttpGet]
        [Route("/[action]")]
        public Object GetAllWallPosts()
        {
            List<WallPostDto> wallPosts = _iOnlineService.GetAllWallPosts(false, 1, 20);
            return JsonConvert.SerializeObject(wallPosts);
        }

        [HttpPut]
        [Route("/[action]")]
        public Object PutReaction(ReactionDto reactionDto)
        {
            bool reaction = _iOnlineService.PutReaction(reactionDto);
            return JsonConvert.SerializeObject(reaction);
        }

        [HttpPost]
        [Route("/[action]")]
        public Object NewPost(PostDto postDto)
        {
            var newPost = _iOnlineService.NewPost(postDto);
            return JsonConvert.SerializeObject(newPost);
        }

        // FOLLOWERS //

        [HttpPost]
        [Route("/[action]")]
        public Object AddNewFollower(NewFollower newFollower)
        {
            Response res = _iOnlineService.AddNewFollower(newFollower);
            return res;
        }

        [HttpPost]
        [Route("/[action]")]
        public Object AddFollowing(NewFollowingDto newFollowingDto)
        {
            UserDto userDto = _iOnlineService.AddFollowing(newFollowingDto.friendId, newFollowingDto.myId);
            return userDto;
        }

        [HttpGet]
        [Route("/[action]")]
        public Object GetMyFollowers(string id, int page)
        {
            List<UserDto> user = _iOnlineService.GetMyFollowers(id, page, true);
            return user;
        }

        [HttpGet]
        [Route("/[action]")]
        public Object GetMyFollowing(string id, int page)
        {
            List<UserDto> user = _iOnlineService.GetMyFollowing(id, page);
            return user;
        }

        [HttpGet]
        [Route("/[action]")]
        public Object GetNewFollowers(string id)
        {
            List<UserDto> userDto = _iOnlineService.GetNewFollowers(id);
            return userDto;
        }

        [HttpGet]
        [Route("/[action]")]
        public Object GetNewFollowings(string id)
        {
            List<UserDto> userDto = _iOnlineService.GetNewFollowings(id);
            return userDto;
        }


        // USER

        [HttpPut]
        [Route("/[action]")]
        public Object UpdateUser(UpdateUserDto updateUserDto)
        {

            Response res = _iOnlineService.UpdateUser(updateUserDto);
            return res;
        }

        [HttpPut]
        [Route("/[action]")]
        public Object ChangePassword(ChangePasswordDto changePasswordDto)
        {

            Response res = _iOnlineService.ChangePassword(changePasswordDto);
            return res;
        }

        [HttpGet]
        [Route("/[action]")]
        public Object GetMyOnlineWallPosts(long id)
        {
            List<WallPostDto> wallPosts = _iOnlineService.GetMyWallPosts(id, 1, 20);
            return JsonConvert.SerializeObject(wallPosts);
        }

    }
}
