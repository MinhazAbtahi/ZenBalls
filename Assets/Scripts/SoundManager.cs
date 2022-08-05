using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : SingletonPersistent<SoundManager>
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource digAudioSource;

    public AudioClip MenuBG;
    public AudioClip GameBG;
    public AudioClip DigSFX;
    public AudioClip BallBumpSFX;
    public AudioClip ObjectBumpSFX;
    public AudioClip StarSFX;
    public AudioClip TapButton;
    public AudioClip VictorySFX;
    public AudioClip DefeatSFX;

    [HideInInspector] public Slider volumeSlider;
    [HideInInspector] public Slider soundEffectVolumeSlider;

    private bool isDigPlaying;
    private bool isDigHaptic;
    private bool isBallBumpPlaying;
    private bool isObjBumpPlaying;

    private WaitForSeconds ballDelay = new WaitForSeconds(.25f);
    private WaitForSeconds objDelay = new WaitForSeconds(.1f);
    public static WaitForSeconds digDelay = new WaitForSeconds(.15f);

    void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();
        digAudioSource = GetComponentInChildren<AudioSource>();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip);
    }

    public void PlayTapSFX()
    {
        PlaySFX(TapButton);
    }

    public void PlayDigSFX()
    {
        if (!isDigPlaying)
        {
            isDigPlaying = true;
            digAudioSource.PlayOneShot(DigSFX);
            isDigHaptic = true;
            StartCoroutine(DigHaptic());
        }
    }

    public void StopDigSFX()
    {
        digAudioSource.Stop();
        isDigPlaying = false;
        isDigHaptic = false;
        StopCoroutine(DigHaptic());
    }

    private IEnumerator DigHaptic()
    {
        while (true && isDigHaptic)
        {
            HapticController.PlayHaptic(HapticController.HapticType.Light);
            yield return digDelay;
            digDelay = new WaitForSeconds(Random.Range(.1f, .2f));
            HapticController.PlayHaptic(HapticController.HapticType.Soft);
            yield return digDelay;
        }
    }


    public void PlayBallBumpSFX()
    {
        if (!isBallBumpPlaying)
        {
            isBallBumpPlaying = true;
            PlaySFX(BallBumpSFX);
            StartCoroutine(BallBumpCoolDown());
        }
    }

    public void PlayObjectBumpSFX()
    {
        if (!isObjBumpPlaying)
        {
            isObjBumpPlaying = true;
            PlaySFX(ObjectBumpSFX);
            HapticController.PlayHaptic(HapticController.HapticType.Soft);
            StartCoroutine(ObjBumpCoolDown());
        }
    }

    public void PlayStarSFX()
    {
        PlaySFX(StarSFX);
    }

    public void PlayVictorySFX()
    {
        PlaySFX(VictorySFX);
    }

    public void PlayDefeatSFX()
    {
        PlaySFX(DefeatSFX);
    }

    private IEnumerator BallBumpCoolDown()
    {
        yield return ballDelay;
        isBallBumpPlaying = false;
    }

    private IEnumerator ObjBumpCoolDown()
    {
        yield return objDelay;
        isObjBumpPlaying = false;
    }

    //public void SetVolume()
    //{
    //    backgroundAudioSource.volume = volumeSlider.value;
    //    PlayerPrefs.SetFloat("volume", backgroundAudioSource.volume);
    //}

    //public void SetSoundEffectVolume()
    //{
    //    sfxAuidoSource.volume = soundEffectVolumeSlider.value;
    //    PlayerPrefs.SetFloat("soundEffectVolume", sfxAuidoSource.volume);
    //}
}
