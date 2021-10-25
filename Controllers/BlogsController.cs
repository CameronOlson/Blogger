using System.Collections.Generic;
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
  public class BlogsController : ControllerBase
  {
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;

    public BlogsController(BlogsService blogsService, CommentsService commentsService)
    {
      _blogsService = blogsService;
      _commentsService = commentsService;
    }

    [HttpGet]
    public ActionResult<List<Blog>> GetAll()
    {
      try
      {
       return Ok(_blogsService.GetAll());
      }
      catch (System.Exception e)
      {
       return BadRequest(e.Message);
      }
    }
    [HttpGet("{blogId}")]
    public ActionResult<Blog> GetById(int blogId)
    {
      try
      {
       return Ok(_blogsService.GetById(blogId));
      }
      catch (System.Exception e)
      {
       return BadRequest(e.Message);
      }
    }
    [HttpGet("{blogId}/comments")]
    public ActionResult<List<Comment>> GetBlogComments(int blogId)
    {
      try
      {
       var comments = _commentsService.GetByBlogId(blogId);
       return Ok(comments);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Blog>> CreateBlog([FromBody] Blog blogData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        blogData.CreatorId = userInfo.Id;
        Blog createBlog = _blogsService.Post(blogData);
        createBlog.Creator = userInfo;
       return Ok(createBlog);
      }
      catch (System.Exception e)
      {
       return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPut("{blogId}")]
    public async Task<ActionResult<Blog>> Update(int blogId, [FromBody] Blog blogData)
    {
      try
      {
       Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       blogData.CreatorId = userInfo.Id;
        var blog = _blogsService.Update(blogData, blogId);
       return Ok(blog);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{blogId}")]
    [Authorize]

    public ActionResult<Blog> DeleteBlog(int blogId)
    {
      try
      {
       var blog = _blogsService.Delete(blogId);
       return Ok(blog);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}