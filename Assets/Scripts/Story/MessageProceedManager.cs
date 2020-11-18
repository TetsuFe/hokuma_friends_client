namespace Story
{
    class MessageProceedManager
    {
        private MessageProceedManager()
        {
        }

        public static readonly MessageProceedManager Instance = new MessageProceedManager();

        private string oneMessage;
        private string[] messages;
        private int messageCharIndex = 0;
        int messagesIndex = 0;

        public void SetupMessages()
        {
            messages = new[] {"こんにちはクマ！", "さようならクマ！"};
            oneMessage = messages[0];
        }

        public string GetCurrentPartialMessage()
        {
            if (messageCharIndex < oneMessage.Length)
            {
                messageCharIndex++;
            }
            else if (messageCharIndex == oneMessage.Length)
            {
                if (messagesIndex < messages.Length - 1)
                {
                    // SetupNextMessage();
                    return null;
                }
            }

            return oneMessage.Substring(0,messageCharIndex);
        }

        public void SetupNextMessage()
        {
            if (messagesIndex < messages.Length-1)
            {
                messagesIndex++;
                oneMessage = messages[messagesIndex];
                messageCharIndex = 0;
            }
        }
    }
}