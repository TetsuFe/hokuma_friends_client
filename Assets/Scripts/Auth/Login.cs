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
    [SerializeField] private Button moveToSignupButton;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckLoginSuccess());
        submitButton.onClick.AddListener(DoLogin);
        moveToSignupButton.onClick.AddListener(LoadSignupScehe);
    }

    IEnumerator CheckLoginSuccess()
    {
    var loginService = new LoginService();
    var coroutine = loginService.LogihWithCredential();
    yield return StartCoroutine(coroutine);
    var isSuccess = (bool) coroutine.Current;
        if (isSuccess)
        {
            LoadTitleScene();
        }
        else
        {
            Debug.Log("not logged in");
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
            SceneManager.LoadScene("MenuScene");
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

    void LoadSignupScehe()
    {
        SceneManager.LoadScene("Signup");
    }
}