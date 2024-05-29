namespace Musbooking.Domain.Http.Responses
{
    internal static class ErrorMessages
    {
        public const string UnknownServerErrorMessage =
            "Something went wrong. This may caused by a technical error that we're working to get fixed";

        public const string ValidationErrorMessage =
            "There is an error(s) found.";

        public const string UnauthorizedMessage = 
            "Unauthorized";
        public const string ServerUnavailableMessage = 
            "Server unavailable";
    }
}