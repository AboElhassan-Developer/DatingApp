using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MessagesController(IMessageRepositroy messageRepositroy,
        IMemberRepositroy memberRepositroy) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            var sender = await memberRepositroy.GetMemberByIdAsync(User.GetMemberId());
            var recipient = await memberRepositroy.GetMemberByIdAsync(createMessageDto.RecipientId);

            if (recipient == null || sender == null || sender.Id == createMessageDto.RecipientId)
                return BadRequest("Cannot send this message");

            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                Content = createMessageDto.Content
            };

            messageRepositroy.AddMessage(message);


            if (await messageRepositroy.SaveAllAsync())
                return Ok(message.ToDto());
            return BadRequest("Failed to send message");
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<MessageDto>>> GetMessagesByContainer
            ([FromQuery] MessageParams messageParams)
        {
            messageParams.MemberId = User.GetMemberId();
            return await messageRepositroy.GetMessagesForMember(messageParams);
        }
        [HttpGet("thread/{recipientId}")]
        public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetMessageThread(string recipientId)
        {
           return Ok(await messageRepositroy.GetMessageThread(User.GetMemberId(), recipientId));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(string id)
        {
            var memberId = User.GetMemberId();
            var message = await messageRepositroy.GetMessage(id);
            if (message == null) return BadRequest("Cannot delete this message");
            if (message.SenderId != memberId && message.RecipientId != memberId)
                return BadRequest("yYu Cannot delete this message");
            if (message.SenderId == memberId) message.SenderDeleted = true;

            if (message.RecipientId == memberId) message.RecipientDeleted = true;

            if (message is { SenderDeleted: true, RecipientDeleted: true })
            {
                messageRepositroy.DeleteMessage(message);
            }

            if (await messageRepositroy.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the message");

        }

       }

}
