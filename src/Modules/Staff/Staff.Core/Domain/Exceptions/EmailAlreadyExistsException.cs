using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class EmailAlreadyExistsException : DreamRoomsException
{
    internal EmailAlreadyExistsException(string email) : base($"Cannot use the {email} because already exists")
    {
    }

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
