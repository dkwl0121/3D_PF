using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgmSource;
    private AudioSource[] efxSource;
    private int nMaxEftCnt;

    public AudioClip[] bgmClips;
    public AudioClip[] effectClips;

    private bool isBgmOn = true;
    public bool BgmOn { get { return isBgmOn; } set { isBgmOn = value; } }
    private bool isEftOn = true;
    public bool EftOn { get { return isEftOn; } set { isEftOn = value; } }

    // 싱글톤 패턴
    private static SoundManager sInstance;
    public static SoundManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject gObject = new GameObject("_SoundManager");
                sInstance = gObject.AddComponent<SoundManager>();
            }
            return sInstance;
        }
    }

    public void Setup()
    {
        // 오디오 소스 컴퍼넌트 추가
        bgmSource = this.gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        nMaxEftCnt = 5;
        efxSource = new AudioSource[nMaxEftCnt];
        for (int i = 0; i < nMaxEftCnt; ++i)
        {
            efxSource[i] = this.gameObject.AddComponent<AudioSource>();
        }
        //efxSource.loop = false; // 기본값

        bgmClips = Resources.LoadAll<AudioClip>(Util.ResourcePath.BGM_FOLDER);
        //effectClips = Resources.LoadAll<AudioClip>(Util.ResourcePath.EFFECT_FOLDER);

        effectClips = new AudioClip[(int)E_EFT_SOUND_LIST.MAX];
        effectClips[(int)E_EFT_SOUND_LIST.PLAYER_ATT] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/1.Player_Att");
        effectClips[(int)E_EFT_SOUND_LIST.PLAYER_SKILL_01] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/2.Player_Skill_01");
        effectClips[(int)E_EFT_SOUND_LIST.PLAYER_SKILL_02] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/3.Player_Skill_02");
        effectClips[(int)E_EFT_SOUND_LIST.PLAYER_SKILL_03] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/4.Player_Skill_03");
        effectClips[(int)E_EFT_SOUND_LIST.PLAYER_DIE] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/5.Player_Die");
        effectClips[(int)E_EFT_SOUND_LIST.PLAYER_LEVEL_UP] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/6.Levelup");
        effectClips[(int)E_EFT_SOUND_LIST.ENEMY_ATT] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/7.Enemy_Att");
        effectClips[(int)E_EFT_SOUND_LIST.ENEMY_SKILL] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/8.Enemy_Skill");
        effectClips[(int)E_EFT_SOUND_LIST.ENEMY_DIE] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/9.Enemy_Die");
        effectClips[(int)E_EFT_SOUND_LIST.ENEMY_BOSS] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/10.Enemy_Boss");
        effectClips[(int)E_EFT_SOUND_LIST.SELECT] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/11.Select");
        effectClips[(int)E_EFT_SOUND_LIST.COIN] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/12.Coin");
        effectClips[(int)E_EFT_SOUND_LIST.POPUP] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/13.Popup");
        effectClips[(int)E_EFT_SOUND_LIST.WIN] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/14.Win");
        effectClips[(int)E_EFT_SOUND_LIST.LOSE] = Resources.Load<AudioClip>(Util.ResourcePath.EFFECT_FOLDER + "/15.Lose");

        DontDestroyOnLoad(this.gameObject);
    }

    public float GetVolume()
    {
        return bgmSource.volume;
    }

    public void PlayBgm(E_BGM_SOUND_LIST eBgm)
    {
        if (!isBgmOn) return;

        bgmSource.clip = bgmClips[(int)eBgm];
        bgmSource.Play();
    }

    //public void PlayRandomMusic(AudioClip[] clips)
    //{
    //    int randIndex = Random.Range(0, clips.Length);

    //    bgmSource.clip = clips[randIndex];
    //    bgmSource.Play();
    //}

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void SetBgmVolume(float fvolume) // volume = 0 ~ 1
    {
        bgmSource.volume = fvolume;
    }

    public void PlayEfx(E_EFT_SOUND_LIST eEft)
    {
        if (!isEftOn) return;

        // 재생중이 아닌 오디오를 찾아서 재생
        int Index = GetCurrEftIndex();
        if (Index == Util.NumValue.INVALID_NUMBER) return;

        efxSource[Index].clip = effectClips[(int)eEft];
        efxSource[Index].Play();
    } 

    private int GetCurrEftIndex()
    {
        for (int i = 0; i < nMaxEftCnt; ++i)
        {
            if (!efxSource[i].isPlaying)
                return i;
        }

        return Util.NumValue.INVALID_NUMBER;
    }

    public void SetEftVolume(float fvolume) // volume = 0 ~ 1
    {
        for (int i = 0; i < nMaxEftCnt; ++i)
        {
            efxSource[i].volume = fvolume;
        }
    }

    //// 랜덤 피치를 결정하기 위한 값
    //public float lowPitchRange = 0.55f;
    //public float highPitchRange = 1.55f;
    //public void PlayEfxRndPitch(AudioClip clip)
    //{
    //    efxSource.pitch = Random.Range(lowPitchRange, highPitchRange);
    //    efxSource.clip = clip;
    //    efxSource.Play();
    //}
}
