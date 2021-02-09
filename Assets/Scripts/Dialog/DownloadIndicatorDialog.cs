using Story;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DownloadIndicatorDialog : MonoBehaviour
    {
        float progress => StoryRepository.instance.StoryListDownloadProgress;
        private void Update()
        {
            UpdateIndicator();
        }

        void UpdateIndicator()
        {
            this.transform.Find("IndicatorText").GetComponent<Text>().text = progress.ToString()+"%";
            if (progress.Equals(100))
            {
                Destroy(gameObject);
            }
        }
    }
}