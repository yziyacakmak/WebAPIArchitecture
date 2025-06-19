using App.Domain.Events;
using MassTransit;

namespace App.Bus.Consumers
{
    public class ProductAddedEventConsumer : IConsumer<ProductAddEvent>
    {
        public Task Consume(ConsumeContext<ProductAddEvent> context)
        {
            Console.WriteLine($"Product Added: Id={context.Message.Id}, Name={context.Message.Name}, Price={context.Message.Price}");

            return Task.CompletedTask;
        }
    }
}
