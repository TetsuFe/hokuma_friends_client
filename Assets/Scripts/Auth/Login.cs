using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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
        Debug.Log("Login Script Start");
        StartCoroutine(Execute());
    }

    IEnumerator Execute()
    {
        // var list = new List<IMultipartFormSection>();
        // var data = new MultipartFormDataSection("email=testtest4@email.com&password=satoshi0224");
        // list.Add(data);
        /*
        var header =new Hashtable();
        header.Add("Content-Type", "application/json; charset=UTF-8");
        */
        var url = "http://localhost:8000/api/auth/login";
        WWWForm form = new WWWForm();
        form.AddField("email", "testtest4@email.com");
        form.AddField("password", "satoshi0224");
        var request = UnityWebRequest.Post(url,form);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
        var loginData = JsonUtility.FromJson<LoginData>(request.downloadHandler.text);
        Debug.Log(loginData.access_token);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
