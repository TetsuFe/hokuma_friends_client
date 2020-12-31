using UnityEngine;

namespace Story
{
    class MessageProceedManager
    {
        private MessageProceedManager(Sentence[] sentences)
        {
            this.sentences = sentences;
            Debug.Log("instanced");
            _messagesIndex = 0;
        }

        public static MessageProceedManager Instance = new MessageProceedManager(
            new Sentence[]
            {
                new Sentence("", "", "", ""),
            }
            /*
            new Sentence[]
            {
                new Sentence("赤ホクマ", "こんにちはクマ。", "", ""),
                new Sentence("赤ホクマ", "さようならクマ。", "", ""),
            }
            */
        );

        private string oneMessage;
        private Sentence[] sentences;
        private int messageCharIndex = 0;
        private static int _messagesIndex;

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
                if (_messagesIndex < sentences.Length - 1)
                {
                    // SetupNextMessage();
                    return null;
                }
            }

            return oneMessage.Substring(0, messageCharIndex);
        }

        public void SetupNextMessage()
        {
            if (!IsStoryEnded())
            {
                _messagesIndex++;
                if (!IsStoryEnded())
                {
                    oneMessage = sentences[_messagesIndex].body;
                    messageCharIndex = 0;
                }
            }
        }

        public MessageProceedManager UpdateSentences(int storyId)
        {
            return new MessageProceedManager(
                new StoryRepository().Get(storyId)
            );
        }

        public string GetCurrentCharacterName()
        {
            if (!IsStoryEnded())
            {
                return sentences[_messagesIndex].characterName;
            }

            return "";
        }

        public bool IsStoryEnded()
        {
            return _messagesIndex == sentences.Length;
        }
    }
}