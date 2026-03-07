using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController(ILikesRepositroy likesRepositroy) : BaseApiController
    {
        [HttpPost("{targetMemberId}")]
        public async Task<ActionResult> ToggleLike(string targetMemberId)
        {
           var sourceMemberId = User.GetMemberId();
            if (sourceMemberId == targetMemberId) return BadRequest("You cannot like yourself");
            var existingLike = await likesRepositroy.GetMemberLike(sourceMemberId, targetMemberId);
            if (existingLike == null)
            {
                var like = new MemberLike
                {
                    SourceMemberId = sourceMemberId,
                    TargetMemberId = targetMemberId
                };
                likesRepositroy.AddLike(like);
            }
            else
            {
                likesRepositroy.DeleteLike(existingLike);
            }

            if (await likesRepositroy.SaveAllChanges()) return Ok();
            return BadRequest("Failed to update like");
        }

        [HttpGet("list")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetCurrentMemberLikeIds()
        {
            return Ok(await likesRepositroy.GetCurrentMemberLikeIds(User.GetMemberId()));

        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<Member>>> GetMemberLikes(
           [FromQuery] LikesParams likesParams)
        {
            likesParams.MemberId = User.GetMemberId();
            var members = await likesRepositroy.GetMemberLikes(likesParams);
            return Ok(members);

        }








        }
}
