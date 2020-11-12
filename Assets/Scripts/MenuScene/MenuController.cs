﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private Button moveToMyCharactersSceneButton;
    [SerializeField]
    private Button moveToGachaListSceneButton;

    [SerializeField] private Button moveToQuestSceneButton;
    // Start is called before the first frame update
    void Start()
    {
        moveToMyCharactersSceneButton.onClick.AddListener(
            LoadMyCharactersScene
        );
        moveToGachaListSceneButton.onClick.AddListener(
            LoadGachaListScene
        );
        moveToQuestSceneButton.onClick.AddListener(LoadQuestScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadMyCharactersScene()
    {
        SceneManager.LoadScene("MyCharactersScene");
    }
    
    private void LoadGachaListScene()
    {
        SceneManager.LoadScene("GachaListScene");
    }

    private void LoadQuestScene()
    {
        SceneManager.LoadScene("QuestListScene");
    }
}
