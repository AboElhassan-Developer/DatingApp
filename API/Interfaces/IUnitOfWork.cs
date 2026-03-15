namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IMemberRepositroy MemberRepository { get; }
        IMessageRepositroy MessageRepositroy { get; }
        ILikesRepositroy LikesRepositroy { get; }
        Task<bool> Complete();
         bool HasChanges();

    }
}
