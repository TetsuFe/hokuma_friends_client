using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class QuestBattleController : MonoBehaviour
    {

        [SerializeField] private Text resultText;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            resultText.text = "WIN!";
        }
    }
}