using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public static int BestScore { get; set; }
    public static int Count { get; private set; }

    public static bool IsTouchMode { get; set; }

    private int score_;
    public int Score
    {
        get
        {
            return score_;
        }
        private set
        {
            score_ = value;
            if (score_ > BestScore)
            {
                BestScore = score_;
            }
        }
    }

    private GameState state_ = GameState.Guide;
    public GameState state
    {
        get
        {
            return state_;
        }
        set
        {
             state_ = value;
        }
    }

    private void Awake()
    {
        if (Count > 0)
        {
            state = GameState.Flappy;
        }
        Count++;
        Score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public enum GameState
    {
        Guide,
        Flappy,
        GameOver,
    }

    public void IncrementScore()
    {
        Score++;
    }
}
