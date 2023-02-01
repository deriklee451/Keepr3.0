namespace Keepr.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _proRepo;

    public ProfilesService(ProfilesRepository proRepo)
    {
        _proRepo = proRepo;
    }

    internal Profile GetProfileById(string profileId)
    {
        return _proRepo.GetProfileById(profileId);
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        return _proRepo.GetKeepsByProfileId(profileId);
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        return _proRepo.GetVaultsByProfileId(profileId);
    }

    internal List<Profile> GetAllProfiles(Account userInfo)
    {
        return _proRepo.GetAllProfiles();
    }


}
