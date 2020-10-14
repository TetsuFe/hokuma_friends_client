using System.Collections;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GachaController.Auth
{

    class Token
    {
        public string token;
    }
    public class SignupController : MonoBehaviour
    {
        [SerializeField] private InputField emailField;

        [SerializeField] private InputField usernameField;
        [SerializeField] private InputField passwordField;
        [SerializeField] private Button submitButton;


        void Start()
        {
            PlayerPrefs.DeleteAll();
        }

        public void handleSubmitButtonTapped()
        {
            StartCoroutine("Signup");
        }

        IEnumerator Signup()
        {
            var email = emailField.text;
            var username = usernameField.text;
            var password = passwordField.text;
            if (email != "" && username != "" && password != "")
            {
                var form = new WWWForm();
                form.AddField("email", email);
                form.AddField("name", username);
                form.AddField("password", password);
                var url = "http://localhost:8000/api/register";
                var response = UnityWebRequest.Post(url, form);
                yield return response.Send();
                var accessToken = JsonUtility.FromJson<Token>(response.downloadHandler.text);
                PlayerPrefs.SetString(AccessTokenKey.accessTokenKey, accessToken.token);
                SceneManager.LoadScene("LoginScene");
            }
        }
    }
}