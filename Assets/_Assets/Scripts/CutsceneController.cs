using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director_Saved;
    [SerializeField] private PlayableDirector _director_Died;

    // Start is called before the first frame update
    void Start()
    {
        _director_Saved.gameObject.SetActive(false);
        _director_Saved.stopped += OnCutsceneEnded;
        _director_Died.stopped += OnCutsceneEnded;
    }

    void OnDisable()
    {
        _director_Saved.stopped -= OnCutsceneEnded;
        _director_Died.stopped -= OnCutsceneEnded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCutscene_Saved()
    {
        StartCoroutine(PlayCutscene_Saved_Routine());
    }

    private IEnumerator PlayCutscene_Saved_Routine()
    {
        yield return new WaitForSeconds(2f);
        _director_Saved.gameObject.SetActive(true);
        _director_Saved.Play();
    }

    public void PlayCutscene_Died()
    {
        StartCoroutine(PlayCutscene_Died_Routine());
    }

    private IEnumerator PlayCutscene_Died_Routine()
    {
        yield return new WaitForSeconds(2f);
        _director_Died.gameObject.SetActive(true);
        _director_Died.Play();
    }

    private void OnCutsceneEnded(PlayableDirector director)
    {
        _director_Died.gameObject.SetActive(false);
        _director_Saved.gameObject.SetActive(false);

        UIController.Instance.ShowEndScreen();
    }
}
