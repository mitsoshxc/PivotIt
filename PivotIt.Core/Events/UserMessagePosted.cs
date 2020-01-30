using PivotIt.Core.Entities;

namespace PivotIt.Core.Events
{
    public class UserMessagePosted : BaseDomainEvent
    {
        public UserMessagePosted(UserMessage userMessage)
        {
            UserMessage = userMessage;
        }

        public UserMessage UserMessage { get; private set; }
    }
}
