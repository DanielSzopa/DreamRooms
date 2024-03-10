using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidEmailException : DreamRoomsException
{
    internal InvalidEmailException(string email) : base($"Provided email: {email} is invalid!")
    {
    }

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
