using BuildingBlocks.Exceptions;
using System.Net;

namespace BuildingBlocks.Domain.Exceptions;

internal class TypedIdValueBaseException : DreamRoomsException
{
    internal TypedIdValueBaseException(string message) : base(message)
    {

    }

    protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
