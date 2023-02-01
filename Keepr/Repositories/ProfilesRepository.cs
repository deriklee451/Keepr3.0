namespace Keepr.Repositories;

public class ProfilesRepository : BaseRepository
{
    public ProfilesRepository(IDbConnection db) : base(db)
    {
    }

    internal Profile GetProfileById(string id)
    {
        string sql = @"
    SELECT *
    FROM
    accounts
    WHERE id = @id
    ;";
        return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
    }


    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        var sql = @"
        SELECT 
        k.*,
        COUNT(vk.id) AS Kept,
        a.* 
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = k.Id
        WHERE k.creatorId =  @profileId
        GROUP BY k.id
        ;";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { profileId }).ToList();
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        var sql = @"
        SELECT 
        v.*,
        a.* 
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.creatorId =  @profileId
        AND v.isPrivate = false
        GROUP BY v.id
        ;";
        return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { profileId }).ToList();

    }

    internal List<Profile> GetAllProfiles()
    {
        string sql = @"
    SELECT
    * FROM
    accounts
    ;";
        return _db.Query<Profile>(sql).ToList();
    }

    internal Profile UpdateProfile(Profile profileData)
    {
        string sql = @"
    UPDATE Profiles
    SET
    name = @name,
    picture = @picture
    WHERE id = @id
    ;";

        _db.Execute(sql, profileData);
        return profileData;
    }

}
