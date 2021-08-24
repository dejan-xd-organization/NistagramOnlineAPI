using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NistagramOnlineAPI.Service;
using NistagramSQLConnection.Model;
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
        public Object GetNewFollowers(string id)
        {
            List<User> user = _iOnlineService.GetNewFollowers(id);
            return user;
        }
    }
}
