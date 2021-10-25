using System;
using Blogger.Models;
using Blogger.Repositories;

namespace Blogger.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepository;
    public ProfilesService(ProfilesRepository profilesRepository)
    {
      _profilesRepository = profilesRepository;
    }

    internal object GetById(string profileId)
    {
      Profile foundProfile = _profilesRepository.GetById(profileId);
      if(foundProfile == null)
      {
        throw new Exception("Doesn't exist");
      }
      return foundProfile;
    }
  }
}