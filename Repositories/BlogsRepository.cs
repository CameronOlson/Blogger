using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Repositories
{
  public class BlogsRepository
  {
    private readonly IDbConnection _db;

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Blog> GetAll()
    {
      string sql = @"
      SELECT
      b.*
      FROM blogs b
      WHERE b.published = true;
      ";
      return _db.Query<Blog>(sql).ToList();
    }

    public Blog GetById(int blogId)
    {
      string sql = @"
      SELECT
      b.*,
      a.*
      FROM blogs b
      JOIN accounts a on b.creatorId = a.id
      WHERE b.id = @blogId;
      ";
      return _db.Query<Blog, Account, Blog>(sql, (b, a) =>
      {
        b.Creator = a;
        return b;
      }, new{blogId}).FirstOrDefault();
    }

    internal Blog Post(Blog blogData)
    {
      string sql = @"
      INSERT INTO blogs(creatorId, published, imgUrl, body, title)
      VALUES(@CreatorId, @Published, @ImgUrl, @Body, @Title);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, blogData);
      blogData.Id = id;
      return blogData;
    }

    internal object GetByProfileId(string profileId)
    {
      var sql = @"
      SELECT *
      FROM blogs b
      WHERE b.creatorId = @profileId AND published = true
      ";
      return _db.Query<Blog>(sql, new { profileId }).ToList();
    }

    internal ActionResult<Blog> Update(int userId, Blog foundBlog)
    {
      foundBlog.Id = userId;
      string sql = @"
      UPDATE blogs
      SET
      title = @Title,
      body = @Body,
      imgUrl = @ImgUrl,
      creatorId = @CreatorId,
      published = @published

      WHERE id = @Id;";
      var rowsAffected = _db.Execute(sql, foundBlog);
      if(rowsAffected == 0)
      {
        throw new Exception("Update Failed");
      }
      return foundBlog;
    }

    internal List<Blog> GetBlogsByAccount(string userId)
    {
      string sql = "SELECT * FROM blogs b WHERE b.creatorId = @userId";
      return _db.Query<Blog>(sql, new{ userId }).ToList();
    }

    internal void Delete(int id)
    {

      var rowsAffected = _db.Execute("DELETE FROM blogs WHERE id = @id", new { id });
      if (rowsAffected > 1)
      {
        throw new System.Exception("this is really bad");  
      }
    }
  }
}