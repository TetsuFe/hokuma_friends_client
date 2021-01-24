using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Story
{
    public class StoryController : MonoBehaviour
    {
        [SerializeField] private Text messageArea;
        [SerializeField] private Text characterName;
        [SerializeField] private Canvas canvas;
        [SerializeField] private TestBackgroundImage prefab;


        private double dt = 0.0f;
        private MessageProceedManager _messageProceedManager = MessageProceedManager.Instance;
        public int storyId;

        // Start is called before the first frame update
        void Start()
        {
            SetupBackgroundImage();
            SetupMessages();
            Debug.Log(storyId);
        }

        // Update is called once per frame
        void Update()
        {
            dt += Time.deltaTime;
            if (dt > 0.1f)
            {
                if (_messageProceedManager.IsStoryEnded())
                {
                    SceneManager.LoadScene("StoryListScene");
                    new StoryApi().markStoryAsRead(storyId);
                }
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

        void SetupBackgroundImage()
        {
            // Instantiate(prefab).transform.SetParent(canvas.transform);
            var handle = Addressables.LoadAssetAsync<GameObject>("Assets/TestBackgroundImage.prefab");
            handle.Completed += (h) =>
            {
                var instance = Instantiate(h.Result);
                instance.transform.SetParent(canvas.transform, false);
                instance.transform.SetAsFirstSibling();
            };
        }

        public void SetupNextMessage()
        {
            messageArea.text = "";
        }
    }
}