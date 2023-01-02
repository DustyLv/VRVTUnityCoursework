using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine

public class GameManager : MonoBehaviour
{
    public float GameLength = 30f;
    public bool GameRunning = false;

    public Action OnGameEnd;

    public GameEndType _gameEndType = GameEndType.None;

    private CutsceneController _cutsceneController;

    public static GameManager Instance;

    

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _cutsceneController = FindObjectOfType<CutsceneController>();
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        GameRunning = true;
    }

    public void GameEnd(GameEndType gameEndType)
    {
        GameRunning = false;
        OnGameEndInvoke();
        _gameEndType = gameEndType;
        switch (_gameEndType)
        {
            case GameEndType.Saved:
                print("--- GAME END!   YOU GOT SAVED!!");
                _cutsceneController.PlayCutscene_Saved();
                //UIController.Instance.ShowEndScreen(_gameEndType);
                break;
            case GameEndType.Died:
                print("--- GAME END!   YOU DIED!!");
                _cutsceneController.PlayCutscene_Died();
                //UIController.Instance.ShowEndScreen(_gameEndType);
                break;
            default:
                break;
        }
    }

    private void OnGameEndInvoke()
    {
        OnGameEnd?.Invoke();
    }
}
