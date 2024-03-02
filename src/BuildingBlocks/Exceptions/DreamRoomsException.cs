using System.Net;

namespace BuildingBlocks.Exceptions;

public abstract class DreamRoomsException : Exception
{
	protected abstract HttpStatusCode HttpStatusCode { get; }
    protected DreamRoomsException(string message) : base(message)
	{

	}
}
