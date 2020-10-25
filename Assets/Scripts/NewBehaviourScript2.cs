using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour
{
    /// <summary>
    /// 縦横比を無視して全画面にするか
    /// </summary>
    [SerializeField]
    private bool isAllScreen = true;


    private void Start()
    {
        Vector2 standardResolution = GetComponent<RectTransform>().sizeDelta;

        if (isAllScreen)
        {
            //縦横比率を変更してでも全画面に合わせる。
            float magnification_x = 0;
            float magnification_y = 0;

            float height = Screen.height;
            magnification_y = height / standardResolution.y;
            float width = Screen.width;
            magnification_x = width / standardResolution.x;

            GetComponent<RectTransform>().sizeDelta = new Vector2(standardResolution.x * magnification_x, standardResolution.y * magnification_y);
        }
    }
}
