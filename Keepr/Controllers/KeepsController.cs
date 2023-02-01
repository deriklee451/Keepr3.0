namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]


public class KeepsController : ControllerBase
{

    private readonly Auth0Provider _auth0provider;

    private readonly KeepsService _ks;

    public KeepsController(Auth0Provider auth0Provider, KeepsService ks)
    {
        _auth0provider = auth0Provider;
        _ks = ks;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep createdKeep = _ks.Create(keepData);
            // createdKeep.Creator = userInfo;
            return Ok(createdKeep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]

    public ActionResult<List<Keep>> GetAll()
    {
        try
        {
            List<Keep> keeps = _ks.GetAll();
            return Ok(keeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("{keepId}")]

    public async Task<ActionResult<Keep>> GetById(int keepId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);

            var keep = _ks.GetById(keepId, userInfo?.Id);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{keepId}")]
    [Authorize]

    public async Task<ActionResult<Keep>> Edit([FromBody] Keep keepData, int keepId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            keepData.Id = keepId;
            Keep keep = _ks.Edit(keepData, userInfo?.Id);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{keepId}")]
    [Authorize]

    public async Task<ActionResult<string>> Delete(int keepId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _ks.Delete(keepId, userInfo?.Id);
            return Ok("Keep Deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
