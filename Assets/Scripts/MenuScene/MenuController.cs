using System;
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
    // Start is called before the first frame update
    void Start()
    {
        moveToMyCharactersSceneButton.onClick.AddListener(
            LoadMyCharactersScene
        );
        moveToGachaListSceneButton.onClick.AddListener(
            LoadGachaListScene
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMyCharactersScene()
    {
        SceneManager.LoadScene("MyCharactersScene");
    }
    
    public void LoadGachaListScene()
    {
        SceneManager.LoadScene("GachaListScene");
    }
}
