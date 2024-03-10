using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidRoleException : DreamRoomsException
{
    internal InvalidRoleException() : base("Role cannot be empty!")
    {
    }

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
