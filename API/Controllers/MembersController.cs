using API.Data;
using API.Entities;
using API.Interfaces;
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
    }
}
