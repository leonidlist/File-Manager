using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    [Serializable]
    public class NoResultException : Exception
    {
        public NoResultException() { }
        public NoResultException(string message) : base(message) { }
        public NoResultException(string message, Exception inner) : base(message, inner) { }
        protected NoResultException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
