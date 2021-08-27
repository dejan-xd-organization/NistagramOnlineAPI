using System.Collections.Generic;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.DTO.WallPost;
using NistagramUtils.Response;

namespace NistagramOnlineAPI.Service
{
    public interface IOnlineService
    {
        List<WallPostDto> GetAllWallPosts(bool isPublic, int page, int limit);
        Response AddNewFollower(NewFollower newFollower);
        List<UserDto> GetFollowers(string id, int page, bool type);
        List<UserDto> GetNewFollowings(string id, int page);
        bool PutReaction(ReactionDto reactionDto);
        PostResponseDto NewPost(PostDto postDto);
    }
}
