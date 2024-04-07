

using MassTransit;

namespace Api;


public class Filter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("filterExample");
        context.Add("test", "testValue");
    }

    public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        return next.Send(context);
    }
}
