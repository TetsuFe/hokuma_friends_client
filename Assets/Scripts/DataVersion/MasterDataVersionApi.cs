using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace DataVersion
{
    [Serializable]
    public class StoryMasterDataVersion
    {
        public int version;
    }
    public class MasterDataVersionApi
    {
        public async UniTask<int> GetStoryMasterDataVersion()
        {
            var request = UnityWebRequest.Get("http://localhost:8001/api/storyMasterDataVersion/");
            await request.SendWebRequest();
            return JsonUtility.FromJson<StoryMasterDataVersion>(request.downloadHandler.text).version;
        }
    }
}