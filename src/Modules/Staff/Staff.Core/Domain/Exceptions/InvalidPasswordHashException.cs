using BuildingBlocks.Exceptions;
using System.Net;

namespace Staff.Core.Domain.Exceptions
{
    internal class InvalidPasswordHashException : DreamRoomsException
    {
        internal InvalidPasswordHashException() : base("Password hash cannot be empty!")
        {
        }

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    }
}
