namespace Keepr.Repositories;

public class KeepsRepository : BaseRepository
{
    public KeepsRepository(IDbConnection db) : base(db)
    { }


    internal Keep Create(Keep keepData)
    {
        string sql = @"
            INSERT INTO
            keeps (name, description, img, creatorId)
            VALUES
            (@Name, @Description, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
        int KeepId = _db.ExecuteScalar<int>(sql, keepData);

        return GetById(KeepId);
    }

    internal List<Keep> GetAll()
    {
        string sql = @"
        SELECT 
        k.*,
        COUNT(vk.id) AS Kept,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId   
        LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
        GROUP BY k.id
        ORDER BY k.createdAt DESC
        ";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, Profile) =>
        {
            keep.Creator = Profile;
            return keep;
        }).ToList();

    }

    internal Keep Edit(Keep data)
    {
        string sql = @"
        UPDATE keeps SET
        name = @Name,
        img = @Img,
        description = @Description,
        views = @Views
        WHERE id = @Id LIMIT 1
        ";
        var rows = _db.Execute(sql, data);
        if (rows != 1)
        {
            throw new Exception("Unable to update!");
        }

        return data;
    }


    internal void Delete(int id)
    {
        string sql = @"
        DELETE FROM
        keeps
        WHERE
        id = @Id
        ";

        var rows = _db.Execute(sql, new { id });
        if (rows != 1)
        {
            throw new Exception("Unable to Delete");
        }
        return;
    }

    internal Keep GetById(int keepId)
    {
        string sql = @"
        SELECT
        k.*,
        COUNT(vk.id) AS Kept,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
        WHERE k.id = @keepId    
        GROUP BY k.id
        ";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { keepId }).FirstOrDefault();
    }



}
