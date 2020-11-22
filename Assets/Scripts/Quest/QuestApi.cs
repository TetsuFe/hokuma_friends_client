using System;
using api;
using UnityEngine;
using Cysharp.Threading.Tasks;
using GachaController.Auth;
using UnityEngine.Networking;

namespace Quest
{
    [Serializable]
    public class QuestResultData
    {
        public QuestResultData(int questId, bool isCleared)
        {
            this.questId = questId;
            this.isCleared = isCleared;
        }

        public int questId;
        public bool isCleared;
    }

    public class QuestApi
    {
        public async UniTask<bool> PostQuestResult(int questId, bool isCleared)
        {
            var data = new QuestResultData(questId, isCleared);
            var json = JsonUtility.ToJson(data);
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(json);
            
            var url = ApiHostName.instance.hostName + "/api/questResult/updateQuestClearResult";
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(byteData);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            
            var accessToken = (new LoginService()).LoadAccessToken();
            request.SetRequestHeader("Authorization", "Bearer " + accessToken);
            request.SetRequestHeader("Content-Type", "application/json");

            try
            {
                await request.SendWebRequest();
            }
            catch (UnityWebRequestException e)
            {
                return false;
            }
            
            return request.responseCode == 200;
        }
    }
}