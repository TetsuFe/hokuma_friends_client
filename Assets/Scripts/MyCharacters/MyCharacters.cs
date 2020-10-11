using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
class MyCharactersData
{
    public List<CharacterData> myCharacters;
}

[Serializable]
class CharacterData
{
    public int id;
    public string created_at;
    public string updated_at;
    public int user_id;
    public int characterId;
}
public class MyCharacters : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FetchMyCharacters());
    }

    IEnumerator FetchMyCharacters()
    {
        var request = UnityWebRequest.Get("http://localhost:8000/api/myCharacters");
        const string accessTokenKey = "accessTokenKey";
        Debug.Log(PlayerPrefs.GetString(accessTokenKey));
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString(accessTokenKey));
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        var myCharacters = JsonUtility.FromJson<MyCharactersData>(request.downloadHandler.text);
        Debug.Log(request.downloadHandler.text);
        foreach (var e in myCharacters.myCharacters)
        {
            Debug.Log(e.characterId);            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}