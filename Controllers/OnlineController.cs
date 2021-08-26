using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NistagramOnlineAPI.Service;
using NistagramSQLConnection.Model;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.DTO.WallPost;
using NistagramUtils.Response;
using System;
using System.Collections.Generic;

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
            List<WallPost> wallPosts = _iOnlineService.GetAllWallPosts();
            List<WallPostDto> postDTO = _mapper.Map<List<WallPostDto>>(wallPosts);
            return JsonConvert.SerializeObject(postDTO);
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
