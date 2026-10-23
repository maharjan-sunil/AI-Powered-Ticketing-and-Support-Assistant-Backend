
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Model;
using Microsoft.AspNetCore.Mvc;

namespace AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Interface
{
    public interface ICommentService
    {
        
        public Task AddCommentAsync(Comment comment);
        public Task DeleteCommentAsync(Guid id);
        public Task<ActionResult<IEnumerable<Comment>>> GetCommentByTicketIdAsync(Guid ticketId);
        public Task<Comment> UpdateCommentAsync(Comment comment);
    }
}
