using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions;

internal class InvalidPhoneNumberException : DreamRoomsException
{
    internal InvalidPhoneNumberException(string number) : base($"Phone number {number} isn't valid!")
    {
        
    }

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
