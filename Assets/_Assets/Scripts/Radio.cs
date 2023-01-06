using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Radio : MonoBehaviour
{
    public AudioClip[] m_MusicClips;
    public AudioClip m_RadioClick_On;
    public AudioClip m_RadioClick_Off;
    public AudioSource m_AudioSource;

    public float m_VolumeFadeLength = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameResume += UnpauseRadio;
        GameManager.Instance.OnGamePause += PauseRadio;
        GameManager.Instance.OnGameEnd += StopRadio;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayRandomSong()
    {
        StartCoroutine(PlayRandomSongRoutine());
    }

    private IEnumerator PlayRandomSongRoutine()
    {
        m_AudioSource.PlayOneShot(m_RadioClick_On);
        yield return new WaitForSeconds(m_RadioClick_On.length);
        m_AudioSource.volume = 0f;
        m_AudioSource.DOFade(1f, m_VolumeFadeLength).OnStart(() =>
        {
            m_AudioSource.clip = Helpers.GetRandomAudioClipFromCollection(m_MusicClips);
            m_AudioSource.Play();
        });

    }

    public void StopRadio()
    {
        m_AudioSource.Stop();
        m_AudioSource.PlayOneShot(m_RadioClick_Off);
    }

    public void UnpauseRadio()
    {
        m_AudioSource.UnPause();
    }

    public void PauseRadio()
    {
        if (!m_AudioSource.isPlaying) return;
        m_AudioSource.Pause();
    }

    private IEnumerator StopRadioRoutine()
    {

        yield return new WaitForSeconds(m_RadioClick_Off.length * 2f);

    }
}

