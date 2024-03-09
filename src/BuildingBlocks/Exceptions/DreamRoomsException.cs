using System.Net;

namespace BuildingBlocks.Exceptions;

public abstract class DreamRoomsException : Exception
{
	public abstract HttpStatusCode HttpStatusCode { get; }
    protected DreamRoomsException(string message) : base(message)
	{

	}
}
