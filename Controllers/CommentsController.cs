using System.Threading.Tasks;
using Blogger.Models;
using Blogger.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  public class CommentsController : ControllerBase
  {
    private readonly CommentsService _commentsService;
    private readonly BlogsService _blogsService;


    public CommentsController(CommentsService commentsService, BlogsService blogsService)
    {
      _commentsService = commentsService;
      _blogsService = blogsService;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment commentData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        int blogId = commentData.Blog;
        Blog blogObject = _blogsService.GetById(blogId);
        commentData.CreatorId = userInfo.Id;
        commentData.Title = blogObject.Title;
        commentData.Published = blogObject.published;
       Comment comment = _commentsService.CreateComment(commentData);
       comment.Creator = userInfo;
       return Ok(comment);
      }
      catch (System.Exception e)
      {
       return BadRequest(e.Message);
      }
    }
    [HttpPut ("{commentId}")]
    [Authorize]

    public async Task<ActionResult<Comment>> Update(int commentId, [FromBody] Comment commentData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string creatorId = userInfo.Id;
        var comment = _commentsService.Update(commentData, commentId, creatorId);
        return Ok(comment);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete ("{commentId}")]
    [Authorize]

    public async Task<ActionResult<Comment>> RemoveComment(int commentId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        var lostComment = _commentsService.RemoveComment(commentId, userInfo.Id);
        return Ok(lostComment);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }



  }
}