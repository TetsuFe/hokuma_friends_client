using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Story
{
    public class StoryListController : MonoBehaviour
    {
        [SerializeField] private Button moveToStoryButton;
        [SerializeField] private Button backToMenuButton;

        void Start()
        {
            moveToStoryButton.onClick.AddListener(LoadStoryScene);
            backToMenuButton.onClick.AddListener(LoadMenuScene);
        }

        // Update is called once per frame
        void Update()
        {
        }

        void LoadStoryScene()
        {
            SceneManager.LoadScene("StoryScene");
        }

        void LoadMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}