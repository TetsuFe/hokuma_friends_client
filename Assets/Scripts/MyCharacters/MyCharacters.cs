using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[Serializable]
class MyCharactersData
{
    // public List<CharacterData> myCharacters;
    public List<int> myCharacters;
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
    [SerializeField] Canvas canvas;
    [SerializeField] private Button backToMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        backToMenuButton.onClick.AddListener(LoadMenuScene);
        StartCoroutine(FetchMyCharacters());
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
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
        int i = 0;
        foreach (var e in myCharacters.myCharacters)
        {
            Debug.Log(e);
            GameObject imgObject = new GameObject("testAAA");

            RectTransform trans = imgObject.AddComponent<RectTransform>();
            trans.transform.SetParent(canvas.transform); // setting parent
            trans.localScale = Vector3.one;
            trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
            trans.sizeDelta = new Vector2(150, 200); // custom size

            Image image = imgObject.AddComponent<Image>();
            imgObject.transform.position = new Vector3(imgObject.transform.position.x + i * 150,
                imgObject.transform.position.y, 1f);

            imgObject.transform.SetParent(canvas.transform);
            string path = "hokuma_" + e;
            image.sprite = Resources.Load<Sprite>(path);
            i++;
        }
    }

    private Image image;

    // Update is called once per frame
    void Update()
    {
    }
}