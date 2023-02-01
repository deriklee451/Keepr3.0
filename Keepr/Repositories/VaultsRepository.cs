namespace Keepr.Repositories;

public class VaultsRepository : BaseRepository
{
    public VaultsRepository(IDbConnection db) : base(db)
    {

    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
            INSERT INTO
            vaults
            (name, description, img, isPrivate, creatorId)
            VALUES
            (@Name, @Description, @Img, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
        vaultData.CreatedAt = DateTime.Now;
        vaultData.UpdatedAt = DateTime.Now;
        vaultData.Id = _db.ExecuteScalar<int>(sql, vaultData);
        return vaultData;
    }



    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.id = @vaultId;
            ";
        return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { vaultId }).FirstOrDefault();
    }

    internal Vault EditVault(Vault data)
    {
        string sql = @"
        UPDATE vaults
        SET 
        name = @Name,
        img = @Img,
        description = @Description,
        isPrivate = @IsPrivate
        WHERE id = @Id
        LIMIT 1
        ";
        var rows = _db.Execute(sql, data);
        if (rows != 1)
        {
            throw new Exception("Unable to Update");
        }
        return GetVaultById(data.Id);
    }


    internal void DeleteVault(int id)
    {
        string sql = @"
        DELETE
        FROM
        vaults
        WHERE id = @id  
        ";
        var rows = _db.Execute(sql, new { id });
        if (rows != 1)
        {
            throw new Exception("Invalid");
        }
        return;
    }


}
