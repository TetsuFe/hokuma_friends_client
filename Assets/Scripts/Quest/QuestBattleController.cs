using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

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
        private Character enemy = new Character(speed: 1, hp: 2);
        private Character myCharacter = new Character(speed: 2, hp: 2);

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame


        double dt = 0.0f;
        private bool isBattleEnded = false;

        void Update()
        {
            if(!isBattleEnded)
            dt += Time.deltaTime;
            if (dt > 0.1f)
            {
                if (((1 / myCharacter.GetSpeed()) * 10) % Convert.ToInt32(dt* 10) == 0)
                {
                    Debug.Log(dt);
                    enemy.hp -= 1;
                }
                if (((1 / enemy.GetSpeed()) * 10) % Convert.ToInt32(dt* 10) == 0)
                {
                    Debug.Log(dt);
                    myCharacter.hp -= 1;
                }

                dt = 0.0f;

                // hp現象の結果に応じて試合結果を表示
                if (enemy.GetHp() == 0)
                {
                    resultText.text = "WIN!";
                    isBattleEnded = true;
                }

                if (myCharacter.GetHp() == 0)
                {
                    resultText.text = "LOSE...";
                    isBattleEnded = true;
                }
            }
        }
    }
}