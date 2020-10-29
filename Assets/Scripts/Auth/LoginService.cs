using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GachaController.Auth

{
    public class AccessTokenKey
    {
        public static string accessTokenKey = "accessTokenKey";
    }

    class LoginData
    {
        public string expire_in;
        public string access_token;
        public string token_type;
    }

    public class LoginService
    {
        public IEnumerator Login(string email, string password)
        {
            Debug.Log(email);
            Debug.Log(password);
            if (email == "" || password == "")
            {
                yield return false;
            }
            else
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
                if (request.result == UnityWebRequest.Result.Success)
                {
                    yield return true;
                }
                else
                {
                    yield return false;
                }
            }
        }

        public bool CheckLoggedIn()
        {
            return true;
        }


        public void SaveAccessToken(string accessToken)
        {
            PlayerPrefs.SetString(AccessTokenKey.accessTokenKey, accessToken);
        }

        public string LoadAccessToken()
        {
            return
                PlayerPrefs.GetString(AccessTokenKey.accessTokenKey);
        }
    }
}