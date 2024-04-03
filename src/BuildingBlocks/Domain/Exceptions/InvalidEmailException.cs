using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

public class InvalidEmailException : DreamRoomsException
{
    public InvalidEmailException(string email) : base($"Provided email: {email} is invalid!")
    {
    }

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
