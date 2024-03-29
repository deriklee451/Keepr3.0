namespace Keepr.Repositories;

public class AccountsRepository
{
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Account GetByEmail(string userEmail)
    {
        string sql = "SELECT * FROM accounts WHERE email = @userEmail";
        return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
        string sql = "SELECT * FROM accounts WHERE id = @id";
        return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal Account Create(Account newAccount)
    {
        string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
        _db.Execute(sql, newAccount);
        return newAccount;
    }

    internal Account Edit(Account update)
    {
        string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture,
              coverImg = @CoverImg
            WHERE id = @Id;";
        _db.Execute(sql, update);
        return update;
    }

    internal List<Keep> GetMyKeeps(string accountId)
    {
        var sql = @"
          SELECT 
            k.*,
            COUNT(vk.id) AS Kept,
            a.* 
          FROM keeps k
          JOIN accounts a ON a.id = k.creatorId
          LEFT JOIN vaultKeeps vk ON vk.keepId = k.Id
          WHERE k.creatorId =  @accountId
          GROUP BY k.id
          ;";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { accountId }).ToList();
    }

    internal List<Vault> GetMyVaults(string accountId)
    {
        var sql = @"
    SELECT 
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE v.creatorId = @accountId
    GROUP BY v.id
    ;";

        return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { accountId }).ToList();
    }
}
