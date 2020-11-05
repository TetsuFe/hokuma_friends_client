using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using GachaController.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Login : MonoBehaviour
{
    [SerializeField] private InputField userIdField;

    [SerializeField] private InputField passwordField;
    [SerializeField] private Button submitButton;


    // Start is called before the first frame update
    void Start()
    {
        
        
    var loginService = new LoginService();
        if (loginService.LoadAccessToken() ==
            "")
        {
            Debug.Log("not logged in");
            // DoLogin();
        }
        else
        {
            //PlayerPrefs.DeleteAll();
            // Debug.Log(LoadAccessToken());
            // LoadMainScene();
            // LoadMyCharactersScene();
            LoadTitleScene();
        }
    }

    public void DoLogin()
    {
        StartCoroutine("ExecuteLogin");
    }

    IEnumerator ExecuteLogin()
    {
        var email = userIdField.text;
        var password = passwordField.text;
        var loginService = new LoginService();
        var coroutine = loginService.Login(email, password);
        yield return StartCoroutine(coroutine);
        var isSuccess = (bool) coroutine.Current;
        if (isSuccess)
        {
        }
        else
        {
            Debug.Log("Login is failed");
        }
    }
    

    void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void LoadMyCharactersScene()
    {
        SceneManager.LoadScene("MyCharactersScene");
    }

    void LoadTitleScene()
    {
            SceneManager.LoadScene("TitleScene");
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}