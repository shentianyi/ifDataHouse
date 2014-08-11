using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Brilliantech.Framwork.Message
{
    [DataContract]
    public class ProcessMessage : BaseMessage
    {
        public ProcessMessage()
            : base()
        {
        }



        public string ToString()
        {
            return string.Join(((char)10).ToString(), this.messages.ToArray());
        }
    }
}
