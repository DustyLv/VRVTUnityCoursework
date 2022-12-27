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

    public static GameManager Instance;

    

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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

    public void GameEnd(GameEndType _gameEndType)
    {
        GameRunning = false;
        OnGameEndInvoke();
        switch (_gameEndType)
        {
            case GameEndType.Saved:
                print("--- GAME END!   YOU GOT SAVED!!");
                UIController.Instance.ShowEndScreen(_gameEndType);
                break;
            case GameEndType.Died:
                print("--- GAME END!   YOU DIED!!");
                UIController.Instance.ShowEndScreen(_gameEndType);
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
