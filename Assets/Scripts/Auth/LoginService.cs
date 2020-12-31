using UnityEngine.Networking;
using System.Collections;
using Common.Api;
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
                var url = ApiHostName.instance.hostName + "/api/auth/login";
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
                if (request.responseCode == 200)
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

        public IEnumerator LogihWithCredential()
        {
            var accessToken = LoadAccessToken();
            var request = UnityWebRequest.Get(ApiHostName.instance.hostName + "/api/auth/me");
            request.SetRequestHeader("Authorization", "Bearer " + accessToken);
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                Debug.Log("true");
                yield return true;
            }
            Debug.Log(request.responseCode);
            if (request.responseCode == 200)
            {
                Debug.Log("true");
                yield return true;
            }
            else
            {
                Debug.Log("false");
                yield return false;
            }
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