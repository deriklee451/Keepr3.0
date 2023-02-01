namespace Keepr.Services;

public class VaultKeepsService
{

    private readonly VaultKeepsRepository _vkRepo;
    private readonly VaultsService _vs;

    public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsService vs)
    {
        _vkRepo = vkRepo;
        _vs = vs;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
        Vault vault = _vs.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("bad vault id");
        }


        var kepts = _vkRepo.CreateVaultKeep(vaultKeepData);

        return kepts;
    }


    internal List<VaultKeep> GetAllVaultKeeps()
    {
        return _vkRepo.GetAllVaultKeeps();
    }

    internal void DeleteVaultKeep(int vaultKeepId, string accountId)
    {
        var vk = GetVaultKeepById(vaultKeepId);
        if (vk.CreatorId != accountId)
        {
            throw new Exception("Unauthorized to delete VaultKeep");
        }
        _vkRepo.DeleteVaultKeep(vaultKeepId);
    }


    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        var vk = _vkRepo.GetVaultKeepById(vaultKeepId);
        if (vk == null)
        {
            throw new Exception("Bad VaultKeep Id");
        }
        return vk;
    }



}
