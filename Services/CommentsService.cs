using System;
using System.Collections.Generic;
using Blogger.Controllers;
using Blogger.Models;
using Blogger.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Services
{
  public class CommentsService
  {
    private readonly CommentsRepository _commentsRepository;
    private readonly ProfilesService _profilesService;
    private readonly BlogsService _blogsService;
public CommentsService(CommentsRepository commentsRepository,ProfilesService profilesService,BlogsService blogsService)
    {
      _commentsRepository = commentsRepository;
      _blogsService = blogsService;
      _profilesService = profilesService;
    }

    public Comment CreateComment(Comment commentData)
    {
      // var blogId = commentData.Blog;
      // var blog = _blogsService.GetById(blogId);

      return _commentsRepository.CreateComment(commentData);
    }

    public Comment GetById(int commentId)
    {
      Comment foundComment = (Comment)_commentsRepository.GetById(commentId);
      if(foundComment == null)
      {
        throw new Exception("Unable to find Comment, ID = 0");
      }
      return foundComment;
    }
    public List<Comment> GetByBlogId(int blogId)
    {
      // var newBlog = _blogsService.GetById(blogId);
      return _commentsRepository.GetByBlogId(blogId);
    }

    internal object GetCommentsByProfileId(string profileId)
    {
      var profile = _profilesService.GetById(profileId);
      return _commentsRepository.GetByProfileId(profileId);
    }

    internal ActionResult<Comment> Update(Comment updatedComment, int commentId, string creatorId)
    {
      var comment = GetById(commentId);
      comment.Body = updatedComment.Body ?? comment.Body;
      comment.Blog = updatedComment.Blog;
      comment.Title = updatedComment.Title ?? comment.Title;
      comment.Published = updatedComment.Published;
       _commentsRepository.UpdateCommentById(commentId, comment, creatorId);
      return comment;

    }

    internal object RemoveComment(int commentId, string id)
    {
     Comment foundComment = GetById(commentId);
     if(foundComment.CreatorId != id)
     {
       throw new Exception ("not your comment");
     }
     return _commentsRepository.RemoveComment(commentId);
    }
  }
}