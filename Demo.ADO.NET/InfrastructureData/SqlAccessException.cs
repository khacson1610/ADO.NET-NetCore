using System;
using System.Runtime.Serialization;

namespace InfrastructureData
{
    [Serializable]
    internal class SqlAccessException : Exception
    {
        public SqlAccessException()
        {
        }

        public SqlAccessException(string message) : base(message)
        {
        }

        public SqlAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SqlAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
