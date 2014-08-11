using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Brilliantech.Framwork.Message
{
    [DataContract] 
    public class BaseMessage
    {
        protected bool result = false;
        protected List<string> messages; 

        public BaseMessage()
        {
            this.result = false;
            this.messages = new List<string>();
        }

        [DataMember]
        public bool Result
        {
            get { return result; }
            set { result = value; }
        }

        [DataMember]
        public List<string> Messages
        {
            get { return messages; }
            set { messages = value; }
        }
         
        public string GetMessageContent()
        {
            return string.Join(((char)10).ToString(), this.messages.ToArray());
        }
    }
}
