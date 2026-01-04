using UnityEngine;

public enum SoundType
{
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
    INSECTDIE,
    EFFECT
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager main;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip[][] soundList;
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
    [SerializeField] private AudioClip[] INSECTDIE;


    void Awake()
    {
        main = this;
        soundList = new AudioClip[][] { SHOOTSMALL, SHOOTMEDIUM, SHOOTLARGE, SHOOTTHINSMALL, SHOOTTHINMEDIUM, SHOOTTHINLARGE, SHOOTROCKETSMALL, SHOOTROCKETMEDIUM, SHOOTROCKETLARGE, SHOOTBALLSMALL, SHOOTBALLMEDIUM, SHOOTBALLLARGE, INSECTDIE};
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType sound, float volume = 1)
    {
        System.Random RandomGen = new System.Random();
        int index = RandomGen.Next(soundList[(int)sound].Length - 1);
        audioSource.pitch = Random.Range(GlobalValues.main.minPitch, GlobalValues.main.maxPitch);
        audioSource.PlayOneShot(soundList[(int)sound][index], volume);
    }
}
