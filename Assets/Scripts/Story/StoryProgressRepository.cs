using Common.Api;
using Cysharp.Threading.Tasks;
using GachaController.Auth;
using Story.Models;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Networking;

namespace Story
{
    public class StoryProgressRepository
    {
        public async UniTask<StoryProgress> Get()  
        {
            var request = UnityWebRequest.Get(ApiHostName.instance.hostName + "/api/auth/storyProgress");
            request.SetRequestHeader("Authorization", "Bearer "+(new LoginService()).LoadAccessToken());
            await request.SendWebRequest();
            var storyProgress = JsonUtility.FromJson<StoryProgress>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            return storyProgress;
        }
    }
}