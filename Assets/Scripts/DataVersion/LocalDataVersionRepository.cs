using UnityEngine;

namespace MasterDataVersion
{
    public class LocalDataVersionRepository
    {
        private readonly string localDataVersionKey = "localDataVersionKey";
        public int GetLocalDataVersion()
        {
            return PlayerPrefs.GetInt(localDataVersionKey);
        }

        public void UpdateLocalDataVersion(int version)
        {
            PlayerPrefs.SetInt(localDataVersionKey, version);
        }
    }
}