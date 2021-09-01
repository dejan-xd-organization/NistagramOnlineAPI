using System.Collections.Generic;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.DTO.User;
using NistagramUtils.DTO.WallPost;
using NistagramUtils.Response;

namespace NistagramOnlineAPI.Service
{
    public interface IOnlineService
    {
        List<WallPostDto> GetAllWallPosts(bool isPublic, int page, int limit);
        Response AddNewFollower(NewFollower newFollower);
        UserDto AddFollowing(long friendId, long myId);
        List<UserDto> GetMyFollowers(string id, int page, bool accepted);
        List<UserDto> GetMyFollowing(string id, int page);
        List<UserDto> GetNewFollowers(string id);
        List<UserDto> GetNewFollowings(string id);
        bool PutReaction(ReactionDto reactionDto);
        PostResponseDto NewPost(PostDto postDto);
        Response UpdateUser(UpdateUserDto updateUserDto);
        Response ChangePassword(ChangePasswordDto changePasswordDto);
        List<WallPostDto> GetMyWallPosts(long id, int page, int limit);
    }
}
