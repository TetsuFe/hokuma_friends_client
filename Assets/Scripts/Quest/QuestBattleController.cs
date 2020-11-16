﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

class Character
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

namespace Quest
{
    public class QuestBattleController : MonoBehaviour
    {
        [SerializeField] private Text resultText;
        [SerializeField] private Text myCharacterHpText;
        [SerializeField] private Text enemyHpText;
        [SerializeField] private Button nextButton;

        private Character[] enemies = new Character[] {new Character(speed: 10, hp: 2), new Character(speed: 10, hp: 2),};

        private Character[] myCharacters = new Character[]
            {new Character(speed: 1, hp: 2), new Character(speed: 1, hp: 2)};

        public int questId;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("questId: " + questId);
        }

        // Update is called once per frame


        double dt = 0.0f;
        private bool isBattleEnded = false;

        void Update()
        {
            if (!isBattleEnded)
            {
                dt += Time.deltaTime;
                if (dt > 0.1f)
                {
                    // 攻撃をしかけるキャラを決める
                    int characterIndex = CanCharacterAttacks(myCharacters);
                    if(characterIndex != -1)
                    {
                        // 攻撃対象を決める
                        int enemyIndex = DecideAttackObject(enemies);
                        // 攻撃計算をする
                        Debug.Log("Playerの攻撃！");
                        Debug.Log(dt);
                        enemies[enemyIndex].hp -= 1;
                        enemyHpText.text = "HP: " + enemies[enemyIndex].hp.ToString();
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
                        int enemyCharacterIndex = CanCharacterAttacks(enemies);
                        if (enemyCharacterIndex != -1)
                        {
                            // 攻撃対象を決める
                            int allyIndex = DecideAttackObject(myCharacters);
                            // 攻撃計算をする
                            Debug.Log("NPCの攻撃！");
                            Debug.Log(dt);
                            myCharacters[allyIndex].hp -= 1;
                            myCharacterHpText.text = "HP: " + myCharacters[allyIndex].hp;
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

        int CanCharacterAttacks(Character[] characters){
            var characterIndex = 0;
            foreach (var character in characters)
            {
                if (((1 / character.GetSpeed()) * 10) % Convert.ToInt32(dt * 10) == 0)
                {
                    return characterIndex;
                }
                characterIndex++;
            }
            return -1;
        }

        int DecideAttackObject(Character[] characters)
        {
            int i = 0;
            foreach (var character in characters)
            {
                if (character.hp > 0)
                {
                    return i;
                }
                i++;
            }
            return -1;
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