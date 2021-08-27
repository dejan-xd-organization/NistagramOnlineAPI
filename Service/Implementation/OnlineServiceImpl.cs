﻿using System.Collections.Generic;
using NistagramSQLConnection.Model;
using NistagramSQLConnection.Service.Interface;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.DTO.WallPost;
using NistagramUtils.Response;

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
        public List<WallPostDto> GetAllWallPosts(bool isPublic, int page, int limit)
        {
            List<bool> isPublics = new List<bool>();
            if (!isPublic)
            {
                isPublics.Add(true);
                isPublics.Add(false);
            }
            else isPublics.Add(true);

            List<WallPost> wallPost = _iPostService.GetAllWallPosts(isPublics, page, 20);
            List<WallPostDto> wallPostDto = new List<WallPostDto>(wallPost.Count);

            foreach (WallPost wp in wallPost)
            {
                wallPostDto.Add(new WallPostDto(wp));
            }

            return wallPostDto;
        }

        public bool PutReaction(ReactionDto reactionDto)
        {
            _iPostService.PutReaction(reactionDto.id, reactionDto.type, reactionDto.userId);
            return false;
        }

        public PostResponseDto NewPost(PostDto postDto)
        {
            string link = "../../../../assets/images/resources/user-avatar-default.png";
            WallPost wallPost = _iPostService.NewPost(postDto.userId, postDto.description, link, true);
            PostResponseDto newPostDto = new PostResponseDto(wallPost);
            return newPostDto;
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

        public List<UserDto> GetFollowers(string id, int page, bool type)
        {
            List<UserFollower> userFollowers = _iUserService.GetFollowers(id, page, 20, type);
            List<UserDto> userDto = new List<UserDto>(userFollowers.Count);

            foreach (UserFollower uf in userFollowers)
            {
                userDto.Add(new UserDto(uf.follower.user));
            }

            return userDto;
        }

        public List<UserDto> GetNewFollowings(string id, int page)
        {
            List<UserFollowing> userFollowings = _iUserService.GetNewFollowings(id, page, 20);
            List<UserDto> userDto = new List<UserDto>(userFollowings.Count);

            foreach (UserFollowing uf in userFollowings)
            {
                userDto.Add(new UserDto(uf.following.user));
            }

            return userDto;
        }
    }
}
