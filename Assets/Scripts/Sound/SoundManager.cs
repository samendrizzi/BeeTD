using UnityEngine;

public enum SoundType
{
    EMPTY,
    //UI
    CLICKPOSITIVE,
    CLICKNEGATIVE,
    CLICKTOGGLE,
    CLICKINTERESTING,
    VICTORY,
    DEFEAT,
    SELLING,
    WAVESTART,
    UIPLACE1,
    UIPLACE2,
    UIPLACE3,
    UIPLACE4,
    UIPLACE5,
    UIPLACE6,
    UIPLACE7,
    UIPLACE8,
    UIPLACE9,
    UIPLACE10,

    //Environment
    BEE1,
    BEE2,
    BEES1,
    BEES2,
    BEES3,
    FLOWERSAPPED,
    HONEYCOMB,
    NECTAR,
    ENVIRONEMTNPLACE1,
    ENVIRONEMTNPLACE2,
    ENVIRONEMTNPLACE3,
    ENVIRONEMTNPLACE4,
    ENVIRONEMTNPLACE5,
    ENVIRONEMTNPLACE6,
    ENVIRONEMTNPLACE7,
    ENVIRONEMTNPLACE8,
    ENVIRONEMTNPLACE9,
    ENVIRONEMTNPLACE10,

    //Towers
    SHOOTSMALL,
    SHOOTMEDIUM,
    SHOOTLARGE,
    SHOOTTHINSMALL,
    SHOOTTHINMEDIUM,
    SHOOTTHINLARGE,
    SHOOTROCKETSMALL,
    SHOOTROCKETMEDIUM,
    SHOOTROCKETLARGE,
    SHOOTBALLSMALL,
    SHOOTBALLMEDIUM,
    SHOOTBALLLARGE,
    PULSESLOW1,
    PULSESLOW2,
    PULSEBUFF1,
    PULSEBUFF2,
    PULSEBUFF3,
    PULSEHEAL1,
    PULSEHEAL2,
    TOWERSPLACE1,
    TOWERSPLACE2,
    TOWERSPLACE3,
    TOWERSPLACE4,
    TOWERSPLACE5,
    TOWERSPLACE6,
    TOWERSPLACE7,
    TOWERSPLACE8,
    TOWERSPLACE9,
    TOWERSPLACE10,

    //Projectiles
    PROJECTILEEXPLOSIONLARGE1,
    PROJECTILEEXPLOSIONLARGE2,
    PROJECTILEEXPLOSIONLARGE3,
    PROJECTILEEXPLOSIONMEDIUM1,
    PROJECTILEEXPLOSIONMEDIUM2,
    PROJECTILEEXPLOSIONMEDIUM3,
    PROJECTILEEXPLOSIONSMALL1,
    PROJECTILEEXPLOSIONSMALL2,
    PROJECTILEEXPLOSIONSMALL3,
    PROJECTILEEXPLOSIONSLOW1,
    PROJECTILEEXPLOSIONSLOW2,
    PROJECTILEEXPLOSIONSLOW3,
    PROJECTILERICOCHET1,
    PROJECTILERICOCHET2,
    PROJECTILERICOCHET3,
    PROJECTILEPLACE1,
    PROJECTILEPLACE2,
    PROJECTILEPLACE3,
    PROJECTILEPLACE4,
    PROJECTILEPLACE5,
    PROJECTILEPLACE6,
    PROJECTILEPLACE7,
    PROJECTILEPLACE8,
    PROJECTILEPLACE9,
    PROJECTILEPLACE10,

    //Mobs
    INSECTDIE1,
    INSECTDIE2,
    INSECTDIE3,
    INSECTDIE4,
    INSECTDIE5,
    CHICKDIE,
    HENDIE,
    ROOSTERDIE,
    HUMMINGBIRDDIE,
    SKUNKDIE,
    SKUNKSPRAY,
    FLEASPAWN,
    LADYBUGHEAL,
    INSECTEGGSPAWN,
    INSECTHATCH,
    EGGSPAWN,
    EGGHATCH,
    MOBSPLACE1,
    MOBSPLACE2,
    MOBSPLACE3,
    MOBSPLACE4,
    MOBSPLACE5,
    MOBSPLACE6,
    MOBSPLACE7,
    MOBSPLACE8,
    MOBSPLACE9,
    MOBSPLACE10,

    //Added Later

}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager main;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip[][] soundList;
    [Header("UI")]
    [Header("__________________________")]
    [SerializeField] private AudioClip[] CLICKPOSITIVE;
    [SerializeField] private AudioClip[] CLICKNEGATIVE;
    [SerializeField] private AudioClip[] CLICKTOGGLE;
    [SerializeField] private AudioClip[] CLICKINTERESTING;
    [SerializeField] private AudioClip[] VICTORY;
    [SerializeField] private AudioClip[] DEFEAT;
    [SerializeField] private AudioClip[] SELLING;
    [SerializeField] private AudioClip[] WAVESTART;
    [SerializeField] private AudioClip[] UIPLACE1;
    [SerializeField] private AudioClip[] UIPLACE2;
    [SerializeField] private AudioClip[] UIPLACE3;
    [SerializeField] private AudioClip[] UIPLACE4;
    [SerializeField] private AudioClip[] UIPLACE5;
    [SerializeField] private AudioClip[] UIPLACE6;
    [SerializeField] private AudioClip[] UIPLACE7;
    [SerializeField] private AudioClip[] UIPLACE8;
    [SerializeField] private AudioClip[] UIPLACE9;
    [SerializeField] private AudioClip[] UIPLACE10;

    [Header("Environment")]
    [Header("__________________________")]
    [SerializeField] private AudioClip[] BEE1;
    [SerializeField] private AudioClip[] BEE2;
    [SerializeField] private AudioClip[] BEES1;
    [SerializeField] private AudioClip[] BEES2;
    [SerializeField] private AudioClip[] BEES3;
    [SerializeField] private AudioClip[] FLOWERSAPPED;
    [SerializeField] private AudioClip[] HONEYCOMB;
    [SerializeField] private AudioClip[] NECTAR;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE1;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE2;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE3;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE4;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE5;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE6;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE7;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE8;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE9;
    [SerializeField] private AudioClip[] ENVIRONEMTNPLACE10;

    [Header("Towers")]
    [Header("__________________________")]
    [SerializeField] private AudioClip[] SHOOTSMALL;
    [SerializeField] private AudioClip[] SHOOTMEDIUM;
    [SerializeField] private AudioClip[] SHOOTLARGE;
    [SerializeField] private AudioClip[] SHOOTTHINSMALL;
    [SerializeField] private AudioClip[] SHOOTTHINMEDIUM;
    [SerializeField] private AudioClip[] SHOOTTHINLARGE;
    [SerializeField] private AudioClip[] SHOOTROCKETSMALL;
    [SerializeField] private AudioClip[] SHOOTROCKETMEDIUM;
    [SerializeField] private AudioClip[] SHOOTROCKETLARGE;
    [SerializeField] private AudioClip[] SHOOTBALLSMALL;
    [SerializeField] private AudioClip[] SHOOTBALLMEDIUM;
    [SerializeField] private AudioClip[] SHOOTBALLLARGE;
    [SerializeField] private AudioClip[] PULSESLOW1;
    [SerializeField] private AudioClip[] PULSESLOW2;
    [SerializeField] private AudioClip[] PULSEBUFF1;
    [SerializeField] private AudioClip[] PULSEBUFF2;
    [SerializeField] private AudioClip[] PULSEBUFF3;
    [SerializeField] private AudioClip[] PULSEHEAL1;
    [SerializeField] private AudioClip[] PULSEHEAL2;
    [SerializeField] private AudioClip[] TOWERSPLACE1;
    [SerializeField] private AudioClip[] TOWERSPLACE2;
    [SerializeField] private AudioClip[] TOWERSPLACE3;
    [SerializeField] private AudioClip[] TOWERSPLACE4;
    [SerializeField] private AudioClip[] TOWERSPLACE5;
    [SerializeField] private AudioClip[] TOWERSPLACE6;
    [SerializeField] private AudioClip[] TOWERSPLACE7;
    [SerializeField] private AudioClip[] TOWERSPLACE8;
    [SerializeField] private AudioClip[] TOWERSPLACE9;
    [SerializeField] private AudioClip[] TOWERSPLACE10;

    [Header("Projectiles")]
    [Header("__________________________")]
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONLARGE1;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONLARGE2;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONLARGE3;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONMEDIUM1;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONMEDIUM2;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONMEDIUM3;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONSMALL1;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONSMALL2;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONSMALL3;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONSLOW1;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONSLOW2;
    [SerializeField] private AudioClip[] PROJECTILEEXPLOSIONSLOW3;
    [SerializeField] private AudioClip[] PROJECTILERICOCHET1;
    [SerializeField] private AudioClip[] PROJECTILERICOCHET2;
    [SerializeField] private AudioClip[] PROJECTILERICOCHET3;
    [SerializeField] private AudioClip[] PROJECTILEPLACE1;
    [SerializeField] private AudioClip[] PROJECTILEPLACE2;
    [SerializeField] private AudioClip[] PROJECTILEPLACE3;
    [SerializeField] private AudioClip[] PROJECTILEPLACE4;
    [SerializeField] private AudioClip[] PROJECTILEPLACE5;
    [SerializeField] private AudioClip[] PROJECTILEPLACE6;
    [SerializeField] private AudioClip[] PROJECTILEPLACE7;
    [SerializeField] private AudioClip[] PROJECTILEPLACE8;
    [SerializeField] private AudioClip[] PROJECTILEPLACE9;
    [SerializeField] private AudioClip[] PROJECTILEPLACE10;

    [Header("Mobs")]
    [Header("__________________________")]
    [SerializeField] private AudioClip[] INSECTDIE1;
    [SerializeField] private AudioClip[] INSECTDIE2;
    [SerializeField] private AudioClip[] INSECTDIE3;
    [SerializeField] private AudioClip[] INSECTDIE4;
    [SerializeField] private AudioClip[] INSECTDIE5;
    [SerializeField] private AudioClip[] CHICKDIE;
    [SerializeField] private AudioClip[] HENDIE;
    [SerializeField] private AudioClip[] ROOSTERDIE;
    [SerializeField] private AudioClip[] HUMMINGBIRDDIE;
    [SerializeField] private AudioClip[] SKUNKDIE;
    [SerializeField] private AudioClip[] SKUNKSPRAY;
    [SerializeField] private AudioClip[] FLEASPAWN;
    [SerializeField] private AudioClip[] LADYBUGHEAL;
    [SerializeField] private AudioClip[] INSECTEGGSPAWN;
    [SerializeField] private AudioClip[] INSECTHATCH;
    [SerializeField] private AudioClip[] EGGSPAWN;
    [SerializeField] private AudioClip[] EGGHATCH;
    [SerializeField] private AudioClip[] MOBSPLACE1;
    [SerializeField] private AudioClip[] MOBSPLACE2;
    [SerializeField] private AudioClip[] MOBSPLACE3;
    [SerializeField] private AudioClip[] MOBSPLACE4;
    [SerializeField] private AudioClip[] MOBSPLACE5;
    [SerializeField] private AudioClip[] MOBSPLACE6;
    [SerializeField] private AudioClip[] MOBSPLACE7;
    [SerializeField] private AudioClip[] MOBSPLACE8;
    [SerializeField] private AudioClip[] MOBSPLACE9;
    [SerializeField] private AudioClip[] MOBSPLACE10;

    void Awake()
    {
        main = this;
        soundList = new AudioClip[][] 
        { 
            new AudioClip[] { },
            CLICKPOSITIVE,
            CLICKNEGATIVE,
            CLICKTOGGLE,
            CLICKINTERESTING,
            VICTORY,
            DEFEAT,
            SELLING,
            WAVESTART,
            UIPLACE1,
            UIPLACE2,
            UIPLACE3,
            UIPLACE4,
            UIPLACE5,
            UIPLACE6,
            UIPLACE7,
            UIPLACE8,
            UIPLACE9,
            UIPLACE10,
            BEE1,
            BEE2,
            BEES1,
            BEES2,
            BEES3,
            FLOWERSAPPED,
            HONEYCOMB,
            NECTAR,
            ENVIRONEMTNPLACE1,
            ENVIRONEMTNPLACE2,
            ENVIRONEMTNPLACE3,
            ENVIRONEMTNPLACE4,
            ENVIRONEMTNPLACE5,
            ENVIRONEMTNPLACE6,
            ENVIRONEMTNPLACE7,
            ENVIRONEMTNPLACE8,
            ENVIRONEMTNPLACE9,
            ENVIRONEMTNPLACE10,
            SHOOTSMALL,
            SHOOTMEDIUM,
            SHOOTLARGE,
            SHOOTTHINSMALL,
            SHOOTTHINMEDIUM,
            SHOOTTHINLARGE,
            SHOOTROCKETSMALL,
            SHOOTROCKETMEDIUM,
            SHOOTROCKETLARGE,
            SHOOTBALLSMALL,
            SHOOTBALLMEDIUM,
            SHOOTBALLLARGE,
            PULSESLOW1,
            PULSESLOW2,
            PULSEBUFF1,
            PULSEBUFF2,
            PULSEBUFF3,
            PULSEHEAL1,
            PULSEHEAL2,
            TOWERSPLACE1,
            TOWERSPLACE2,
            TOWERSPLACE3,
            TOWERSPLACE4,
            TOWERSPLACE5,
            TOWERSPLACE6,
            TOWERSPLACE7,
            TOWERSPLACE8,
            TOWERSPLACE9,
            TOWERSPLACE10,
            PROJECTILEEXPLOSIONLARGE1,
            PROJECTILEEXPLOSIONLARGE2,
            PROJECTILEEXPLOSIONLARGE3,
            PROJECTILEEXPLOSIONMEDIUM1,
            PROJECTILEEXPLOSIONMEDIUM2,
            PROJECTILEEXPLOSIONMEDIUM3,
            PROJECTILEEXPLOSIONSMALL1,
            PROJECTILEEXPLOSIONSMALL2,
            PROJECTILEEXPLOSIONSMALL3,
            PROJECTILEEXPLOSIONSLOW1,
            PROJECTILEEXPLOSIONSLOW2,
            PROJECTILEEXPLOSIONSLOW3,
            PROJECTILERICOCHET1,
            PROJECTILERICOCHET2,
            PROJECTILERICOCHET3,
            PROJECTILEPLACE1,
            PROJECTILEPLACE2,
            PROJECTILEPLACE3,
            PROJECTILEPLACE4,
            PROJECTILEPLACE5,
            PROJECTILEPLACE6,
            PROJECTILEPLACE7,
            PROJECTILEPLACE8,
            PROJECTILEPLACE9,
            PROJECTILEPLACE10,
            INSECTDIE1,
            INSECTDIE2,
            INSECTDIE3,
            INSECTDIE4,
            INSECTDIE5,
            CHICKDIE,
            HENDIE,
            ROOSTERDIE,
            HUMMINGBIRDDIE,
            SKUNKDIE,
            SKUNKSPRAY,
            FLEASPAWN,
            LADYBUGHEAL,
            INSECTEGGSPAWN,
            INSECTHATCH,
            EGGSPAWN,
            EGGHATCH,
            MOBSPLACE1,
            MOBSPLACE2,
            MOBSPLACE3,
            MOBSPLACE4,
            MOBSPLACE5,
            MOBSPLACE6,
            MOBSPLACE7,
            MOBSPLACE8,
            MOBSPLACE9,
            MOBSPLACE10
        };
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType sound, float pitchChange = 1f, float volume = 1)
    {
        System.Random RandomGen = new System.Random();
        if (soundList[(int)sound].Length == 0)
        {
            return;
        }
        if (pitchChange == 1f)
        {
            pitchChange = GlobalValues.main.pitchChange;
        }
        int index = RandomGen.Next(soundList[(int)sound].Length - 1);
        audioSource.pitch = Random.Range(1f - pitchChange, 1f + pitchChange);
        audioSource.PlayOneShot(soundList[(int)sound][index], volume);
    }
}
