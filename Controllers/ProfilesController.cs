using System.Collections.Generic;
using Blogger.Models;
using Blogger.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    private readonly BlogsService _blogsService;
    private readonly CommentsService _commentsService;

    public ProfilesController(ProfilesService profilesService, BlogsService blogsService, CommentsService commentsService)
    {
      _profilesService = profilesService;
      _blogsService = blogsService;
      _commentsService = commentsService;
    }
    [HttpGet("{profileId}")]

    public ActionResult<Profile> GetById(string profileId)
    {
      try
      {
           return Ok(_profilesService.GetById(profileId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/blogs")]

    public ActionResult<List<Blog>> GetBlogsByProfileId(string profileId)
    {
      try
      {
           return Ok(_blogsService.GetBlogsByProfileId(profileId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/comments")]

    public ActionResult<List<Comment>> GetCommentsByProfileId(string profileId)
    {
      try
      {
           return Ok(_commentsService.GetCommentsByProfileId(profileId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }


  }
}