namespace Keepr.Services;

public class VaultsService
{

    private readonly VaultsRepository _repo;

    private readonly VaultKeepsRepository _vkRepo;

    public VaultsService(VaultsRepository repo, VaultKeepsRepository vkRepo)
    {
        _repo = repo;
        _vkRepo = vkRepo;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        return _repo.CreateVault(vaultData);
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repo.GetVaultById(vaultId);
        if (vault.Id == 0)
        {
            throw new Exception("Not Your Vault");
        }
        if (vault == null)
        {
            throw new Exception("Invalid Vault ID");
        }
        if (vault.IsPrivate == true)
        {
            if (vault.CreatorId != userId)
            {
                throw new Exception("Not your vault");
            }
        }
        return vault;
    }


    internal Vault EditVault(Vault vault, string userId)
    {
        var original = GetVaultById(vault.Id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("Not Yours To Edit");
        }

        original.Name = vault.Name ?? original.Name;
        original.Img = vault.Img ?? original.Img;
        original.Description = vault.Description ?? original.Description;
        original.IsPrivate = vault.IsPrivate ?? original.IsPrivate;

        var updated = _repo.EditVault(original);
        return updated;
    }

    internal void DeleteVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("Not Your Vault");
        }
        _repo.DeleteVault(vaultId);
    }

    internal List<KeepInVault> GetKeepsByVaultId(int vaultId, string id)
    {
        Vault vault = GetVaultById(vaultId, id);

        List<KeepInVault> keeps = _vkRepo.GetKeepsByVaultId(vaultId);
        return keeps;
    }


}
