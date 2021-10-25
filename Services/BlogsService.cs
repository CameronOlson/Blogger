using System;
using System.Collections.Generic;
using Blogger.Models;
using Blogger.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Services
{
  public class BlogsService
  {
    private readonly BlogsRepository _blogsRepository;
    private readonly ProfilesService _profilesService;

    public BlogsService(BlogsRepository blogsRepository, ProfilesService profilesService)
    {
      _blogsRepository = blogsRepository;
      _profilesService = profilesService;
    }

    public List<Blog> GetAll()
    {
      return _blogsRepository.GetAll();
    }

    public Blog GetById(int blogId)
    {
      Blog foundBlog = _blogsRepository.GetById(blogId);
      if(foundBlog == null)
      {
        throw new Exception("Unable to find Blog");
      }
      return foundBlog;
    }

    public Blog Post(Blog blogData)
    {
      return _blogsRepository.Post(blogData);
    }

    internal object GetBlogsByProfileId(string profileId)
    {
      return _blogsRepository.GetByProfileId(profileId);
    }

    public Blog Update(Blog blogData, int userId)
    {
      var blog = GetById(userId);
      blog.Title =blogData.Title ?? blog.Title;
      blog.Body = blogData.Body ?? blog.Body;
      blog.imgUrl = blogData.imgUrl ?? blog.imgUrl;
      blog.published = blogData.published;
      

      _blogsRepository.Update(userId, blogData);
      return blog;

    //  Blog foundBlog = GetById(blogData.Id);
    //  if(foundBlog.CreatorId == null)
    //  {
    //    throw new Exception("you aren't allowed to do that");
    //  }
    //  foundBlog.Id = blogData.Id;
    //  return _blogsRepository.Update(foundBlog);
    }

    public List<Blog> GetBlogsByAccount(string userId)
    {
      return _blogsRepository.GetBlogsByAccount(userId);
    }

    public Blog Delete(int blogId)
    {
      var blog = GetById(blogId);
      _blogsRepository.Delete(blogId);
      return blog;
    }
  }
}