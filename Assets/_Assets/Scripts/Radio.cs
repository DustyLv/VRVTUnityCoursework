using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musicClips;
    [SerializeField] private AudioClip _radioClick_On;
    [SerializeField] private AudioClip _radioClick_Off;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _volumeFadeLength = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameResume += UnpauseRadio;
        GameManager.Instance.OnGamePause += PauseRadio;
        GameManager.Instance.OnGameEnd += StopRadio;
    }

    public void PlayRandomSong()
    {
        StartCoroutine(PlayRandomSongRoutine());
    }

    private IEnumerator PlayRandomSongRoutine()
    {
        _audioSource.PlayOneShot(_radioClick_On);
        yield return new WaitForSeconds(_radioClick_On.length);
        _audioSource.volume = 0f;
        AudioClip clip = Helpers.GetRandomAudioClipFromCollection(_musicClips);
        _audioSource.DOFade(1f, _volumeFadeLength).OnStart(() =>
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        });

        yield return new WaitForSeconds(clip.length);
        _audioSource.PlayOneShot(_radioClick_Off);
    }

    public void StopRadio()
    {
        if (!_audioSource.isPlaying) return;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_radioClick_Off);
    }

    public void UnpauseRadio()
    {
        _audioSource.UnPause();
    }

    public void PauseRadio()
    {
        if (!_audioSource.isPlaying) return;
        _audioSource.Pause();
    }

    private IEnumerator StopRadioRoutine()
    {

        yield return new WaitForSeconds(_radioClick_Off.length * 2f);

    }
}

