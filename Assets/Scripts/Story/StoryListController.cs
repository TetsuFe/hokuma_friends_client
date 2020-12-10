﻿using System.Collections;
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

        void Start()
        {
            backToMenuButton.onClick.AddListener(LoadMenuScene);
            SetupStoryListView();
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

        void SetupStoryListView()
        {
            var storyList = new Story[] { new Story(id:1, new Sentences()), new Story(id:1, new Sentences())};
            int i = 0;
            foreach (var story in storyList)
            {
                var storyListButtonObj = Instantiate(storyListButton);
                storyListButtonObj.transform.localPosition = new Vector3(0, -50*i);
                var button = storyListButtonObj.GetComponent<Button>();
                button.onClick.AddListener(LoadStoryScene);
                storyListButtonObj.transform.SetParent(canvas.transform, false);
                i++;
            }
        }
    }
}