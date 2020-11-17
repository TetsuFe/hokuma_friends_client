using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class StoryController : MonoBehaviour
    {
        [SerializeField] private Text messageArea;
        [SerializeField] private Text characterName;

        private string oneMessage;
        private string[] messages;

        private int messageCharIndex = 0;
        int messagesIndex = 0;
        private double dt = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            SetupMessages();
            characterName.text = "赤ホクマ";
        }

        // Update is called once per frame
        void Update()
        {
            dt += Time.deltaTime;
            if (dt > 0.1f)
            {
                if (messageCharIndex < oneMessage.Length)
                {
                    messageArea.text += oneMessage[messageCharIndex];
                    messageCharIndex++;
                }
                else if (messageCharIndex == oneMessage.Length)
                {
                    if (messagesIndex < messages.Length - 1)
                    {
                        SetupNextMessage();
                    }
                }

                dt = 0.0f;
            }
        }

        void SetupMessages()
        {
            messageArea.text = "";
            messages = new[] {"こんにちはクマ！", "さようならクマ！"};
            oneMessage = messages[0];
        }

        void SetupNextMessage()
        {
            messagesIndex++;
            oneMessage = messages[messagesIndex];
            messageCharIndex = 0;
            messageArea.text = "";
        }
    }
}