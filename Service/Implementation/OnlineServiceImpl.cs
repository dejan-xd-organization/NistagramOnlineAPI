using System.Collections.Generic;
using AutoMapper;
using NistagramSQLConnection.Model;
using NistagramSQLConnection.Service.Interface;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.DTO.User;
using NistagramUtils.DTO.WallPost;
using NistagramUtils.Response;

namespace NistagramOnlineAPI.Service.Implementation
{
    public class OnlineServiceImpl : IOnlineService
    {

        private readonly IUserService _iUserService;
        private readonly IPostService _iPostService;
        private readonly IMapper _mapper;

        public OnlineServiceImpl(IUserService iUserService, IPostService iPostService, IMapper mapper)
        {
            _iPostService = iPostService;
            _iUserService = iUserService;
            _mapper = mapper;
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
            WallPost wallPost = _iPostService.NewPost(postDto.userId, postDto.description, link, postDto.isPublic);
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

        public UserDto AddFollowing(long friendId, long myId)
        {
            User user = _iUserService.AddFollowing(friendId, myId);
            if (user == null) return null;
            UserDto userDTO = _mapper.Map<UserDto>(user);
            return userDTO;
        }

        public List<UserDto> GetMyFollowers(string id, int page, bool accepted)
        {
            List<UserFollower> userFollowers = _iUserService.GetMyFollowers(id, page, 20, accepted);
            List<UserDto> userDto = new List<UserDto>(userFollowers.Count);

            foreach (UserFollower uf in userFollowers)
            {
                userDto.Add(new UserDto(uf.follower.user));
            }

            return userDto;
        }

        public List<UserDto> GetMyFollowing(string id, int page)
        {
            List<UserFollowing> userFollowings = _iUserService.GetMyFollowing(id, page, 20);
            List<UserDto> userDto = new List<UserDto>(userFollowings.Count);

            foreach (UserFollowing uf in userFollowings)
            {
                userDto.Add(new UserDto(uf.following.user));
            }

            return userDto;
        }

        public List<UserDto> GetNewFollowers(string id)
        {
            List<UserFollower> userFollowers = _iUserService.GetNewFollowers(id);
            List<UserDto> userDto = new List<UserDto>(userFollowers.Count);

            foreach (UserFollower uf in userFollowers)
            {
                userDto.Add(new UserDto(uf.follower.user));
            }

            return userDto;
        }

        public List<UserDto> GetNewFollowings(string id)
        {
            List<UserFollowing> userFollowings = _iUserService.GetNewFollowings(id);
            List<UserDto> userDto = new List<UserDto>(userFollowings.Count);

            foreach (UserFollowing uf in userFollowings)
            {
                userDto.Add(new UserDto(uf.following.user));
            }

            return userDto;
        }


        // USERS

        public Response UpdateUser(UpdateUserDto updateUserDto)
        {
            Response res = new Response();
            var mapperUser = _mapper.Map<User>(updateUserDto);
            User user = _iUserService.UpdateUser(mapperUser);
            if (user != null)
            {
                UpdateUserDto uuDto = new UpdateUserDto();
                uuDto.id = user.id;
                uuDto.firstName = user.firstName;
                uuDto.lastName = user.lastName;
                uuDto.username = user.username;
                uuDto.email = user.email;
                uuDto.sex = user.sex;
                uuDto.isPublicProfile = user.isPublicProfile;
                uuDto.relationship = user.relationship;
                uuDto.dateOfBirth = (System.DateTime)user.dateOfBirth;
                uuDto.address = user.address;


                res.status = "SUCCESS";
                res.message = "user_update_SUCCESS";
                res.updateUserDto = uuDto;
            }
            else
            {
                res.status = "ERROR";
                res.message = "user_update_failed";
            }
            return res;
        }


        public Response ChangePassword(ChangePasswordDto changePasswordDto)
        {
            Response res = new Response();
            bool status = _iUserService.ChangePassword(changePasswordDto.id, changePasswordDto.oldPassword, changePasswordDto.newPassword);
            if (status)
            {
                res.status = "SUCCESS";
                res.message = "passwor_changed_SUCCESS";
            }
            else
            {
                res.status = "ERROR";
                res.message = "password_changed_failed";
            }
            return res;
        }

        public List<WallPostDto> GetMyWallPosts(long id, int page, int limit)
        {

            List<UserPost> userPost = _iPostService.GetMyWallPosts(id, page, 20);
            List<WallPostDto> wallPostDto = new List<WallPostDto>(userPost.Count);

            foreach (UserPost up in userPost)
            {
                wallPostDto.Add(new WallPostDto(up.wallPost, up.user));
            }

            return wallPostDto;
        }
    }
}