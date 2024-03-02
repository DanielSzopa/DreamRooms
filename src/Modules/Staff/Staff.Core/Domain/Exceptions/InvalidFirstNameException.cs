using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidFirstNameException : DreamRoomsException
{
    protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    internal InvalidFirstNameException(string firstName) : base($"Invalid firstName, firstName is empty or too short: invalid value {firstName}") 
    {
        
    }
}
