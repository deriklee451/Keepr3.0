namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;

    private readonly VaultKeepsService _vks;

    public VaultKeepsController(Auth0Provider auth0Provider, VaultKeepsService vks)
    {
        _auth0provider = auth0Provider;
        _vks = vks;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);

            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _vks.CreateVaultKeep(vaultKeepData, userInfo?.Id);
            return Ok(vaultKeep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _vks.DeleteVaultKeep(vaultKeepId, userInfo?.Id);
            return Ok("VaultKeep deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet]
    public ActionResult<List<VaultKeep>> GetAllVaultKeeps()
    {
        try
        {
            List<VaultKeep> example = _vks.GetAllVaultKeeps();
            return Ok(example);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
