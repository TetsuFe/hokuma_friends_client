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

        private Character enemy = new Character(speed: 1, hp: 2);
        private Character myCharacter = new Character(speed: 2, hp: 2);
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
                    if (((1 / myCharacter.GetSpeed()) * 10) % Convert.ToInt32(dt * 10) == 0)
                    {
                        Debug.Log(dt);
                        enemy.hp -= 1;
                        enemyHpText.text = "HP: " + enemy.hp.ToString();
                    }

                    // hp現象の結果に応じて試合結果を表示
                    if (enemy.GetHp() == 0)
                    {
                        resultText.text = "WIN!";
                        isBattleEnded = true;
                        SendBattleResult(1, true);
                    }

                    if (!isBattleEnded)
                    {
                        if (((1 / enemy.GetSpeed()) * 10) % Convert.ToInt32(dt * 10) == 0)
                        {
                            Debug.Log(dt);
                            myCharacter.hp -= 1;
                            myCharacterHpText.text = "HP: " + myCharacter.hp.ToString();
                        }

                        if (myCharacter.GetHp() == 0)
                        {
                            resultText.text = "LOSE...";
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