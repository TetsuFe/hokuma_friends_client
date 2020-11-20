using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Dialog
{
    public class LoginErrorDialog : MonoBehaviour
    {
        public void OnOk()
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("LoginScene");
        }
    }
}
