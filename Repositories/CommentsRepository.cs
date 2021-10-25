using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;

namespace Blogger.Repositories
{
  public class CommentsRepository
  {
    private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Comment CreateComment(Comment commentData)
    {
      var sql =@"
        INSERT INTO comments(
          body,
          blog,
          creatorId
        )
        VALUES (
          @Body,
          @Blog,
          @CreatorId
        );
        SELECT LAST_INSERT_ID();
      ";
      //  @"
      // INSERT INTO comments(body, blogId, creatorId)
      // VALUES (@Body, @BlogId, @CreatorId);
      // SELECT LAST_INSERT_ID();
      // ";
      var id = _db.ExecuteScalar<int>(sql, commentData);
      commentData.Id = id;
      return commentData;
    }

    internal object GetById(int commentId)
    {
      string sql = @"
      SELECT
      c.*,
      a.*
      FROM comments c
      JOIN accounts a on c.creatorId = a.id
      WHERE c.id = @commentId;
      ";
      return _db.Query<Comment, Account, Comment>(sql, (c, a) =>
      {
        c.Creator = a;
        return c;
      }, new{commentId}).FirstOrDefault();
    }

    internal object GetByProfileId(string profileId)
    {
         var sql = @"
      SELECT *
      FROM comments c
      WHERE c.creatorId = @profileId
      ";
      return _db.Query<Blog>(sql, new { profileId }).ToList();
    }

    internal List<Comment> GetByBlogId(int blog)
    {
      var sql = @"
      SELECT * FROM comments c
      WHERE c.blog = @blog;";
      return _db.Query<Comment>(sql, new { blog }).ToList();
    }

    public Comment UpdateCommentById(int commentId, Comment commentData, string creatorId)
    {
      commentData.CreatorId = creatorId;
      commentData.Id = commentId;
      var sql = @"
      UPDATE comments
      SET
      body = @Body,
      blog = @Blog,
      creatorId = @CreatorId
      WHERE id = @Id
      ";
      var rowsAffected = _db.Execute(sql, commentData);

      if (rowsAffected > 1)
      {
        throw new System.Exception("SOMEONE GO TELL THE DB ADMIN WE DONE MESSED UP");
      }
      if (rowsAffected == 0)
      {
        throw new System.Exception("The update failed");
      }
      return commentData;
    }

    internal object RemoveComment(int commentId)
    {
      string sql = "DELETE FROM comments WHERE id = @commentId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {commentId});
      if(affectedRows == 0)
      {
        throw new Exception("Nah");
      }
      return new Exception("comment Has Been Deleted.");
    }
  }
}