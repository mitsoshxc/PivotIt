using System;
using System.Collections.Generic;
using System.Text;

namespace PivotIt.Core.Entities
{
    public class UserMessage : IBaseEntity
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public string CCUsersID { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }
    }

    public class UserMessageAttachment : IBaseEntity
    {
        public int ID { get; set; }

        public string AttachmentPath { get; set; }

        public int MessageID { get; set; }
    }
}
