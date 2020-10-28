using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class LoginData
{
    public string expire_in;
    public string access_token;
    public string token_type;
}

public class AccessTokenKey
{
 public static string accessTokenKey = "accessTokenKey";   
}

public class Login : MonoBehaviour
{
    [SerializeField] private InputField userIdField;

    [SerializeField] private InputField passwordField;
    [SerializeField] private Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        if (LoadAccessToken() ==
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
            LoadMyCharactersScene();
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
        Debug.Log(email);
        Debug.Log(password);
        if (email != "" && password != "")
        {
            var url = "http://localhost:8000/api/auth/login";
            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);
            var request = UnityWebRequest.Post(url, form);
            yield return request.SendWebRequest();
            // Debug.Log(request.downloadHandler.text);
            var loginData = JsonUtility.FromJson<LoginData>(request.downloadHandler.text);
            // Debug.Log(request.downloadHandler.text);
            SaveAccessToken(loginData.access_token);
            Debug.Log(loginData.access_token);
            var accessToken = LoadAccessToken();
            Debug.Log(accessToken);
            SceneManager.LoadScene("LoginScene");
        }
    }


    void SaveAccessToken(string accessToken)
    {
        PlayerPrefs.SetString(AccessTokenKey.accessTokenKey, accessToken);
    }

    string LoadAccessToken()
    {
        return
            PlayerPrefs.GetString(AccessTokenKey.accessTokenKey);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void LoadMyCharactersScene()
    {
        SceneManager.LoadScene("MyCharactersScene");
    }

    // Update is called once per frame
    void Update()
    {
    }
}