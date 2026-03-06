using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public enum VariantType
{
    EMPTY,
    ORANGE,
    BROWN,
    GREEN,
    BLUE,
    YELLOW,
    RED,
    BLACK,
    WHITE,
    PURPLE,
    PINK,
    GOLD
}

public class Variants : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite spriteBlack;
    [SerializeField] private Sprite spriteWhite;
    [SerializeField] private Sprite spriteBrown;
    [SerializeField] private Sprite spriteOrange;
    [SerializeField] private Sprite spriteYellow;
    [SerializeField] private Sprite spriteRed;
    [SerializeField] private Sprite spriteGreen;
    [SerializeField] private Sprite spriteBlue;
    [SerializeField] private Sprite spritePurple;
    [SerializeField] private Sprite spritePink;

    [Header("Orange Modifications")]
    [SerializeField] private string roleNameOrange = "Carrier";
    [SerializeField] private float flatHPOrange = 0f;
    [SerializeField] private float percentageHPOrange = 0.3f;
    [SerializeField] private float flatArmorOrange = -15f;
    [SerializeField] private float percentageArmorOrange = 0f;
    [SerializeField] private float flatMoveSpeedOrange = 0f;
    [SerializeField] private float percentageMoveSpeedOrange = 0.05f;
    [SerializeField] private float flatResistanceOrange = -10f;
    [SerializeField] private float percentageResistanceOrange = 0f;
    [SerializeField] private float flatDodgeOrange = -10f;
    [SerializeField] private float percentageDodgeOrange = 0f;
    [SerializeField] private float flatCarryCapacityOrange = 0f;
    [SerializeField] private float percentageCarryCapacityOrange = 0.5f;
    [SerializeField] private float flatShieldOrange = 0f;
    [SerializeField] private float percentageShieldOrange = 0f;
    [SerializeField] private bool stealHoneyOrange = true;
    [SerializeField] private float percentageActionPowerOrange = 0f;
    [SerializeField] private float percentageEffectPowerOrange = 0f;
    [SerializeField] private float percentagePassivePowerOrange = 0f;
    [SerializeField] private string extraActionOrange;
    [SerializeField] private GameObject actionPrefabOrange;
    [SerializeField] private SoundType actionSoundOrange;
    [SerializeField] private float actionPowerModifierOrange = 0f;
    [SerializeField] private float actionRateModifierOrange = 0f;
    [SerializeField] private float actionRangeModifierOrange = 0f;
    [SerializeField] private float actionPierceModifierOrange = 0f;
    [SerializeField] private float actionDurationOrange = 0f;
    [SerializeField] private float actionExtraModifierOrange = 0f;
    [SerializeField] private string extraEffectOrange;
    [SerializeField] private GameObject effectPrefabOrange;
    [SerializeField] private SoundType effectSoundOrange;
    [SerializeField] private float effectPowerModifierOrange = 0f;
    [SerializeField] private float effectRateModifierOrange = 0f;
    [SerializeField] private float effectRangeModifierOrange = 0f;
    [SerializeField] private float effectPierceModifierOrange = 0f;
    [SerializeField] private float effectDurationOrange = 0f;
    [SerializeField] private float effectExtraModifierOrange = 0f;
    [SerializeField] private string extraPassiveOrange;
    [SerializeField] private GameObject passivePrefabOrange;
    [SerializeField] private SoundType passiveSoundOrange;
    [SerializeField] private float passivePowerModifierOrange = 0f;
    [SerializeField] private float passiveRateModifierOrange = 0f;
    [SerializeField] private float passiveRangeModifierOrange = 0f;
    [SerializeField] private float passivePierceModifierOrange = 0f;
    [SerializeField] private float passiveDurationOrange = 0f;
    [SerializeField] private float passiveExtraModifierOrange = 0f;

    [Header("Brown Modifications")]
    [SerializeField] private string roleNameBrown = "Hauler";
    [SerializeField] private float flatHPBrown = 0f;
    [SerializeField] private float percentageHPBrown = 0.6f;
    [SerializeField] private float flatArmorBrown = 20f;
    [SerializeField] private float percentageArmorBrown = 0f;
    [SerializeField] private float flatMoveSpeedBrown = 0f;
    [SerializeField] private float percentageMoveSpeedBrown = -0.25f;
    [SerializeField] private float flatResistanceBrown = 10f;
    [SerializeField] private float percentageResistanceBrown = 0f;
    [SerializeField] private float flatDodgeBrown = -20f;
    [SerializeField] private float percentageDodgeBrown = 0f;
    [SerializeField] private float flatCarryCapacityBrown = 0f;
    [SerializeField] private float percentageCarryCapacityBrown = 1f;
    [SerializeField] private float flatShieldBrown = 0f;
    [SerializeField] private float percentageShieldBrown = 0f;
    [SerializeField] private bool stealHoneyBrown = true;
    [SerializeField] private float percentageActionPowerBrown = 0f;
    [SerializeField] private float percentageEffectPowerBrown = 0f;
    [SerializeField] private float percentagePassivePowerBrown = 0f;
    [SerializeField] private string extraActionBrown;
    [SerializeField] private GameObject actionPrefabBrown;
    [SerializeField] private SoundType actionSoundBrown;
    [SerializeField] private float actionPowerModifierBrown = 0f;
    [SerializeField] private float actionRateModifierBrown = 0f;
    [SerializeField] private float actionRangeModifierBrown = 0f;
    [SerializeField] private float actionPierceModifierBrown = 0f;
    [SerializeField] private float actionDurationBrown = 0f;
    [SerializeField] private float actionExtraModifierBrown = 0f;
    [SerializeField] private string extraEffectBrown;
    [SerializeField] private GameObject effectPrefabBrown;
    [SerializeField] private SoundType effectSoundBrown;
    [SerializeField] private float effectPowerModifierBrown = 0f;
    [SerializeField] private float effectRateModifierBrown = 0f;
    [SerializeField] private float effectRangeModifierBrown = 0f;
    [SerializeField] private float effectPierceModifierBrown = 0f;
    [SerializeField] private float effectDurationBrown = 0f;
    [SerializeField] private float effectExtraModifierBrown = 0f;
    [SerializeField] private string extraPassiveBrown;
    [SerializeField] private GameObject passivePrefabBrown;
    [SerializeField] private SoundType passiveSoundBrown;
    [SerializeField] private float passivePowerModifierBrown = 0f;
    [SerializeField] private float passiveRateModifierBrown = 0f;
    [SerializeField] private float passiveRangeModifierBrown = 0f;
    [SerializeField] private float passivePierceModifierBrown = 0f;
    [SerializeField] private float passiveDurationBrown = 0f;
    [SerializeField] private float passiveExtraModifierBrown = 0f;
    
    [Header("Green Modifications")]
    [SerializeField] private string roleNameGreen = "Regenerator";
    [SerializeField] private float flatHPGreen = 0f;
    [SerializeField] private float percentageHPGreen = 0.35f;
    [SerializeField] private float flatArmorGreen = 0f;
    [SerializeField] private float percentageArmorGreen = 0f;
    [SerializeField] private float flatMoveSpeedGreen = 0f;
    [SerializeField] private float percentageMoveSpeedGreen = -0.15f;
    [SerializeField] private float flatResistanceGreen = 25f;
    [SerializeField] private float percentageResistanceGreen = 0f;
    [SerializeField] private float flatDodgeGreen = -10f;
    [SerializeField] private float percentageDodgeGreen = 0f;
    [SerializeField] private float flatCarryCapacityGreen = 0f;
    [SerializeField] private float percentageCarryCapacityGreen = 0.2f;
    [SerializeField] private float flatShieldGreen = 0f;
    [SerializeField] private float percentageShieldGreen = 0f;
    [SerializeField] private bool stealHoneyGreen = true;
    [SerializeField] private float percentageActionPowerGreen = 0f;
    [SerializeField] private float percentageEffectPowerGreen = 0f;
    [SerializeField] private float percentagePassivePowerGreen = 0f;
    [SerializeField] private string extraActionGreen;
    [SerializeField] private GameObject actionPrefabGreen;
    [SerializeField] private SoundType actionSoundGreen;
    [SerializeField] private float actionPowerModifierGreen = 0f;
    [SerializeField] private float actionRateModifierGreen = 0f;
    [SerializeField] private float actionRangeModifierGreen = 0f;
    [SerializeField] private float actionPierceModifierGreen = 0f;
    [SerializeField] private float actionDurationGreen = 0f;
    [SerializeField] private float actionExtraModifierGreen = 0f;
    [SerializeField] private string extraEffectGreen = "Health Regen";
    [SerializeField] private GameObject effectPrefabGreen;
    [SerializeField] private SoundType effectSoundGreen;
    [SerializeField] private float effectPowerModifierGreen = 0f;
    [SerializeField] private float effectRateModifierGreen = 1f;
    [SerializeField] private float effectRangeModifierGreen = 1f;
    [SerializeField] private float effectPierceModifierGreen = 1f;
    [SerializeField] private float effectDurationGreen = 1f;
    [SerializeField] private float effectExtraModifierGreen = 0.02f;
    [SerializeField] private string extraPassiveGreen;
    [SerializeField] private GameObject passivePrefabGreen;
    [SerializeField] private SoundType passiveSoundGreen;
    [SerializeField] private float passivePowerModifierGreen = 0f;
    [SerializeField] private float passiveRateModifierGreen = 0f;
    [SerializeField] private float passiveRangeModifierGreen = 0f;
    [SerializeField] private float passivePierceModifierGreen = 0f;
    [SerializeField] private float passiveDurationGreen = 0f;
    [SerializeField] private float passiveExtraModifierGreen = 0f;
    
    [Header("Blue Modifications")]
    [SerializeField] private string roleNameBlue = "Sentinel";
    [SerializeField] private float flatHPBlue = 0f;
    [SerializeField] private float percentageHPBlue = 0.25f;
    [SerializeField] private float flatArmorBlue = 40f;
    [SerializeField] private float percentageArmorBlue = 0f;
    [SerializeField] private float flatMoveSpeedBlue = 0f;
    [SerializeField] private float percentageMoveSpeedBlue = -0.2f;
    [SerializeField] private float flatResistanceBlue = 30f;
    [SerializeField] private float percentageResistanceBlue = 0f;
    [SerializeField] private float flatDodgeBlue = -20f;
    [SerializeField] private float percentageDodgeBlue = 0f;
    [SerializeField] private float flatCarryCapacityBlue = 0f;
    [SerializeField] private float percentageCarryCapacityBlue = 0f;
    [SerializeField] private float flatShieldBlue = 0f;
    [SerializeField] private float percentageShieldBlue = 0f;
    [SerializeField] private bool stealHoneyBlue = false;
    [SerializeField] private float percentageActionPowerBlue = 0f;
    [SerializeField] private float percentageEffectPowerBlue = 0f;
    [SerializeField] private float percentagePassivePowerBlue = 0f;
    [SerializeField] private string extraActionBlue;
    [SerializeField] private GameObject actionPrefabBlue;
    [SerializeField] private SoundType actionSoundBlue;
    [SerializeField] private float actionPowerModifierBlue = 0f;
    [SerializeField] private float actionRateModifierBlue = 0f;
    [SerializeField] private float actionRangeModifierBlue = 0f;
    [SerializeField] private float actionPierceModifierBlue = 0f;
    [SerializeField] private float actionDurationBlue = 0f;
    [SerializeField] private float actionExtraModifierBlue = 0f;
    [SerializeField] private string extraEffectBlue;
    [SerializeField] private GameObject effectPrefabBlue;
    [SerializeField] private SoundType effectSoundBlue;
    [SerializeField] private float effectPowerModifierBlue = 0f;
    [SerializeField] private float effectRateModifierBlue = 0f;
    [SerializeField] private float effectRangeModifierBlue = 0f;
    [SerializeField] private float effectPierceModifierBlue = 0f;
    [SerializeField] private float effectDurationBlue = 0f;
    [SerializeField] private float effectExtraModifierBlue = 0f;
    [SerializeField] private string extraPassiveBlue;
    [SerializeField] private GameObject passivePrefabBlue;
    [SerializeField] private SoundType passiveSoundBlue;
    [SerializeField] private float passivePowerModifierBlue = 0f;
    [SerializeField] private float passiveRateModifierBlue = 0f;
    [SerializeField] private float passiveRangeModifierBlue = 0f;
    [SerializeField] private float passivePierceModifierBlue = 0f;
    [SerializeField] private float passiveDurationBlue = 0f;
    [SerializeField] private float passiveExtraModifierBlue = 0f;
    
    [Header("Yellow Modifications")]
    [SerializeField] private string roleNameYellow = "Scout";
    [SerializeField] private float flatHPYellow = 0f;
    [SerializeField] private float percentageHPYellow = -0.15f;
    [SerializeField] private float flatArmorYellow = -20f;
    [SerializeField] private float percentageArmorYellow = 0f;
    [SerializeField] private float flatMoveSpeedYellow = 0f;
    [SerializeField] private float percentageMoveSpeedYellow = 0.5f;
    [SerializeField] private float flatResistanceYellow = -10f;
    [SerializeField] private float percentageResistanceYellow = 0f;
    [SerializeField] private float flatDodgeYellow = 30f;
    [SerializeField] private float percentageDodgeYellow = 0f;
    [SerializeField] private float flatCarryCapacityYellow = 0f;
    [SerializeField] private float percentageCarryCapacityYellow = -0.25f;
    [SerializeField] private float flatShieldYellow = 0f;
    [SerializeField] private float percentageShieldYellow = 0f;
    [SerializeField] private bool stealHoneyYellow = true;
    [SerializeField] private float percentageActionPowerYellow = 0f;
    [SerializeField] private float percentageEffectPowerYellow = 0f;
    [SerializeField] private float percentagePassivePowerYellow = 0f;
    [SerializeField] private string extraActionYellow;
    [SerializeField] private GameObject actionPrefabYellow;
    [SerializeField] private SoundType actionSoundYellow;
    [SerializeField] private float actionPowerModifierYellow = 0f;
    [SerializeField] private float actionRateModifierYellow = 0f;
    [SerializeField] private float actionRangeModifierYellow = 0f;
    [SerializeField] private float actionPierceModifierYellow = 0f;
    [SerializeField] private float actionDurationYellow = 0f;
    [SerializeField] private float actionExtraModifierYellow = 0f;
    [SerializeField] private string extraEffectYellow;
    [SerializeField] private GameObject effectPrefabYellow;
    [SerializeField] private SoundType effectSoundYellow;
    [SerializeField] private float effectPowerModifierYellow = 0f;
    [SerializeField] private float effectRateModifierYellow = 0f;
    [SerializeField] private float effectRangeModifierYellow = 0f;
    [SerializeField] private float effectPierceModifierYellow = 0f;
    [SerializeField] private float effectDurationYellow = 0f;
    [SerializeField] private float effectExtraModifierYellow = 0f;
    [SerializeField] private string extraPassiveYellow;
    [SerializeField] private GameObject passivePrefabYellow;
    [SerializeField] private SoundType passiveSoundYellow;
    [SerializeField] private float passivePowerModifierYellow = 0f;
    [SerializeField] private float passiveRateModifierYellow = 0f;
    [SerializeField] private float passiveRangeModifierYellow = 0f;
    [SerializeField] private float passivePierceModifierYellow = 0f;
    [SerializeField] private float passiveDurationYellow = 0f;
    [SerializeField] private float passiveExtraModifierYellow = 0f;
    
    [Header("Red Modifications")]
    [SerializeField] private string roleNameRed = "Brawler";
    [SerializeField] private float flatHPRed = 0f;
    [SerializeField] private float percentageHPRed = 0.2f;
    [SerializeField] private float flatArmorRed = 0f;
    [SerializeField] private float percentageArmorRed = 0f;
    [SerializeField] private float flatMoveSpeedRed = 0f;
    [SerializeField] private float percentageMoveSpeedRed = 0.3f;
    [SerializeField] private float flatResistanceRed = -15f;
    [SerializeField] private float percentageResistanceRed = 0f;
    [SerializeField] private float flatDodgeRed = 5f;
    [SerializeField] private float percentageDodgeRed = 0f;
    [SerializeField] private float flatCarryCapacityRed = 0f;
    [SerializeField] private float percentageCarryCapacityRed = 0f;
    [SerializeField] private float flatShieldRed = 0f;
    [SerializeField] private float percentageShieldRed = 0f;
    [SerializeField] private bool stealHoneyRed = false;
    [SerializeField] private float percentageActionPowerRed = 0f;
    [SerializeField] private float percentageEffectPowerRed = 0f;
    [SerializeField] private float percentagePassivePowerRed = 0f;
    [SerializeField] private string extraActionRed;
    [SerializeField] private GameObject actionPrefabRed;
    [SerializeField] private SoundType actionSoundRed;
    [SerializeField] private float actionPowerModifierRed = 0f;
    [SerializeField] private float actionRateModifierRed = 0f;
    [SerializeField] private float actionRangeModifierRed = 0f;
    [SerializeField] private float actionPierceModifierRed = 0f;
    [SerializeField] private float actionDurationRed = 0f;
    [SerializeField] private float actionExtraModifierRed = 0f;
    [SerializeField] private string extraEffectRed;
    [SerializeField] private GameObject effectPrefabRed;
    [SerializeField] private SoundType effectSoundRed;
    [SerializeField] private float effectPowerModifierRed = 0f;
    [SerializeField] private float effectRateModifierRed = 0f;
    [SerializeField] private float effectRangeModifierRed = 0f;
    [SerializeField] private float effectPierceModifierRed = 0f;
    [SerializeField] private float effectDurationRed = 0f;
    [SerializeField] private float effectExtraModifierRed = 0f;
    [SerializeField] private string extraPassiveRed;
    [SerializeField] private GameObject passivePrefabRed;
    [SerializeField] private SoundType passiveSoundRed;
    [SerializeField] private float passivePowerModifierRed = 0f;
    [SerializeField] private float passiveRateModifierRed = 0f;
    [SerializeField] private float passiveRangeModifierRed = 0f;
    [SerializeField] private float passivePierceModifierRed = 0f;
    [SerializeField] private float passiveDurationRed = 0f;
    [SerializeField] private float passiveExtraModifierRed = 0f;
    
    [Header("Black Modifications")]
    [SerializeField] private string roleNameBlack = "Predator";
    [SerializeField] private float flatHPBlack = 0f;
    [SerializeField] private float percentageHPBlack = -0.2f;
    [SerializeField] private float flatArmorBlack = -10f;
    [SerializeField] private float percentageArmorBlack = 0f;
    [SerializeField] private float flatMoveSpeedBlack = 0f;
    [SerializeField] private float percentageMoveSpeedBlack = 0.5f;
    [SerializeField] private float flatResistanceBlack = 0f;
    [SerializeField] private float percentageResistanceBlack = 0f;
    [SerializeField] private float flatDodgeBlack = 20f;
    [SerializeField] private float percentageDodgeBlack = 0f;
    [SerializeField] private float flatCarryCapacityBlack = 0f;
    [SerializeField] private float percentageCarryCapacityBlack = 0f;
    [SerializeField] private float flatShieldBlack = 0f;
    [SerializeField] private float percentageShieldBlack = 0f;
    [SerializeField] private bool stealHoneyBlack = false;
    [SerializeField] private float percentageActionPowerBlack = 0f;
    [SerializeField] private float percentageEffectPowerBlack = 0f;
    [SerializeField] private float percentagePassivePowerBlack = 0f;
    [SerializeField] private string extraActionBlack;
    [SerializeField] private GameObject actionPrefabBlack;
    [SerializeField] private SoundType actionSoundBlack;
    [SerializeField] private float actionPowerModifierBlack = 0f;
    [SerializeField] private float actionRateModifierBlack = 0f;
    [SerializeField] private float actionRangeModifierBlack = 0f;
    [SerializeField] private float actionPierceModifierBlack = 0f;
    [SerializeField] private float actionDurationBlack = 0f;
    [SerializeField] private float actionExtraModifierBlack = 0f;
    [SerializeField] private string extraEffectBlack;
    [SerializeField] private GameObject effectPrefabBlack;
    [SerializeField] private SoundType effectSoundBlack;
    [SerializeField] private float effectPowerModifierBlack = 0f;
    [SerializeField] private float effectRateModifierBlack = 0f;
    [SerializeField] private float effectRangeModifierBlack = 0f;
    [SerializeField] private float effectPierceModifierBlack = 0f;
    [SerializeField] private float effectDurationBlack = 0f;
    [SerializeField] private float effectExtraModifierBlack = 0f;
    [SerializeField] private string extraPassiveBlack;
    [SerializeField] private GameObject passivePrefabBlack;
    [SerializeField] private SoundType passiveSoundBlack;
    [SerializeField] private float passivePowerModifierBlack = 0f;
    [SerializeField] private float passiveRateModifierBlack = 0f;
    [SerializeField] private float passiveRangeModifierBlack = 0f;
    [SerializeField] private float passivePierceModifierBlack = 0f;
    [SerializeField] private float passiveDurationBlack = 0f;
    [SerializeField] private float passiveExtraModifierBlack = 0f;
    
    [Header("White Modifications")]
    [SerializeField] private string roleNameWhite = "Shielder";
    [SerializeField] private float flatHPWhite = 0f;
    [SerializeField] private float percentageHPWhite = 0.2f;
    [SerializeField] private float flatArmorWhite = 20f;
    [SerializeField] private float percentageArmorWhite = 0f;
    [SerializeField] private float flatMoveSpeedWhite = 0f;
    [SerializeField] private float percentageMoveSpeedWhite = -0.1f;
    [SerializeField] private float flatResistanceWhite = 20f;
    [SerializeField] private float percentageResistanceWhite = 0f;
    [SerializeField] private float flatDodgeWhite = 0f;
    [SerializeField] private float percentageDodgeWhite = 0f;
    [SerializeField] private float flatCarryCapacityWhite = 0f;
    [SerializeField] private float percentageCarryCapacityWhite = 0f;
    [SerializeField] private float flatShieldWhite = 0f;
    [SerializeField] private float percentageShieldWhite = 0.25f;
    [SerializeField] private bool stealHoneyWhite = true;
    [SerializeField] private float percentageActionPowerWhite = 0f;
    [SerializeField] private float percentageEffectPowerWhite = 0f;
    [SerializeField] private float percentagePassivePowerWhite = 0f;
    [SerializeField] private string extraActionWhite;
    [SerializeField] private GameObject actionPrefabWhite;
    [SerializeField] private SoundType actionSoundWhite;
    [SerializeField] private float actionPowerModifierWhite = 0f;
    [SerializeField] private float actionRateModifierWhite = 0f;
    [SerializeField] private float actionRangeModifierWhite = 0f;
    [SerializeField] private float actionPierceModifierWhite = 0f;
    [SerializeField] private float actionDurationWhite = 0f;
    [SerializeField] private float actionExtraModifierWhite = 0f;
    [SerializeField] private string extraEffectWhite = "Shield Regen";
    [SerializeField] private GameObject effectPrefabWhite;
    [SerializeField] private SoundType effectSoundWhite;
    [SerializeField] private float effectPowerModifierWhite = 0f;
    [SerializeField] private float effectRateModifierWhite = 1f;
    [SerializeField] private float effectRangeModifierWhite = 1f;
    [SerializeField] private float effectPierceModifierWhite = 1f;
    [SerializeField] private float effectDurationWhite = 0f;
    [SerializeField] private float effectExtraModifierWhite = 0.025f;
    [SerializeField] private string extraPassiveWhite;
    [SerializeField] private GameObject passivePrefabWhite;
    [SerializeField] private SoundType passiveSoundWhite;
    [SerializeField] private float passivePowerModifierWhite = 0f;
    [SerializeField] private float passiveRateModifierWhite = 0f;
    [SerializeField] private float passiveRangeModifierWhite = 0f;
    [SerializeField] private float passivePierceModifierWhite = 0f;
    [SerializeField] private float passiveDurationWhite = 0f;
    [SerializeField] private float passiveExtraModifierWhite = 0f;
    
    [Header("Purple Modifications")]
    [SerializeField] private string roleNamePurple = "Trickster";
    [SerializeField] private float flatHPPurple = 0f;
    [SerializeField] private float percentageHPPurple = -0.2f;
    [SerializeField] private float flatArmorPurple = 0f;
    [SerializeField] private float percentageArmorPurple = 0f;
    [SerializeField] private float flatMoveSpeedPurple = 0f;
    [SerializeField] private float percentageMoveSpeedPurple = 0.1f;
    [SerializeField] private float flatResistancePurple = 10f;
    [SerializeField] private float percentageResistancePurple = 0f;
    [SerializeField] private float flatDodgePurple = 25f;
    [SerializeField] private float percentageDodgePurple = 0f;
    [SerializeField] private float flatCarryCapacityPurple = 0f;
    [SerializeField] private float percentageCarryCapacityPurple = 0f;
    [SerializeField] private float flatShieldPurple = 0f;
    [SerializeField] private float percentageShieldPurple = 0f;
    [SerializeField] private bool stealHoneyPurple = true;
    [SerializeField] private float percentageActionPowerPurple = 0f;
    [SerializeField] private float percentageEffectPowerPurple = 0f;
    [SerializeField] private float percentagePassivePowerPurple = 0f;
    [SerializeField] private string extraActionPurple;
    [SerializeField] private GameObject actionPrefabPurple;
    [SerializeField] private SoundType actionSoundPurple;
    [SerializeField] private float actionPowerModifierPurple = 0f;
    [SerializeField] private float actionRateModifierPurple = 0f;
    [SerializeField] private float actionRangeModifierPurple = 0f;
    [SerializeField] private float actionPierceModifierPurple = 0f;
    [SerializeField] private float actionDurationPurple = 0f;
    [SerializeField] private float actionExtraModifierPurple = 0f;
    [SerializeField] private string extraEffectPurple = "Stealth";
    [SerializeField] private GameObject effectPrefabPurple;
    [SerializeField] private SoundType effectSoundPurple;
    [SerializeField] private float effectPowerModifierPurple = 0.25f;
    [SerializeField] private float effectRateModifierPurple = 0.125f;
    [SerializeField] private float effectRangeModifierPurple = 0f;
    [SerializeField] private float effectPierceModifierPurple = 0f;
    [SerializeField] private float effectDurationPurple = 5f;
    [SerializeField] private float effectExtraModifierPurple = 0f;
    [SerializeField] private string extraPassivePurple;
    [SerializeField] private GameObject passivePrefabPurple;
    [SerializeField] private SoundType passiveSoundPurple;
    [SerializeField] private float passivePowerModifierPurple = 0f;
    [SerializeField] private float passiveRateModifierPurple = 0f;
    [SerializeField] private float passiveRangeModifierPurple = 0f;
    [SerializeField] private float passivePierceModifierPurple = 0f;
    [SerializeField] private float passiveDurationPurple = 0f;
    [SerializeField] private float passiveExtraModifierPurple = 0f;
    
    [Header("Pink Modifications")]
    [SerializeField] private string roleNamePink = "Debilitator";
    [SerializeField] private float flatHPPink = 0f;
    [SerializeField] private float percentageHPPink = -0.2f;
    [SerializeField] private float flatArmorPink = -10f;
    [SerializeField] private float percentageArmorPink = 0f;
    [SerializeField] private float flatMoveSpeedPink = 0f;
    [SerializeField] private float percentageMoveSpeedPink = -0.05f;
    [SerializeField] private float flatResistancePink = 15f;
    [SerializeField] private float percentageResistancePink = 0f;
    [SerializeField] private float flatDodgePink = 0f;
    [SerializeField] private float percentageDodgePink = 0f;
    [SerializeField] private float flatCarryCapacityPink = 0f;
    [SerializeField] private float percentageCarryCapacityPink = 0.25f;
    [SerializeField] private float flatShieldPink = 0f;
    [SerializeField] private float percentageShieldPink = 0f;
    [SerializeField] private bool stealHoneyPink = true;
    [SerializeField] private float percentageActionPowerPink = 0f;
    [SerializeField] private float percentageEffectPowerPink = 0f;
    [SerializeField] private float percentagePassivePowerPink = 0f;
    [SerializeField] private string extraActionPink;
    [SerializeField] private GameObject actionPrefabPink;
    [SerializeField] private SoundType actionSoundPink;
    [SerializeField] private float actionPowerModifierPink = 0f;
    [SerializeField] private float actionRateModifierPink = 0f;
    [SerializeField] private float actionRangeModifierPink = 0f;
    [SerializeField] private float actionPierceModifierPink = 0f;
    [SerializeField] private float actionDurationPink = 0f;
    [SerializeField] private float actionExtraModifierPink = 0f;
    [SerializeField] private string extraEffectPink = "Debilitate";
    [SerializeField] private GameObject effectPrefabPink;
    [SerializeField] private SoundType effectSoundPink;
    [SerializeField] private float effectPowerModifierPink = 1f;
    [SerializeField] private float effectRateModifierPink = 1f;
    [SerializeField] private float effectRangeModifierPink = 5f;
    [SerializeField] private float effectPierceModifierPink = 0f;
    [SerializeField] private float effectDurationPink = 1f;
    [SerializeField] private float effectExtraModifierPink = 0f;
    [SerializeField] private string extraPassivePink;
    [SerializeField] private GameObject passivePrefabPink;
    [SerializeField] private SoundType passiveSoundPink;
    [SerializeField] private float passivePowerModifierPink = 0f;
    [SerializeField] private float passiveRateModifierPink = 0f;
    [SerializeField] private float passiveRangeModifierPink = 0f;
    [SerializeField] private float passivePierceModifierPink = 0f;
    [SerializeField] private float passiveDurationPink = 0f;
    [SerializeField] private float passiveExtraModifierPink = 0f;

    [Header("Gold Modifications")]
    [SerializeField] private string roleNameGold = "Royal";
    [SerializeField] private float flatHPGold = 0f;
    [SerializeField] private float percentageHPGold = 3f;
    [SerializeField] private float flatArmorGold = 30f;
    [SerializeField] private float percentageArmorGold = 0f;
    [SerializeField] private float flatMoveSpeedGold = 0f;
    [SerializeField] private float percentageMoveSpeedGold = 0f;
    [SerializeField] private float flatResistanceGold = 30f;
    [SerializeField] private float percentageResistanceGold = 0f;
    [SerializeField] private float flatDodgeGold = 30f;
    [SerializeField] private float percentageDodgeGold = 0f;
    [SerializeField] private float flatCarryCapacityGold = 0f;
    [SerializeField] private float percentageCarryCapacityGold = 5f;
    [SerializeField] private float flatShieldGold = 0f;
    [SerializeField] private float percentageShieldGold = 0f;
    [SerializeField] private bool stealHoneyGold = true;
    [SerializeField] private float percentageActionPowerGold = 0f;
    [SerializeField] private float percentageEffectPowerGold = 0f;
    [SerializeField] private float percentagePassivePowerGold = 0f;
    [SerializeField] private string extraActionGold;
    [SerializeField] private GameObject actionPrefabGold;
    [SerializeField] private SoundType actionSoundGold;
    [SerializeField] private float actionPowerModifierGold = 0f;
    [SerializeField] private float actionRateModifierGold = 0f;
    [SerializeField] private float actionRangeModifierGold = 0f;
    [SerializeField] private float actionPierceModifierGold = 0f;
    [SerializeField] private float actionDurationGold = 0f;
    [SerializeField] private float actionExtraModifierGold = 0f;
    [SerializeField] private string extraEffectGold;
    [SerializeField] private GameObject effectPrefabGold;
    [SerializeField] private SoundType effectSoundGold;
    [SerializeField] private float effectPowerModifierGold = 1f;
    [SerializeField] private float effectRateModifierGold = 1f;
    [SerializeField] private float effectRangeModifierGold = 5f;
    [SerializeField] private float effectPierceModifierGold = 0f;
    [SerializeField] private float effectDurationGold = 1f;
    [SerializeField] private float effectExtraModifierGold = 0f;
    [SerializeField] private string extraPassiveGold;
    [SerializeField] private GameObject passivePrefabGold;
    [SerializeField] private SoundType passiveSoundGold;
    [SerializeField] private float passivePowerModifierGold = 0f;
    [SerializeField] private float passiveRateModifierGold = 0f;
    [SerializeField] private float passiveRangeModifierGold = 0f;
    [SerializeField] private float passivePierceModifierGold = 0f;
    [SerializeField] private float passiveDurationGold = 0f;
    [SerializeField] private float passiveExtraModifierGold = 0f;

    //trackers
    private Attributes attributes;
    private VariantType variant;

    private void Start()
    {
        attributes = gameObject.GetComponent<Attributes>();
        SetVariantStats();
    }

    public void SetVariantStats()
    {
        variant = attributes.variant;
        if (variant == VariantType.ORANGE)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteOrange;
            //Update Stats
            attributes.variantName = roleNameOrange;
            attributes.maxHP += attributes.maxHP * percentageHPOrange + flatHPOrange;
            attributes.armor += attributes.armor * percentageArmorOrange + flatArmorOrange;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedOrange + flatMoveSpeedOrange;
            attributes.resistance += attributes.resistance * percentageResistanceOrange + flatResistanceOrange;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeOrange + flatDodgeOrange;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityOrange + flatCarryCapacityOrange);
            attributes.maxShield += attributes.maxHP * percentageShieldOrange + flatShieldOrange;
            attributes.actionPower += attributes.actionPower * percentageActionPowerOrange;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerOrange;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerOrange;
            //Update Actions
            if (stealHoneyOrange == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                attributes.moveSpeed += (attributes.moveSpeed * percentageMoveSpeedOrange) / 2;
            }
            if (extraActionOrange != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionOrange, actionPrefabOrange, actionSoundOrange, actionPowerModifierOrange, actionRateModifierOrange, actionRangeModifierOrange, actionPierceModifierOrange, actionDurationOrange, actionExtraModifierOrange);
            }
            //Update Effects
            if (extraEffectOrange != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectOrange, effectPrefabOrange, effectSoundOrange, effectPowerModifierOrange, effectRateModifierOrange, effectRangeModifierOrange, effectPierceModifierOrange, effectDurationOrange, effectExtraModifierOrange);
            }
            //Update Passives
            if (extraPassiveOrange != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveOrange, passivePrefabOrange, passiveSoundOrange, passivePowerModifierOrange, passiveRateModifierOrange, passiveRangeModifierOrange, passivePierceModifierOrange, passiveDurationOrange, passiveExtraModifierOrange);
            }
        }
        else if (variant == VariantType.BROWN)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteBrown;
            //Update Stats
            attributes.variantName = roleNameBrown;
            attributes.maxHP += attributes.maxHP * percentageHPBrown + flatHPBrown;
            attributes.armor += attributes.armor * percentageArmorBrown + flatArmorBrown;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedBrown + flatMoveSpeedBrown;
            attributes.resistance += attributes.resistance * percentageResistanceBrown + flatResistanceBrown;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeBrown + flatDodgeBrown;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityBrown + flatCarryCapacityBrown);
            attributes.maxShield += attributes.maxHP * percentageShieldBrown + flatShieldBrown;
            attributes.actionPower += attributes.actionPower * percentageActionPowerBrown;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerBrown;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerBrown;
            //Update Actions
            if (stealHoneyBrown == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                attributes.moveSpeed += (attributes.moveSpeed * percentageMoveSpeedRed) / 2;
            }
            if (extraActionBrown != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionBrown, actionPrefabBrown, actionSoundBrown, actionPowerModifierBrown, actionRateModifierBrown, actionRangeModifierBrown, actionPierceModifierBrown, actionDurationBrown, actionExtraModifierBrown);
            }
            //Update Effects
            if (extraEffectBrown != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectBrown, effectPrefabBrown, effectSoundBrown, effectPowerModifierBrown, effectRateModifierBrown, effectRangeModifierBrown, effectPierceModifierBrown, effectDurationBrown, effectExtraModifierBrown);
            }
            //Update Passives
            if (extraPassiveBrown != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveBrown, passivePrefabBrown, passiveSoundBrown, passivePowerModifierBrown, passiveRateModifierBrown, passiveRangeModifierBrown, passivePierceModifierBrown, passiveDurationBrown, passiveExtraModifierBrown);
            }
        }
        else if (variant == VariantType.GREEN)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteGreen;
            //Update Stats
            attributes.variantName = roleNameGreen;
            attributes.maxHP += attributes.maxHP * percentageHPGreen + flatHPGreen;
            attributes.armor += attributes.armor * percentageArmorGreen + flatArmorGreen;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedGreen + flatMoveSpeedGreen;
            attributes.resistance += attributes.resistance * percentageResistanceGreen + flatResistanceGreen;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeGreen + flatDodgeGreen;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityGreen + flatCarryCapacityGreen);
            attributes.maxShield += attributes.maxHP * percentageShieldGreen + flatShieldGreen;
            attributes.actionPower += attributes.actionPower * percentageActionPowerGreen;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerGreen;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerGreen;
            //Update Actions
            if (stealHoneyGreen == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionGreen != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionGreen, actionPrefabGreen, actionSoundGreen, actionPowerModifierGreen, actionRateModifierGreen, actionRangeModifierGreen, actionPierceModifierGreen, actionDurationGreen, actionExtraModifierGreen);
            }
            //Update Effects
            if (extraEffectGreen != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectGreen, effectPrefabGreen, effectSoundGreen, effectPowerModifierGreen, effectRateModifierGreen, effectRangeModifierGreen, effectPierceModifierGreen, effectDurationGreen, effectExtraModifierGreen);
            }
            //Update Passives
            if (extraPassiveGreen != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveGreen, passivePrefabGreen, passiveSoundGreen, passivePowerModifierGreen, passiveRateModifierGreen, passiveRangeModifierGreen, passivePierceModifierGreen, passiveDurationGreen, passiveExtraModifierGreen);
            }
        }
        else if (variant == VariantType.BLUE)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteBlue;
            //Update Stats
            attributes.variantName = roleNameBlue;
            attributes.maxHP += attributes.maxHP * percentageHPBlue + flatHPBlue;
            attributes.armor += attributes.armor * percentageArmorBlue + flatArmorBlue;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedBlue + flatMoveSpeedBlue;
            attributes.resistance += attributes.resistance * percentageResistanceBlue + flatResistanceBlue;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeBlue + flatDodgeBlue;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityBlue + flatCarryCapacityBlue);
            attributes.maxShield += attributes.maxHP * percentageShieldBlue + flatShieldBlue;
            attributes.actionPower += attributes.actionPower * percentageActionPowerBlue;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerBlue;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerBlue;
            //Update Actions
            if (stealHoneyBlue == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionBlue != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionBlue, actionPrefabBlue, actionSoundBlue, actionPowerModifierBlue, actionRateModifierBlue, actionRangeModifierBlue, actionPierceModifierBlue, actionDurationBlue, actionExtraModifierBlue);
            }
            //Update Effects
            if (extraEffectBlue != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectBlue, effectPrefabBlue, effectSoundBlue, effectPowerModifierBlue, effectRateModifierBlue, effectRangeModifierBlue, effectPierceModifierBlue, effectDurationBlue, effectExtraModifierBlue);
            }
            //Update Passives
            if (extraPassiveBlue != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveBlue, passivePrefabBlue, passiveSoundBlue, passivePowerModifierBlue, passiveRateModifierBlue, passiveRangeModifierBlue, passivePierceModifierBlue, passiveDurationBlue, passiveExtraModifierBlue);
            }
        }
        else if (variant == VariantType.YELLOW)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteYellow;
            //Update Stats
            attributes.variantName = roleNameYellow;
            attributes.maxHP += attributes.maxHP * percentageHPYellow + flatHPYellow;
            attributes.armor += attributes.armor * percentageArmorYellow + flatArmorYellow;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedYellow + flatMoveSpeedYellow;
            attributes.resistance += attributes.resistance * percentageResistanceYellow + flatResistanceYellow;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeYellow + flatDodgeYellow;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityYellow + flatCarryCapacityYellow);
            attributes.maxShield += attributes.maxHP * percentageShieldYellow + flatShieldYellow;
            attributes.actionPower += attributes.actionPower * percentageActionPowerYellow;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerYellow;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerYellow;
            //Update Actions
            if (stealHoneyYellow == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionYellow != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionYellow, actionPrefabYellow, actionSoundYellow, actionPowerModifierYellow, actionRateModifierYellow, actionRangeModifierYellow, actionPierceModifierYellow, actionDurationYellow, actionExtraModifierYellow);
            }
            //Update Effects
            if (extraEffectYellow != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectYellow, effectPrefabYellow, effectSoundYellow, effectPowerModifierYellow, effectRateModifierYellow, effectRangeModifierYellow, effectPierceModifierYellow, effectDurationYellow, effectExtraModifierYellow);
            }
            //Update Passives
            if (extraPassiveYellow != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveYellow, passivePrefabYellow, passiveSoundYellow, passivePowerModifierYellow, passiveRateModifierYellow, passiveRangeModifierYellow, passivePierceModifierYellow, passiveDurationYellow, passiveExtraModifierYellow);
            }
        }
        else if (variant == VariantType.RED)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteRed;
            //Update Stats
            attributes.variantName = roleNameRed;
            attributes.maxHP += attributes.maxHP * percentageHPRed + flatHPRed;
            attributes.armor += attributes.armor * percentageArmorRed + flatArmorRed;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedRed + flatMoveSpeedRed;
            attributes.resistance += attributes.resistance * percentageResistanceRed + flatResistanceRed;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeRed + flatDodgeRed;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityRed + flatCarryCapacityRed);
            attributes.maxShield += attributes.maxHP * percentageShieldRed + flatShieldRed;
            attributes.actionPower += attributes.actionPower * percentageActionPowerRed;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerRed;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerRed;
            //Update Actions
            if (stealHoneyRed == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionRed != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionRed, actionPrefabRed, actionSoundRed, actionPowerModifierRed, actionRateModifierRed, actionRangeModifierRed, actionPierceModifierRed, actionDurationRed, actionExtraModifierRed);
            }
            //Update Effects
            if (extraEffectRed != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectRed, effectPrefabRed, effectSoundRed, effectPowerModifierRed, effectRateModifierRed, effectRangeModifierRed, effectPierceModifierRed, effectDurationRed, effectExtraModifierRed);
            }
            //Update Passives
            if (extraPassiveRed != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveRed, passivePrefabRed, passiveSoundRed, passivePowerModifierRed, passiveRateModifierRed, passiveRangeModifierRed, passivePierceModifierRed, passiveDurationRed, passiveExtraModifierRed);
            }
        }
        else if (variant == VariantType.BLACK)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteBlack;
            //Update Stats
            attributes.variantName = roleNameBlack;
            attributes.maxHP += attributes.maxHP * percentageHPBlack + flatHPBlack;
            attributes.armor += attributes.armor * percentageArmorBlack + flatArmorBlack;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedBlack + flatMoveSpeedBlack;
            attributes.resistance += attributes.resistance * percentageResistanceBlack + flatResistanceBlack;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeBlack + flatDodgeBlack;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityBlack + flatCarryCapacityBlack);
            attributes.maxShield += attributes.maxHP * percentageShieldBlack + flatShieldBlack;
            attributes.actionPower += attributes.actionPower * percentageActionPowerBlack;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerBlack;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerBlack;
            //Update Actions
            if (stealHoneyBlack == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionBlack != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionBlack, actionPrefabBlack, actionSoundBlack, actionPowerModifierBlack, actionRateModifierBlack, actionRangeModifierBlack, actionPierceModifierBlack, actionDurationBlack, actionExtraModifierBlack);
            }
            //Update Effects
            if (extraEffectBlack != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectBlack, effectPrefabBlack, effectSoundBlack, effectPowerModifierBlack, effectRateModifierBlack, effectRangeModifierBlack, effectPierceModifierBlack, effectDurationBlack, effectExtraModifierBlack);
            }
            //Update Passives
            if (extraPassiveBlack != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveBlack, passivePrefabBlack, passiveSoundBlack, passivePowerModifierBlack, passiveRateModifierBlack, passiveRangeModifierBlack, passivePierceModifierBlack, passiveDurationBlack, passiveExtraModifierBlack);
            }
        }
        else if (variant == VariantType.WHITE)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteWhite;
            //Update Stats
            attributes.variantName = roleNameWhite;
            attributes.maxHP += attributes.maxHP * percentageHPWhite + flatHPWhite;
            attributes.armor += attributes.armor * percentageArmorWhite + flatArmorWhite;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedWhite + flatMoveSpeedWhite;
            attributes.resistance += attributes.resistance * percentageResistanceWhite + flatResistanceWhite;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeWhite + flatDodgeWhite;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityWhite + flatCarryCapacityWhite);
            attributes.maxShield += attributes.maxHP * percentageShieldWhite + flatShieldWhite;
            attributes.actionPower += attributes.actionPower * percentageActionPowerWhite;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerWhite;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerWhite;
            //Update Actions
            if (stealHoneyWhite == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionWhite != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionWhite, actionPrefabWhite, actionSoundWhite, actionPowerModifierWhite, actionRateModifierWhite, actionRangeModifierWhite, actionPierceModifierWhite, actionDurationWhite, actionExtraModifierWhite);
            }
            //Update Effects
            if (extraEffectWhite != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectWhite, effectPrefabWhite, effectSoundWhite, effectPowerModifierWhite, effectRateModifierWhite, effectRangeModifierWhite, effectPierceModifierWhite, effectDurationWhite, effectExtraModifierWhite);
            }
            //Update Passives
            if (extraPassiveWhite != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveWhite, passivePrefabWhite, passiveSoundWhite, passivePowerModifierWhite, passiveRateModifierWhite, passiveRangeModifierWhite, passivePierceModifierWhite, passiveDurationWhite, passiveExtraModifierWhite);
            }
        }
        else if (variant == VariantType.PURPLE)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spritePurple;
            //Update Stats
            attributes.variantName = roleNamePurple;
            attributes.maxHP += attributes.maxHP * percentageHPPurple + flatHPPurple;
            attributes.armor += attributes.armor * percentageArmorPurple + flatArmorPurple;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedPurple + flatMoveSpeedPurple;
            attributes.resistance += attributes.resistance * percentageResistancePurple + flatResistancePurple;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgePurple + flatDodgePurple;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityPurple + flatCarryCapacityPurple);
            attributes.maxShield += attributes.maxHP * percentageShieldPurple + flatShieldPurple;
            attributes.actionPower += attributes.actionPower * percentageActionPowerPurple;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerPurple;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerPurple;
            //Update Actions
            if (stealHoneyPurple == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionPurple != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionPurple, actionPrefabPurple, actionSoundPurple, actionPowerModifierPurple, actionRateModifierPurple, actionRangeModifierPurple, actionPierceModifierPurple, actionDurationPurple, actionExtraModifierPurple);
            }
            //Update Effects
            if (extraEffectPurple != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectPurple, effectPrefabPurple, effectSoundPurple, effectPowerModifierPurple, effectRateModifierPurple, effectRangeModifierPurple, effectPierceModifierPurple, effectDurationPurple, effectExtraModifierPurple);
            }
            //Update Passives
            if (extraPassivePurple != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassivePurple, passivePrefabPurple, passiveSoundPurple, passivePowerModifierPurple, passiveRateModifierPurple, passiveRangeModifierPurple, passivePierceModifierPurple, passiveDurationPurple, passiveExtraModifierPurple);
            }
        }
        else if (variant == VariantType.PINK)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spritePink;
            //Update Stats
            attributes.variantName = roleNamePink;
            attributes.maxHP += attributes.maxHP * percentageHPPink + flatHPPink;
            attributes.armor += attributes.armor * percentageArmorPink + flatArmorPink;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedPink + flatMoveSpeedPink;
            attributes.resistance += attributes.resistance * percentageResistancePink + flatResistancePink;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgePink + flatDodgePink;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityPink + flatCarryCapacityPink);
            attributes.maxShield += attributes.maxHP * percentageShieldPink + flatShieldPink;
            attributes.actionPower += attributes.actionPower * percentageActionPowerPink;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerPink;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerPink;
            //Update Actions
            if (stealHoneyPink == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionPink != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionPink, actionPrefabPink, actionSoundPink, actionPowerModifierPink, actionRateModifierPink, actionRangeModifierPink, actionPierceModifierPink, actionDurationPink, actionExtraModifierPink);
            }
            //Update Effects
            if (extraEffectPink != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectPink, effectPrefabPink, effectSoundPink, effectPowerModifierPink, effectRateModifierPink, effectRangeModifierPink, effectPierceModifierPink, effectDurationPink, effectExtraModifierPink);
            }
            //Update Passives
            if (extraPassivePink != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassivePink, passivePrefabPink, passiveSoundPink, passivePowerModifierPink, passiveRateModifierPink, passiveRangeModifierPink, passivePierceModifierPink, passiveDurationPink, passiveExtraModifierPink);
            }
        }
        else if (variant == VariantType.GOLD)
        {
            //Change Sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteGold;
            //Update Stats
            attributes.variantName = roleNameGold;
            attributes.maxHP += attributes.maxHP * percentageHPGold + flatHPGold;
            attributes.armor += attributes.armor * percentageArmorGold + flatArmorGold;
            attributes.moveSpeed += attributes.moveSpeed * percentageMoveSpeedGold + flatMoveSpeedGold;
            attributes.resistance += attributes.resistance * percentageResistanceGold + flatResistanceGold;
            attributes.dodgeChance += attributes.dodgeChance * percentageDodgeGold + flatDodgeGold;
            attributes.carryCapacity += (int)(attributes.carryCapacity * percentageCarryCapacityGold + flatCarryCapacityGold);
            attributes.maxShield += attributes.maxHP * percentageShieldGold + flatShieldGold;
            attributes.actionPower += attributes.actionPower * percentageActionPowerGold;
            attributes.effectPower += attributes.effectPower * percentageEffectPowerGold;
            attributes.passivePower += attributes.passivePower * percentagePassivePowerGold;
            //Update Actions
            if (stealHoneyGold == false && gameObject.GetComponent<Actions>().CheckAction("Steal Honey") == true)
            {
                gameObject.GetComponent<Actions>().RemoveAction("Steal Honey");
                gameObject.GetComponent<Actions>().AddAction("Attack Queen", null, SoundType.EMPTY, 1, 1, 1, 1, 1, 1);
            }
            if (extraActionGold != "")
            {
                gameObject.GetComponent<Actions>().AddAction(extraActionGold, actionPrefabGold, actionSoundGold, actionPowerModifierGold, actionRateModifierGold, actionRangeModifierGold, actionPierceModifierGold, actionDurationGold, actionExtraModifierGold);
            }
            //Update Effects
            if (extraEffectGold != "")
            {
                gameObject.GetComponent<Effects>().AddEffect(extraEffectGold, effectPrefabGold, effectSoundGold, effectPowerModifierGold, effectRateModifierGold, effectRangeModifierGold, effectPierceModifierGold, effectDurationGold, effectExtraModifierGold);
            }
            //Update Passives
            if (extraPassiveGold != "")
            {
                gameObject.GetComponent<Passives>().AddPassive(extraPassiveGold, passivePrefabGold, passiveSoundGold, passivePowerModifierGold, passiveRateModifierGold, passiveRangeModifierGold, passivePierceModifierGold, passiveDurationGold, passiveExtraModifierPink);
            }
        }
        else
        {
            Debug.Log(attributes.sName + " has invalid variant: " + variant);
        }
        //In case attributes has already awakened
        attributes.SetBaseAttributes();
    }
}
