namespace App.Domain.Events;

public record ProductAddEvent(int Id, string Name, decimal Price) : IEventOrMessage;
