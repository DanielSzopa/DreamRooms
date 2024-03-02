using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidLastNameException : DreamRoomsException
{
    protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    internal InvalidLastNameException(string lastName) : base($"Invalid lastName, lastName is empty or too short: invalid value {lastName}") 
    {
        
    }
}
