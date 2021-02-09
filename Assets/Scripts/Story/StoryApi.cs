using System;
using Common.Api;
using Cysharp.Threading.Tasks;
using GachaController.Auth;
using UnityEngine;
using UnityEngine.Networking;

namespace Story
{
    [Serializable]
    class Stories
    {
        public MasterDataStory[] stories;
    }

    [Serializable]
    public class MasterDataStory
    {
        public int id;
        public string title;
        public string sentences;
        public string created_at;
        public string updated_at;
    }

    public class StoryApi
    {
        public float StoryListDownloadProgress = 0;
        public async UniTask<MasterDataStory[]> GetAll()
        {
            // var request = UnityWebRequest.Get(ApiHostName.instance.hostName + "/api/stories");
            var request = UnityWebRequest.Get("http://localhost:8001" + "/api/stories");
            await request.SendWebRequest().ToUniTask(Progress.Create<float>((x) =>
            {
                StoryListDownloadProgress = x*100; 
                Debug.Log(x*100);
            }));
            var stories = JsonUtility.FromJson<Stories>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            Debug.Log(stories);
            return stories.stories;
        }

        public async UniTaskVoid markStoryAsRead(int storyId)
        {
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes("{\"read_story_id\":" + $"{storyId}" + "}");
            var request = new UnityWebRequest(ApiHostName.instance.hostName + "/api/auth/updateStoryProgress", "POST")
            {
                uploadHandler = (UploadHandler) new UploadHandlerRaw(byteData)
            };
            request.SetRequestHeader("Authorization", "Bearer " + (new LoginService()).LoadAccessToken());
            request.SetRequestHeader("Content-Type", "application/json");
            await request.SendWebRequest();
        }
    }
}