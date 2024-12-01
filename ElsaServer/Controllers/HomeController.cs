using System.Net;
using Elsa.Workflows.Runtime;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Requests;
using ElsaServer.Models;
using ElsaServer.Workflows;
using Microsoft.AspNetCore.Mvc;

namespace ElsaServer.Controllers;

[ApiController]
[Route("/")]
public class HomeController(IWorkflowDispatcher workflowDispatcher) : ControllerBase
{
    [HttpPost]
    [Consumes("application/json", "application/octet-stream")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<ActionResult> Consume(
        [FromBody] TestEvent evt,
        CancellationToken cancellationToken
    )
    {
        await DispatchAndForgetWorkflow(evt, cancellationToken);
        return Accepted();
    }

    private async Task DispatchAndForgetWorkflow(TestEvent evt, CancellationToken cancellationToken)
    {
        var dispatchRequest = new DispatchWorkflowDefinitionRequest
        {
            DefinitionVersionId = $"{evt.GetType().Name}Workflow",
            CorrelationId = evt.EventKey,
            Input = new Dictionary<string, object> { { WorkflowInputKeys.InputEvent, evt } },
        };
        // Fire and Forget
        await workflowDispatcher.DispatchAsync(dispatchRequest, cancellationToken);
    }
}
