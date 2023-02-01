

namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    internal Keep Create(Keep keepData)
    {
        return _repo.Create(keepData);
    }

    internal List<Keep> GetAll()
    {
        return _repo.GetAll();
    }

    internal Keep GetById(int keepId, string userId)
    {
        Keep keep = _repo.GetById(keepId);
        if (keep == null)
        {
            throw new Exception("Invalid ID");
        }
        if (keep.CreatorId != userId)
        {
            keep.Views++;
            Update(keep);
        }
        return keep;
    }

    internal Keep Edit(Keep keep, string accoungId)
    {
        Keep original = GetById(keep.Id, accoungId);
        if (original.CreatorId != accoungId)
        {
            throw new Exception("Not Your Keep");
        }
        original.Name = keep.Name ?? original.Name;
        original.Img = keep.Img ?? original.Img;
        original.Description = keep.Description ?? original.Description;

        var updated = _repo.Edit(original);
        return updated;
    }

    public void Update(Keep keep)
    {
        _repo.Edit(keep);
    }

    internal void Delete(int KeepId, string userId)
    {
        var keep = GetById(KeepId, userId);
        if (keep.CreatorId != userId)
        {
            throw new Exception("Unauthorized to Delete Keep");
        }
        _repo.Delete(KeepId);
    }


}
