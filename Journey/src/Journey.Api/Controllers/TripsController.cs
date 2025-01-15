using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        RegisterTripUseCase useCase = new RegisterTripUseCase();

        ResponseShortTripJson response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        GetAllTripsUseCase useCase = new GetAllTripsUseCase();

        ResponseTripsJson result = useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id) 
    {
        GetTripByIdUseCase useCase = new GetTripByIdUseCase();

        ResponseTripJson response = useCase.Execute(id);

        return Ok(response);
    } 

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        DeleteTripByIdUseCase useCase = new DeleteTripByIdUseCase();

        useCase.Execute(id);

        return NoContent();
    }

    [HttpPost]
    [Route("{tripId:guid}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
    {
        RegisterActivityForTripUseCase useCase = new RegisterActivityForTripUseCase();

        ResponseActivityJson response = useCase.Execute(tripId, request);

        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{tripId:guid}/activity/{activityId:guid}/complete")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        CompleteActivityForTripUseCase useCase = new CompleteActivityForTripUseCase();

        useCase.Execute(tripId, activityId);

        return NoContent();
    }

    [HttpDelete]
    [Route("{tripId:guid}/activity/{activityId:guid}")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        DeleteActivityForTripUseCase useCase = new DeleteActivityForTripUseCase();

        useCase.Execute(tripId, activityId);

        return NoContent();
    }
}
