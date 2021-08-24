using NistagramSQLConnection.Model;
using NistagramSQLConnection.Service.Interface;
using NistagramUtils.DTO.Follower;
using NistagramUtils.Response;
using System;
using System.Collections.Generic;

namespace NistagramOnlineAPI.Service.Implementation
{
    public class OnlineServiceImpl : IOnlineService
    {

        private readonly IUserService _iUserService;
        private readonly IPostService _iPostService;

        public OnlineServiceImpl(IUserService iUserService, IPostService iPostService)
        {
            _iPostService = iPostService;
            _iUserService = iUserService;
        }

        // WALL POSTS //

        public List<WallPost> GetAllWallPosts()
        {
            return _iPostService.GetAllWallPosts(false);
        }

        // FOLLOWERS

        public Response AddNewFollower(NewFollower newFollower)
        {
            Response res = new Response();
            bool status = _iUserService.AddNewFollower(newFollower.myId, newFollower.followerId);
            if (status)
            {
                res.status = "SUCCESS";
                res.message = "added_new_follower";
            }
            else
            {
                res.status = "ERROR";
                res.message = "failed_to_add_new_follower";
            }
            return res;
        }

        public List<User> GetNewFollowers(string id)
        {
            var res = _iUserService.GetNewFollowers(id);

            foreach (Object obj in res)
            {
                var i = obj;
            }

            return null;
        }
    }
}
