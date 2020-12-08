namespace Story
{
    class MessageProceedManager
    {
        private MessageProceedManager(Sentence[] sentences)
        {
            this.sentences = sentences;
        }

        public static readonly MessageProceedManager Instance = new MessageProceedManager(
            new Sentence[]
            {
                new Sentence("赤ホクマ", "こんにちはクマ。", "", ""),
                new Sentence("赤ホクマ", "さようならクマ。", "", ""),
            }
        );

        private string oneMessage;
        private Sentence[] sentences;
        private int messageCharIndex = 0;
        int messagesIndex = 0;

        public void SetupMessages()
        {
            oneMessage = sentences[0].body;
        }

        public string GetCurrentPartialMessage()
        {
            if (messageCharIndex < oneMessage.Length)
            {
                messageCharIndex++;
            }
            else if (messageCharIndex == oneMessage.Length)
            {
                if (messagesIndex < sentences.Length - 1)
                {
                    // SetupNextMessage();
                    return null;
                }
            }

            return oneMessage.Substring(0,messageCharIndex);
        }

        public void SetupNextMessage()
        {
            if (messagesIndex < sentences.Length-1)
            {
                messagesIndex++;
                oneMessage = sentences[messagesIndex].body;
                messageCharIndex = 0;
            }
        }
    }
}