namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]


public class ProfilesController : ControllerBase
{

    private readonly Auth0Provider _auth0Provider;
    private readonly ProfilesService _ps;


    public ProfilesController(ProfilesService ps, Auth0Provider auth0Provider)
    {
        _auth0Provider = auth0Provider;
        _ps = ps;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Profile profile = _ps.GetProfileById(profileId);
            return Ok(profile);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
        try
        {
            List<Keep> profileKeeps = _ps.GetKeepsByProfileId(profileId);
            return Ok(profileKeeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByProfileId(string profileId)
    {
        try
        {
            List<Vault> profileKeeps = _ps.GetVaultsByProfileId(profileId);
            return Ok(profileKeeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Profile>>> GetAllProfiles()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<Profile> profile = _ps.GetAllProfiles(userInfo);
            return Ok(profile);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }






}
