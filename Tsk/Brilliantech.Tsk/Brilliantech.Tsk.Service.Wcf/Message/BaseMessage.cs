using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Brilliantech.Tsk.Service.Wcf.Message
{
    [DataContract]
    public class BaseMessage
    {
        private bool result = false;
        private List<string> messages;
        private string messageContent;

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
 
        public string MessageContent
        {
            get {
                return string.Join(((char)10).ToString(), this.messages.ToArray());
            }
        }
    }
}
