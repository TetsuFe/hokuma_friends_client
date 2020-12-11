using Cysharp.Threading.Tasks;

namespace DataVersion
{
    public class MasterDataVersionApi
    {
        public async UniTask<int> GetMasterDataVersion()
        {
            return 2;
        }
    }
}