using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;


namespace Quest
{
    
public class Character
{
    public Character(int speed, int hp)
    {
        this.speed = speed;
        this.hp = hp;
    }

    private int speed;
    public int hp;

    public int GetSpeed()
    {
        return speed;
    }

    public int GetHp()
    {
        return hp;
    }
}
    public class QuestBattleController : MonoBehaviour
    {
        [SerializeField] private Text resultText;
        [SerializeField] private Text myCharacterHpText;
        [SerializeField] private Text enemyHpText;
        [SerializeField] private Text myCharacter2HpText;
        [SerializeField] private Text enemy2HpText;
        [SerializeField] private Button nextButton;
        
        Text[] allyHpTexts;
        Text[] enemyHpTexts;

        private Character[] enemies = new Character[] {new Character(speed: 1, hp: 20), new Character(speed: 1, hp: 20),};

        private Character[] myCharacters = new Character[]
            {new Character(speed: 5, hp: 20), new Character(speed: 5, hp: 20)};

        public int questId;

        private BattleRule battleRule = new BattleRule();

        // Start is called before the first frame update
        void Start()
        {
            allyHpTexts = new[] {myCharacterHpText, myCharacter2HpText};
            enemyHpTexts = new[] {enemyHpText, enemy2HpText};
            allyHpTexts[0].text = "HP: " + myCharacters[0].hp;
            allyHpTexts[1].text = "HP: " + myCharacters[1].hp;
            enemyHpTexts[0].text = "HP: " + enemies[0].hp;
            enemyHpTexts[1].text = "HP: " + enemies[1].hp;
        }

        // Update is called once per frame


        double dt = 0.0f;
        private bool isBattleEnded = false;

        private double adt;
        void Update()
        {
            if (!isBattleEnded)
            {
                dt += Time.deltaTime;
                if (dt > 0.1f)
                {
                    adt += dt;
                    if (adt > 1)
                    {
                        adt = 0;
                    }
                    // 攻撃をしかけるキャラを決める
                    int characterIndex = battleRule.CanCharacterAttacks(myCharacters, adt);
                    if(characterIndex != -1)
                    {
                        // 攻撃対象を決める
                        int enemyIndex = battleRule.DecideAttackObject(enemies);
                        // 攻撃計算をする
                        Debug.Log("Playerの攻撃！");
                        Debug.Log(dt);
                        enemies[enemyIndex].hp -= 1;
                        enemyHpTexts[enemyIndex].text = "HP: " + enemies[enemyIndex].hp.ToString();
                    }

                    // hp現象の結果に応じて試合結果を表示
                    // 勝利条件
                    bool winFlag = true;
                    foreach (var enemy in enemies)
                    {
                        if (enemy.GetHp() > 0)
                        {
                            winFlag = false;
                        }
                    }

                    if (winFlag)
                    {
                        resultText.text = "WIN!";
                        isBattleEnded = true;
                        SendBattleResult(1, true);
                    }

                    if (!isBattleEnded)
                    {
                        // 攻撃をしかけるキャラを決める
                        int enemyCharacterIndex =  battleRule.CanCharacterAttacks(enemies, adt);
                        if (enemyCharacterIndex != -1)
                        {
                            // 攻撃対象を決める
                            int allyIndex =  battleRule.DecideAttackObject(myCharacters);
                            // 攻撃計算をする
                            Debug.Log("NPCの攻撃！");
                            Debug.Log(dt);
                            myCharacters[allyIndex].hp -= 1;
                            allyHpTexts[allyIndex].text = "HP: " + myCharacters[allyIndex].hp;
                        }

                        bool loseFlag = true;
                        foreach (var ally in myCharacters)
                        {
                            if (ally.GetHp() > 0)
                            {
                                loseFlag = false;
                            }
                        }

                        if (loseFlag)
                        {
                            resultText.text = "LOSE..";
                            isBattleEnded = true;
                            SendBattleResult(1, false);
                        }
                    }
                    dt = 0.0f;
                }
            }
        }


        async void SendBattleResult(int questId, bool isCleared)
            {
                var api = new QuestApi();
                var returnedValue = await api.PostQuestResult(questId, isCleared);
                if (returnedValue)
                {
                    EnableNextButton();
                }
            }

            void EnableNextButton()
            {
                nextButton.interactable = true;
                nextButton.onClick.AddListener(() => SceneManager.LoadScene("QuestListScene"));
            }
        }
    }