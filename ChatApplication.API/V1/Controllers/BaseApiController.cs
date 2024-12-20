using Asp.Versioning;
using ChatApplication.Shared.V1.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.API.V1.Controllers;

[ApiController]
[ApiVersion("1")]
[Route(ApiConstants.IngressRealTimePrefix + "/v{version:apiVersion}/[controller]")]
public class BaseApiController :ControllerBase
{
}
