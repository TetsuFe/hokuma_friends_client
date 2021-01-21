using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Story
{
    public class StoryListController : MonoBehaviour
    {
        [SerializeField] private StoryListButton storyListButton;
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private Canvas canvas;
        private int storyId;

        void Start()
        {
            backToMenuButton.onClick.AddListener(LoadMenuScene);
            SetupStoryListView();
        }

        // Update is called once per frame
        void Update()
        {
        }

        void LoadMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
        }

        void LoadStoryScene(int storyId)
        {
            SceneManager.sceneLoaded += StorySceneLoaded;
            SceneManager.LoadScene("StoryScene");
            this.storyId = storyId;
        }

        private void StorySceneLoaded(Scene next, LoadSceneMode mode)
        {
            var controller = GameObject.FindWithTag("StoryControllerObject")
                .GetComponent<StoryController>();
            controller.storyId = storyId;
            // イベントから削除
            SceneManager.sceneLoaded -= StorySceneLoaded;
        }

        async void SetupStoryListView()
        {
            var storyList = new StoryRepository().GetAll();
            var storyProgress = await new StoryProgressRepository().Get();
            int i = 0;
            foreach (var story in storyList)
            {
                var storyListButtonObj = Instantiate(storyListButton);
                var text = storyListButtonObj.GetComponentInChildren<Text>();
                text.text = story.title;
                storyListButtonObj.transform.localPosition = new Vector3(0, -50*i);
                var button = storyListButtonObj.GetComponent<Button>();
                Debug.Log(storyProgress.latest_readable);
                if (story.id <= storyProgress.latest_readable)
                {
                    button.onClick.AddListener(()=>LoadStoryScene(story.id));
                }
                else
                {
                    button.interactable = false;
                }
                storyListButtonObj.transform.SetParent(canvas.transform, false);
                i++;
            }
        }
    }
}