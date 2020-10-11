using System.Collections;
using System.Collections.Generic;
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
        public Image receivedGachaCharacterImage;

        IEnumerator OnSend()
        {
            //URLをGETで用意
            var webRequest =
                UnityWebRequest.Get(
                    "https://i948gk4k0m.execute-api.ap-northeast-1.amazonaws.com/dev/api/gacha/platinum/");
            //URLに接続して結果が戻ってくるまで待機
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

        public void OnClick()
        {
            // setImage(1);
            StartCoroutine("OnSend");
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

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}