using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quest
{
    public class QuestListController : MonoBehaviour
    {
        [SerializeField] private Button backToMenuButton;

        [SerializeField] private Button moveToQuestBattleButton;
        [SerializeField] private Canvas canvas;
        [SerializeField] private ListButtonItem listButtonItem;

        // Start is called before the first frame update
        void Start()
        {
            backToMenuButton.onClick.AddListener(LoadMenuScene);
            moveToQuestBattleButton.onClick.AddListener(LoadQuestBattleScene);
            SetUpQuestList();
        }

        void SetUpQuestList()
        {
            var repository = new QuestRepository();
            var quests = repository.GetAll();
            Debug.Log(quests);

            int i = 1;
            foreach (var quest in quests)
            {
                var obj = Instantiate(listButtonItem);
                obj.transform.localPosition = new Vector3(0, -100*i);
                var text = obj.GetComponentInChildren<Text>();
                text.text = quest.name;
                Debug.Log(quest.name);
                Debug.Log(i);
                obj.transform.SetParent(canvas.transform, false);
                i++;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void LoadMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
        }

        private void LoadQuestBattleScene()
        {
            SceneManager.sceneLoaded += QuestBattleSceneLoaded;
            SceneManager.LoadScene("QuestBattleScene");
        }

        private void QuestBattleSceneLoaded(Scene next, LoadSceneMode mode)
        {
            // シーン切り替え後のスクリプトを取得
            var controller = GameObject.FindWithTag("QuestBattleControllerObject")
                .GetComponent<QuestBattleController>();

            // データを渡す処理
            controller.questId = 1;

            // イベントから削除
            SceneManager.sceneLoaded -= QuestBattleSceneLoaded;
        }
    }
}