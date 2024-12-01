using Elsa.Extensions;
using Elsa.Workflows;
using ElsaServer.Models;

namespace ElsaServer.Workflows;

public class PrintInputActivity : CodeActivity
{
    protected override void Execute(ActivityExecutionContext context)
    {
        var inputEvent = context.GetWorkflowInput<TestEvent>(WorkflowInputKeys.InputEvent);
        Console.WriteLine(inputEvent.EventKey);
    }
}
