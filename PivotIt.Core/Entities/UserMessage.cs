using System.Collections.Generic;

namespace PivotIt.Core.Entities
{
    public class UserMessage : BaseEntity
    {
        public string UserID { get; set; }

        public string CCUsersID { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }
    }

    public class UserMessageAttachment : BaseEntity
    {
        public string AttachmentPath { get; set; }

        public int MessageID { get; set; }
    }
}
