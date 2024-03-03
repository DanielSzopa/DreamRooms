using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidPasswordException : DreamRoomsException
{
    internal InvalidPasswordException() : base("Password should be between 8 and 20 characters!!!")
    {
    }

    protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
