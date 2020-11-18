using UnityEngine;

namespace Story
{

    public class MessageArea : MonoBehaviour
    {
         public void OnClickHander()
         {
             Debug.Log("clicked");
             MessageProceedManager.Instance.SetupNextMessage();
         }
    }
}