using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

class LoginData
{
    public string expire_in;
    public string access_token;
    public string token_type;
}

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LoadAccessToken() == null)
        {
            StartCoroutine(Execute());
        }
        else
        {
            Debug.Log(LoadAccessToken());
            LoadMainScene();
        }
    }

    IEnumerator Execute()
    {
        var url = "http://localhost:8000/api/auth/login";
        WWWForm form = new WWWForm();
        form.AddField("email", "testtest4@email.com");
        form.AddField("password", "satoshi0224");
        var request = UnityWebRequest.Post(url,form);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
        var loginData = JsonUtility.FromJson<LoginData>(request.downloadHandler.text);
        SaveAccessToken(loginData.access_token);
        var accessToken = LoadAccessToken();
        Debug.Log(accessToken);
    }
    
        const string AccessTokenKey = "accessTokenKey";
    void SaveAccessToken(string accessTokan)
    {
        PlayerPrefs.SetString(AccessTokenKey, accessTokan);
    }

    string LoadAccessToken()
    {
        return 
            PlayerPrefs.GetString(AccessTokenKey);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
