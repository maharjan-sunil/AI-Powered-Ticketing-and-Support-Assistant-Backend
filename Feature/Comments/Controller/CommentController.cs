using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Model;
using Microsoft.AspNetCore.Mvc;


namespace DockerDemo.Docker.Controller
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentService _service;
        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentByTicketId(Guid ticketId)
        {
            return await _service.GetCommentByTicketIdAsync(ticketId);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            await _service.AddCommentAsync(comment);
            return Ok();

        }

        [HttpPatch]
        //[Authorize]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            var updatedComment = await _service.UpdateCommentAsync(comment);
            return Ok(updatedComment);

        }

        [HttpDelete]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid comment ID.");
            }
            await _service.DeleteCommentAsync(id);
            return Ok();

        }


    }
}

