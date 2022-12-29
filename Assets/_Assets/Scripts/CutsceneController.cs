using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    // Start is called before the first frame update
    void Start()
    {
        _director.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCutscene_Saved()
    {
        _director.gameObject.SetActive(true);
        _director.Play();
    }
}
