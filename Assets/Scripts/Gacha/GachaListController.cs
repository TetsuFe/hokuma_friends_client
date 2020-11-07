using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GachaListController : MonoBehaviour
{

    [SerializeField] private Button drawOneTimeButton;
    // Start is called before the first frame update
    void Start()
    {
        drawOneTimeButton.onClick.AddListener(LoadGachaScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadGachaScene()
    {
        SceneManager.LoadScene("GachaScene");
    }
}
