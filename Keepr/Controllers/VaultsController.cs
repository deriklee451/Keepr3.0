namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{

    private readonly Auth0Provider _auth0provider;

    private readonly VaultsService _vs;

    private readonly VaultKeepsService _vks;

    public VaultsController(Auth0Provider auth0Provider, VaultsService vs, VaultKeepsService vks)
    {
        _auth0provider = auth0Provider;
        _vs = vs;
        _vks = vks;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vs.CreateVault(vaultData);
            vault.Creator = userInfo;
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);


            Vault vault = _vs.GetVaultById(vaultId, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<KeepInVault>>> GetKeepsByVaultId(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<KeepInVault> vaultKeeps = _vs.GetKeepsByVaultId(vaultId, userInfo?.Id);
            return Ok(vaultKeeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.Id = vaultId;
            Vault vault = _vs.EditVault(vaultData, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _vs.DeleteVault(vaultId, userInfo?.Id);
            return Ok("Vault deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }


    }








}
