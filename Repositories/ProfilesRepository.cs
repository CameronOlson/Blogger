using System;
using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;

namespace Blogger.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string profileId)
    {
      string sql = "SELECT * FROM accounts WHERE id = @profileId";
      return _db.QueryFirstOrDefault<Profile>(sql, new { profileId });
    }
  }
}