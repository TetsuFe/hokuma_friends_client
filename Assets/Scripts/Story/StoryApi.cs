using System;
using Common.Api;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Story
{
    [Serializable]
    class Stories
    {
        public Story2[] stories;
    }

    public class StoryApi
    {
        public async UniTask<Story2[]> GetAll()
        {
            // var request = UnityWebRequest.Get(ApiHostName.instance.hostName + "/api/stories");
            var request = UnityWebRequest.Get("http://localhost:8001" + "/api/stories");
            await request.SendWebRequest();
            var stories = JsonUtility.FromJson<Stories>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            Debug.Log(stories);
            return stories.stories;
        }
    }
}