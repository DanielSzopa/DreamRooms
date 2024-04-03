using BuildingBlocks.Exceptions;
using System.Net;

namespace Reservations.Core.Domain.Exceptions;
internal class InvalidFullNameException : DreamRoomsException
{
    internal InvalidFullNameException(string fullName) : base($"FullName with value {fullName} is wrong, value should has min 4 characters")
    {
    }

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
