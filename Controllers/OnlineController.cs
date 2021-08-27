using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NistagramOnlineAPI.Service;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.DTO.WallPost;
using NistagramUtils.Response;

namespace NistagramOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnlineController : ControllerBase
    {

        private readonly IOnlineService _iOnlineService;
        private readonly IMapper _mapper;

        public OnlineController(IOnlineService iOnlineService, IMapper mapper)
        {
            _iOnlineService = iOnlineService;
            _mapper = mapper;
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

        [HttpGet]
        [Route("/[action]")]
        public Object GetAllFollowers(string id, int page)
        {
            List<UserDto> user = _iOnlineService.GetFollowers(id, page, true);
            return user;
        }

        [HttpGet]
        [Route("/[action]")]
        public Object GetNewFollowings(string id, int page)
        {
            List<UserDto> userDto = _iOnlineService.GetNewFollowings(id, page);
            return userDto;
        }
    }
}
