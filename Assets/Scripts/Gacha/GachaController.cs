using System.Collections;
using System.Collections.Generic;
using GachaController.Auth;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace GachaController
{
    class Character
    {
        public int characterId;
    }

    public class GachaController : MonoBehaviour
    {
        [SerializeField]
        private Image receivedGachaCharacterImage;
        
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("OnSend"); 
        }

        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator OnSend()
        {
            //URLをGETで用意
            var webRequest =
                UnityWebRequest.Get(
                    "http://localhost:8000/api/gacha/platinum/");
            //URLに接続して結果が戻ってくるまで待機
            webRequest.SetRequestHeader("Authorization", "Bearer "+(new LoginService()).LoadAccessToken());
            yield return webRequest.SendWebRequest();

            //エラーが出ていないかチェック
            if (webRequest.isNetworkError)
            {
                //通信失敗
                Debug.Log(webRequest.error);
            }
            else
            {
                //通信成功
                Debug.Log(webRequest.downloadHandler.text);
                var text = webRequest.downloadHandler.text;
                var character = JsonUtility.FromJson<Character>(text);
                setImage(character.characterId);
            }
        }


        public void setImage(int characterId)
        {
            string path = "hokuma_" + characterId.ToString();
            Debug.Log(path);
            Sprite sprite = Resources.Load<Sprite>(path);
            /*
            GameObject imageObject = GameObject.Find("Image");

            if (imageObject != null)
            {
                receivedGachaCharacterImage = imageObject.GetComponent<Image>();
            }
            */

            receivedGachaCharacterImage.sprite = sprite;
        }

    }
}