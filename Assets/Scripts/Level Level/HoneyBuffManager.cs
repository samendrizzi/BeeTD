using UnityEngine;
using System;

public enum HoneyBuff 
{
    EMPTY,
    QUEENTHORNS,
    QUEENARMOR,
    HONEYDROPRATE,
    HONEYINTEREST,
    POLLENGATHERRATE,
    POLLENBUFF,
    NECTARGENERATIONRATE,
    HONEYCOMBTICKS,
    HONEYCOMBGENERATIONRATE,
    HONEYCOMBFILLTIME,
    SLOWPOWER,
    SLOWPIERCE,
    FREEZEPOWER,
    FREEZEPIERCE,
    BEEHEAL,
    BEESHIELD,
    BEESHIELDGENERATION,
    BEETARGETINGRANGE,
    BEEDAMAGE,
    BEEATTACKRATE,
    BEEACTIONPOWER,
    BEEACTIONRATE,
    BEEEFFECTPOWER,
    BEEEFFECTRATE,
    BEECARRYCAPACITY,
    BEEMOVESPEED,
    BEEARMOR,
    BEEREISTANCE,
    BEEDODGE,
    BEEARMORPIERCE,
    BEERESISTANCEPIERCE,
    BEEDODGEPIERCE,
    BEESTEALTHDETECTION,
    TOWERHEAL,
    TOWERSHIELD,
    TOWERSHIELDGENERATION,
    TOWERTARGETINGRANGE,
    TOWERDAMAGE,
    TOWERATTACKRATE,
    TOWERACTIONPOWER,
    TOWERACTIONRATE,
    TOWERBUFFPOWER,
    TOWEREFFECTPOWER,
    TOWEREFFECTRATE,
    TOWERCARRYCAPACITY,
    TOWERMOVESPEED,
    TOWERARMOR,
    TOWERREISTANCE,
    TOWERDODGE,
    TOWERARMORPIERCE,
    TOWERRESISTANCEPIERCE,
    TOWERDODGEPIERCE,
    TOWERSTEALTHDETECTION,
    TOWERAOEAREA,
    TOWERAOEDAMAGEDROPOFF,
    TOWERRAMPINGDAMAGE,
    TOWEREXTRARAMPCOUNT,
    TOWEREXTRARICOCHETCOUNT,
    ENEMYHEAL,
    ENEMYSHIELD,
    ENEMYSHIELDGENERATION,
    ENEMYTARGETINGRANGE,
    ENEMYDAMAGE,
    ENEMYATTACKRATE,
    ENEMYACTIONPOWER,
    ENEMYACTIONRATE,
    ENEMYEFFECTPOWER,
    ENEMYEFFECTRATE,
    ENEMYCARRYCAPACITY,
    ENEMYMOVESPEED,
    ENEMYARMOR,
    ENEMYREISTANCE,
    ENEMYDODGE,
    ENEMYARMORPIERCE,
    ENEMYRESISTANCEPIERCE,
    ENEMYDODGEPIERCE,
    ENEMYSTEALTHDETECTION,
    ENEMYHATCHTIME,
    ENEMYSPAWNTIME,
    ENEMYSTEALTHTIME
}

public class HoneyBuffManager : MonoBehaviour
{

    public static HoneyBuffManager main;

    public float[] honeyBuffs = new float[] {0f, 0f, 0f, 0f, 0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0f, 0f, 0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0f, 0f, 0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0f, 0f, 0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f};
    private float[] honeyBuffsBase;

    public float[] honeyThresholds = new float[] {};
    public int level = 0;

    void Awake() 
    {
        main = this;
        honeyBuffsBase = honeyBuffs;
        Array.Resize(ref honeyThresholds, LevelManager.main.honeyBuffThresholds.Length);
        for (int i = 0; i < LevelManager.main.honeyBuffThresholds.Length; i++) 
        {
            honeyThresholds[i] = LevelManager.main.honeyBuffThresholds[i] * LevelManager.main.honeyRequired;
        }
    }

    public void HoneyUpdate() 
    {
        if (level == 0) 
        {
            if (LevelManager.main.honey >= honeyThresholds[level]) 
            {
                level++;
            }
        }
        else if (level == honeyThresholds.Length) 
        {
            if (LevelManager.main.honey <= honeyThresholds[level - 1]) 
            {
                level--;
            }
        }
        else 
        {
            if (LevelManager.main.honey >= honeyThresholds[level]) 
            {
                level++;
            }
            else if (LevelManager.main.honey <= honeyThresholds[level - 1]) 
            {
                level--;
            }
        }
        SetBuffs();
    }

    private void SetBuffs() 
    {
        bool changed = false;
        for (int i = 0; i < LevelManager.main.honeyBuffThresholds.Length; i++) 
        {
            if (level > i)
            {
                if (honeyBuffs[(int)LevelManager.main.honeyBuffs[i]] == LevelManager.main.honeyBuffAmount[i])
                {
                    honeyBuffs[(int)LevelManager.main.honeyBuffs[i]] = LevelManager.main.honeyBuffAmount[i];
                    changed = true;
                }
            }
            else 
            {
                if (honeyBuffs[(int)LevelManager.main.honeyBuffs[i]] == honeyBuffsBase[i])
                {
                    honeyBuffs[(int)LevelManager.main.honeyBuffs[i]] = honeyBuffsBase[i];
                    changed = true;
                }
            }
        }
        if (changed == true) 
        {
            BuffManager.main.RefreshBuffs();
        }
        UIManager.main.RefreshHoneyPanel();
    }
}
