using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions
{
    internal class InvalidPasswordHashException : DreamRoomsException
    {
        internal InvalidPasswordHashException() : base("Password hash cannot be empty!")
        {
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    }
}
