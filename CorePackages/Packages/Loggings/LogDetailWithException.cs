namespace Packages.Loggings
{
    public class LogDetailWithException : LogDetail
    {
        public LogDetailWithException()
        {
            ExceptionMessage = string.Empty;
        }

        public LogDetailWithException(string fullName,
            string methodName,
            string user,
            List<LogParameter> parameters,
            string exceptionMessage) : base(fullName, methodName, user, parameters)
        {
            ExceptionMessage = exceptionMessage;
        }

        public string ExceptionMessage { get; set; }
    }
}
