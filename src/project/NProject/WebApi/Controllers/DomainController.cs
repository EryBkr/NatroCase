using Application.Features.FavoriteDomains.Commands.AddFavoriteDomainByUserId;
using Application.Features.FavoriteDomains.Commands.DeleteFavoriteDomainByUserId;
using Application.Features.FavoriteDomains.Queries;
using Application.Features.Rdap.Queries.RdapQuery;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DomainController : BaseController
    {
        [HttpGet("domain/check/{domainName}")]
        public async Task<IActionResult> GetDomainInfo(string domainName)
        {
            var result = await Mediator.Send(new RdapQuery(domainName));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("AddFavoriteDomain")]
        public async Task<IActionResult> AddFavoriteDomain([FromBody] AddFavoriteDomainByUserIdCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response.Id);
        }

        [HttpDelete("remove/{id:guid}")]
        public async Task<IActionResult> RemoveFavoriteDomain(Guid id)
        {
            var command = new DeleteFavoriteDomainByUserIdCommand(id);
            var response = await Mediator.Send(command);

            return Ok(response.message);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamicQuery, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetListByDynamicFavoriteDomainQuery { PageRequest = pageRequest, DynamicQuery = dynamicQuery }, cancellationToken));
        }
    }
}
