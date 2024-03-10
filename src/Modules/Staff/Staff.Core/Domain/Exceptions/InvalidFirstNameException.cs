using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidFirstNameException : DreamRoomsException
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    internal InvalidFirstNameException(string firstName) : base($"Invalid firstName, firstName is empty or too short: invalid value {firstName}") 
    {
        
    }
}
