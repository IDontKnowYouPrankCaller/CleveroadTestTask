namespace CleveroadTestProject.Infrastructure.Exceptions
{
    #region namespaces
    using System;
    using System.Runtime.Serialization;
    #endregion

    [Serializable]
    public class ApiException : Exception
    {
        public ResponseCode Code { get; set; }
        public ApiException(ResponseCode code, string message) : base(message)
        {
            this.Code = code;
        }

        public ApiException(ResponseCode code, string message, Exception inner) : base(message, inner)
        {
            this.Code = code;
        }

        protected ApiException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
