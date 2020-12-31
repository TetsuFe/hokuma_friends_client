using System;
using System.Collections;
using System.Collections.Generic;
using Common.Api;
using UnityEngine;
using Cysharp.Threading.Tasks;
using GachaController.Auth;
using UnityEngine.Networking;

namespace Quest
{
    [Serializable]
    class QuestResultsData
    {
        public List<QuestResultData> questResults;
    }

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
        private LoginService loginService = new LoginService();

        public async UniTask<bool> PostQuestResult(int questId, bool isCleared)
        {
            var data = new QuestResultData(questId, isCleared);
            var json = JsonUtility.ToJson(data);
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(json);

            var url = ApiHostName.instance.hostName + "/api/questResult/updateQuestClearResult";
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(byteData);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();

            var accessToken = loginService.LoadAccessToken();
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

        public async UniTask<List<QuestResultData>> GetAllQuestResult()
        {
            var url = ApiHostName.instance.hostName + "/api/questResult/index";
            var request = UnityWebRequest.Get(url);
            var accessToken = loginService.LoadAccessToken();
            request.SetRequestHeader("Authorization", "Bearer " + accessToken);
            request.SetRequestHeader("Content-Type", "application/json");
            try
            {
                var result = await request.SendWebRequest();
                return JsonUtility.FromJson<QuestResultsData>(result.downloadHandler.text).questResults;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
    }
}