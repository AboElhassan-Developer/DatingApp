using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IMemberRepositroy? _memberRepository;
        private IMessageRepositroy? _messageRepository;
            private ILikesRepositroy? _likesRepository;



        public IMemberRepositroy MemberRepository => _memberRepository 
            ??= new MemberRepositroy(context);

        public IMessageRepositroy MessageRepositroy => _messageRepository
            ??= new MessageRepositroy(context);

        public ILikesRepositroy LikesRepositroy => _likesRepository 
            ??= new LikesRepositroy(context);

        public async Task<bool> Complete()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}
