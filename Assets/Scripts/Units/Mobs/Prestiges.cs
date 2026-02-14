using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Prestiges : MonoBehaviour
{
    [SerializeField] private GameObject prestigeStar1;
    [SerializeField] private GameObject prestigeStar2;
    [SerializeField] private GameObject prestigeStar3;
    [SerializeField] private GameObject prestigeStar4;
    [SerializeField] private GameObject prestigeStar5;

    //trackers
    private Attributes attributes;

    private void Awake()
    {
        attributes = gameObject.GetComponent<Attributes>();
    }
    
    private void Start()
    {
        SetPrestigeStats();
    }

    public void SetPrestigeStats()
    {
        prestigeStar1.SetActive(false);
        prestigeStar2.SetActive(false);
        prestigeStar3.SetActive(false);
        prestigeStar4.SetActive(false);
        prestigeStar5.SetActive(false);
        if (attributes.prestiges!= null)
        {
            SetPrestigeRed(true, Regex.Matches(attributes. prestiges, "Red").Count);
            SetPrestigeBlue(true, Regex.Matches(attributes. prestiges, "Blue").Count);
            SetPrestigeGreen(true, Regex.Matches(attributes. prestiges, "Green").Count);
            SetPrestigeYellow(true, Regex.Matches(attributes. prestiges, "Yellow").Count);
            SetPrestigeWhite(true, Regex.Matches(attributes. prestiges, "White").Count);
            SetPrestigePurple(true, Regex.Matches(attributes. prestiges, "Purple").Count);
            SetPrestigeBlack(true, Regex.Matches(attributes. prestiges, "Black").Count);
            SetPrestigeGold(true, Regex.Matches(attributes. prestiges, "Gold").Count);
            SetPrestigeBrown(true, Regex.Matches(attributes. prestiges, "Brown").Count);
            SetPrestigeGrey(true, Regex.Matches(attributes. prestiges, "Grey").Count);
            SetPrestigePrismatic(true, Regex.Matches(attributes. prestiges, "Prismatic").Count);
            SetPrestigeSilver(true, Regex.Matches(attributes. prestiges, "Silver").Count);
            SetPrestigePlatinum(true, Regex.Matches(attributes. prestiges, "Platinum").Count);
            SetPrestigeTeal(true, Regex.Matches(attributes. prestiges, "Teal").Count);
        }
        attributes.SetHealthBar();
    }

    public void SetPrestigeRed(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float hpIncrease = 1f;
        float armorIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            hpIncrease += (GlobalValues.main.prestigeRed * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            armorIncrease = armorIncrease + (GlobalValues.main.prestigeRed * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true) 
        {
            attributes.hitPoints = attributes.hitPoints * hpIncrease;
            attributes.maxHP = attributes.maxHP * hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase * hpIncrease;
            attributes.armorBase += armorIncrease * 100f;
            attributes.armor += armorIncrease * 100f;
        }
        else
        {
            attributes.hitPoints = attributes.hitPoints / hpIncrease;
            attributes.maxHP = attributes.maxHP / hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase / hpIncrease;
            attributes.armorBase -= armorIncrease * 100f;
            attributes.armor -= armorIncrease * 100f;
        }
        TurnOnStar(Color.red, count);
    }

    public void SetPrestigeBlue(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float resistanceIncrease = 0f;
        float moveSpeedIncrease = 1f;
        for (int i = 1; i <= count; i++)
        {
            resistanceIncrease += (GlobalValues.main.prestigeBlue * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            moveSpeedIncrease += (GlobalValues.main.prestigeBlue * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.resistanceBase += resistanceIncrease * 100f;
            attributes.resistance += resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase * moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed * moveSpeedIncrease;
        }
        else
        {
            attributes.resistanceBase -= resistanceIncrease * 100f;
            attributes.resistance -= resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase / moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed / moveSpeedIncrease;
        }
        TurnOnStar(Color.blue, count);
    }

    public void SetPrestigeGreen(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (gameObject.GetComponent<Effects>().CheckEffect("Health Regen"))
            {
                gameObject.GetComponent<Effects>().ModifyEffect("Health Regen", GlobalValues.main.prestigeGreen * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f + GlobalValues.main.prestigeGreenMaxRatio * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))));
            }
            else
            {
                gameObject.GetComponent<Effects>().AddEffect("Health Regen", null, SoundType.EMPTY, GlobalValues.main.prestigeGreen * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), GlobalValues.main.prestigeGreenRate, 1f, 1f, 1f, 0.1f);
            }
        }
        TurnOnStar(Color.green, count);
    }

    public void SetPrestigeYellow(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float carryCapacityIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            carryCapacityIncrease += GlobalValues.main.prestigeYellow * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        attributes.carryCapacity += (int)((float)attributes.carryCapacity * carryCapacityIncrease);
        TurnOnStar(Color.yellow, count);
    }

    public void SetPrestigeWhite(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        attributes.willFly = !attributes.willFly;
        gameObject.GetComponent<Enemy>().SetPathSettings();
        TurnOnStar(Color.white, count);
    }

    public void SetPrestigePurple(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (gameObject.GetComponent<Effects>().CheckEffect("Blink"))
            {
                gameObject.GetComponent<Effects>().ModifyEffect("Blink", GlobalValues.main.prestigePurple * ((1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<Effects>().AddEffect("Blink", null, SoundType.EMPTY, GlobalValues.main.prestigePurple * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), GlobalValues.main.prestigePurpleRate, GlobalValues.main.prestigePurpleRange, 1f, 1f, 1f);
            }
        }
        TurnOnStar(Color.purple, count);
    }

    public void SetPrestigeBlack(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (gameObject.GetComponent<Passives>().CheckPassive("Death Split"))
            {
                gameObject.GetComponent<Passives>().ModifyPassive("Death Split", GlobalValues.main.prestigeBlack * ((1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<Passives>().AddPassive("Death Split", null, SoundType.EMPTY, GlobalValues.main.prestigeBlack * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1f, 1f, 1f, 1f, 1f);
            }
        }
        TurnOnStar(Color.black, count);
    }

    public void SetPrestigeGold(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float hpIncrease = 1f;
        float armorIncrease = 0f;
        float resistanceIncrease = 0f;
        float moveSpeedIncrease = 1f;
        float dodgeIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            hpIncrease += (GlobalValues.main.prestigeRed * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            armorIncrease += (GlobalValues.main.prestigeRed * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            resistanceIncrease += (GlobalValues.main.prestigeBlue * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            moveSpeedIncrease += (GlobalValues.main.prestigeBlue * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            dodgeIncrease += (GlobalValues.main.prestigeBrown * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.hitPoints = attributes.hitPoints * hpIncrease;
            attributes.maxHP = attributes.maxHP * hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase * hpIncrease;
            attributes.armorBase += armorIncrease * 100f;
            attributes.armor += armorIncrease * 100f;
            attributes.resistanceBase += resistanceIncrease * 100f;
            attributes.resistance += resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase * moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed * moveSpeedIncrease;
            attributes.dodgeChanceBase += dodgeIncrease * 100f;
            attributes.dodgeChance += dodgeIncrease * 100f;
        }
        else
        {
            attributes.hitPoints = attributes.hitPoints / hpIncrease;
            attributes.maxHP = attributes.maxHP / hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase / hpIncrease;
            attributes.armorBase -= armorIncrease * 100f;
            attributes.armor -= armorIncrease * 100f;
            attributes.resistanceBase -= resistanceIncrease * 100f;
            attributes.resistance -= resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase / moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed / moveSpeedIncrease;
            attributes.dodgeChanceBase -= dodgeIncrease * 100f;
            attributes.dodgeChance -= dodgeIncrease * 100f;
        }
        TurnOnStar(Color.gold, count);
    }

    public void SetPrestigeBrown(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float dodgeIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            dodgeIncrease += (GlobalValues.main.prestigeBrown * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.dodgeChanceBase += dodgeIncrease * 100f;
            attributes.dodgeChance += dodgeIncrease * 100f;
        }
        else
        {
            attributes.dodgeChanceBase -= dodgeIncrease * 100f;
            attributes.dodgeChance -= dodgeIncrease * 100f;
        }
        TurnOnStar(Color.brown, count);
    }

    public void SetPrestigeGrey(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (gameObject.GetComponent<Effects>().CheckEffect("Stealth"))
            {
                gameObject.GetComponent<Effects>().ModifyEffect("Stealth", GlobalValues.main.prestigeGrey * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<Effects>().AddEffect("Stealth", null, SoundType.EMPTY, GlobalValues.main.prestigeGrey * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), GlobalValues.main.prestigeGreyRate, 1f, 1f, GlobalValues.main.prestigeGreyDuration, 1f);
            }
        }
        TurnOnStar(Color.gray, count);
    }

    public void SetPrestigePrismatic(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (gameObject.GetComponent<Effects>().CheckEffect("Prismatic Buff"))
            {
                gameObject.GetComponent<Effects>().ModifyEffect("Prismatic Buff", GlobalValues.main.prestigePrismatic * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<Effects>().AddEffect("Prismatic Buff", null, SoundType.EMPTY, GlobalValues.main.prestigePrismatic * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1 / GlobalValues.main.prestigePrismaticDuration, 1f, 1f, GlobalValues.main.prestigePrismaticDuration, 1f);
            }
        }
        TurnOnStar(Color.paleVioletRed, count);
    }

    public void SetPrestigeSilver(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float shieldIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            shieldIncrease += GlobalValues.main.prestigeSilver * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.shield += attributes.maxHP * shieldIncrease;
            attributes.maxShield += attributes.maxHP * shieldIncrease;
            attributes.maxShieldBase += attributes.maxHP * shieldIncrease;
        }
        else
        {
            attributes.shield -= attributes.maxHP * shieldIncrease;
            attributes.maxShield -= attributes.maxHP * shieldIncrease;
            attributes.maxShieldBase -= attributes.maxHP * shieldIncrease;
        }
        TurnOnStar(Color.silver, count);
    }

    public void SetPrestigePlatinum(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (gameObject.GetComponent<Passives>().CheckPassive("Revive"))
            {
                gameObject.GetComponent<Passives>().ModifyPassive("Revive", GlobalValues.main.prestigePlatinum * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<Passives>().AddPassive("Revive", null, SoundType.EMPTY, GlobalValues.main.prestigePlatinum * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1 / GlobalValues.main.prestigePlatinumCooldown, 1f, 1f, 1f, 1f);
            }
        }
        TurnOnStar(new Color32(229, 228, 226, 255), count);
    }

    public void SetPrestigeTeal(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (gameObject.GetComponent<Passives>().CheckPassive("Slow & Freeze Immunity"))
            {
                gameObject.GetComponent<Passives>().ModifyPassive("Slow & Freeze Immunity", GlobalValues.main.prestigeTeal * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<Passives>().AddPassive("Slow & Freeze Immunity", null, SoundType.EMPTY, GlobalValues.main.prestigeTeal * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1f, 1f, 1f, 1f, 1f);
            }
        }
        TurnOnStar(Color.teal, count);
    }

    private void TurnOnStar(Color color, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            if (prestigeStar1.activeSelf == false)
            {
                prestigeStar1.GetComponent<SpriteRenderer>().color = color;
                prestigeStar1.SetActive(true);
            }
            else if (prestigeStar2.activeSelf == false)
            {
                prestigeStar2.GetComponent<SpriteRenderer>().color = color;
                prestigeStar2.SetActive(true);
            }
            else if (prestigeStar3.activeSelf == false)
            {
                prestigeStar3.GetComponent<SpriteRenderer>().color = color;
                prestigeStar3.SetActive(true);
            }
            else if (prestigeStar4.activeSelf == false)
            {
                prestigeStar4.GetComponent<SpriteRenderer>().color = color;
                prestigeStar4.SetActive(true);
            }
            else if (prestigeStar5.activeSelf == false)
            {
                prestigeStar5.GetComponent<SpriteRenderer>().color = color;
                prestigeStar5.SetActive(true);
            }
            else
            {
                Debug.Log(attributes.sName + " has too many prestiges.");
            }
        }
    }
}
