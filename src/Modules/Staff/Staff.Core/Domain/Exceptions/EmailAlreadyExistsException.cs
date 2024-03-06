using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class EmailAlreadyExistsException : DreamRoomsException
{
    internal EmailAlreadyExistsException(string email) : base($"Cannot use the {email} because already existes")
    {
    }

    protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
