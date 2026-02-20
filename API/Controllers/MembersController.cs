using API.Data;
using API.Entities;
using API.DTOs;
using API.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [Authorize]
    public class MembersController(IMemberRepositroy memberRepositroy) : BaseApiController
    {
        [HttpGet]
         public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            return Ok(await memberRepositroy.GetMembersAsync());
        }

        [HttpGet("{id}")]
        
        public async Task <ActionResult<Member>> GetMember(string id)
        {
            var member =await memberRepositroy.GetMemberByIdAsync(id);
            if(member == null) return NotFound();
            return member;
        }

        [HttpGet("{id}/photos")]
        public async Task <ActionResult<IReadOnlyList<Photo>>>GetMemberPhotos(string id)
        {
            return Ok(await memberRepositroy.GetPhotosForMemberAsync(id));
        }

    [HttpPut]
    public async Task<ActionResult> UpdateMember(MemberUpdateDto memberUpdateDto)
    {
      var memberId = User.GetMemberId();
      
      var member = await memberRepositroy.GetMemberForUpdate(memberId);
      if (member == null) return BadRequest("Could not get member"); 

      member.DisplayName = memberUpdateDto.DisplayName ?? member.DisplayName;
      member.Description = memberUpdateDto.Description ?? member.Description;
      member.City = memberUpdateDto.City ?? member.City;
      member.Country = memberUpdateDto.Country ?? member.Country;

        member.User.DisplayName=memberUpdateDto.DisplayName?? member.User.DisplayName;
      memberRepositroy.Update(member);
      if (await memberRepositroy.SaveAllAsync()) return NoContent();
      return BadRequest("Failed to update member");
    }
  }
}
