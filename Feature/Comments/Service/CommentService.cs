using AI_Powered_Ticketing_and_Support_System.Data;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Service
{
    public class CommentService : ICommentService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<CommentService> _logger;
        //  private readonly AzureKey _key;

        public CommentService(ApplicationDbContext context, ILogger<CommentService> logger)
        {
            _context = context;
            //    _key = key;
            _logger = logger;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentByTicketIdAsync(Guid ticketId)
        {
            return await _context.Comments.Where(x => x.TicketId == ticketId).ToListAsync();
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            var commentDetail = await _context.Comments.FindAsync(comment.Id);
            if (commentDetail == null)
            {
                return null;
            }


            commentDetail.Message = comment.Message;

            await _context.SaveChangesAsync();

            return commentDetail;

        }

    
    }
}
