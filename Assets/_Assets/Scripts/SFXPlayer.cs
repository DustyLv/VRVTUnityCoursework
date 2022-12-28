using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioClip m_CarHorn;
    public AudioClip[] m_DogBarks;

    public AudioSource m_AudioSource_Horn;
    public AudioSource m_AudioSource_Dog;

    public Vector2 m_CarHorn_Pitch_MinMax = Vector2.one;
    public Vector2 m_Dog_Pitch_MinMax = Vector2.one;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource_Horn.clip = m_CarHorn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHornSound()
    {
        
        float randomPitch = Random.Range(m_CarHorn_Pitch_MinMax.x, m_CarHorn_Pitch_MinMax.y);
        m_AudioSource_Horn.pitch = randomPitch;
        m_AudioSource_Horn.Play();
    }

    public void PlayDogSound()
    {
        m_AudioSource_Dog.clip = Helpers.GetRandomAudioClipFromCollection(m_DogBarks);
        float randomPitch = Random.Range(m_Dog_Pitch_MinMax.x, m_Dog_Pitch_MinMax.y);
        m_AudioSource_Dog.pitch = randomPitch;
        m_AudioSource_Dog.Play();
    }
}
