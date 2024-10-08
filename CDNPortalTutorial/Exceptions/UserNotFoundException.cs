using System.Net;

namespace CDNPortalTutorial.Exceptions
{
    public class UserNotFoundException:BaseException
    {
        public UserNotFoundException(Guid id) : base(
            $"User with ID {id} was not found."
            , HttpStatusCode.NotFound)
        {
        }
    }
}
