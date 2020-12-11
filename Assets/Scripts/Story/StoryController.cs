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


        private double dt = 0.0f;
        private MessageProceedManager _messageProceedManager = MessageProceedManager.Instance;
        public int storyId;

        // Start is called before the first frame update
        void Start()
        {
            SetupMessages();
            Debug.Log(storyId);
        }

        // Update is called once per frame
        void Update()
        {
            dt += Time.deltaTime;
            if (dt > 0.1f)
            {
                var message = _messageProceedManager.GetCurrentPartialMessage();
                var characterNameText = _messageProceedManager.GetCurrentCharacterName();
                if (characterNameText != null)
                {
                    characterName.text = characterNameText;
                }
                if (message != null)
                {
                    messageArea.text = message;
                }

                dt = 0.0f;
            }
        }

        void SetupMessages()
        {
            messageArea.text = "";
            _messageProceedManager = _messageProceedManager.UpdateSentences(storyId);
            _messageProceedManager.SetupMessages();
        }

        public void SetupNextMessage()
        {
            messageArea.text = "";
        }
    }
}