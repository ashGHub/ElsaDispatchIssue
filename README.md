# How to reproduce

1. Run `Elsa Server`
2. Go to workflow designer and adjust the layout and publish it... you can publish it as many times as you want
3. Run the endpoint found at `endpoints.http`

### Checkout `HomeController` to see how it is being dispatched


## What is Happening

The workflow dispatcher is only dispatching `Version 1` of the workflow, even though the layout has been updated and published multiple times.

## Expected Behavior

The workflow dispatcher should dispatch the latest version of the workflow.