using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidRoleException : DreamRoomsException
{
    public InvalidRoleException() : base("Role cannot be empty!")
    {
    }

    protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
