using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Elsa.Workflows.Activities.Flowchart.Models;
using ElsaServer.Models;

namespace ElsaServer.Workflows;

public class TestEventWorkflow : WorkflowBase
{
    private PrintInputActivity _printInputActivity = default!;
    private WriteLine _writeLine2 = default!;
    private WriteLine _writeLine3 = default!;

    protected override void Build(IWorkflowBuilder builder)
    {
        CreateWorkflowInputs(builder);
        CreateWorkflowActivity();
        builder.Id = nameof(TestEventWorkflow); //<--- added for definition version id
        builder.Root = new Flowchart
        {
            Activities = { _printInputActivity, _writeLine2, _writeLine3 },
            Connections =
            {
                new Connection(_printInputActivity, _writeLine2),
                new Connection(_writeLine2, _writeLine3),
            },
        };
    }

    private static void CreateWorkflowInputs(IWorkflowBuilder builder)
    {
        var eventInput = builder.WithInput<TestEvent>(WorkflowInputKeys.InputEvent);
        eventInput.StorageDriverType = typeof(WorkflowInstanceStorageDriver);
    }

    private void CreateWorkflowActivity()
    {
        _printInputActivity = new PrintInputActivity();
        _writeLine2 = new WriteLine("Test Me 2");
        _writeLine3 = new WriteLine("Test Me 3");
    }
}
