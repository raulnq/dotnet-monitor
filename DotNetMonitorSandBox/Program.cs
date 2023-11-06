using System.Diagnostics.Tracing;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("/", () =>
{
    app.Logger.LogInformation("Hello World!");
    MyEventSource.Log.Request("Hello World!");
    return "Hello World!";
}
);

app.Run();

[EventSource(Name = "MyEventSource")]
public sealed class MyEventSource : EventSource
{
    public static MyEventSource Log { get; } = new MyEventSource();

    private EventCounter _counter;

    public MyEventSource()
    {
        _counter = new EventCounter("my-custom-counter", this)
        {
            DisplayName = "my-custom-counter",
            DisplayUnits = "ms"
        };
    }

    [Event(1, Level = EventLevel.Informational)]
    public void Request(string message)
    {
        WriteEvent(1, message);
        _counter.WriteMetric(1);
    }
}