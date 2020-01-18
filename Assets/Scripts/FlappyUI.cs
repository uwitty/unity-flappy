using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlappyUI : MonoBehaviour
{
    public State state_ = null;
    public Text countText_ = null;
    public Text scoreText_ = null;
    public GameObject guide_ = null;
    public GameObject gameOver_ = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(countText_ != null);
        countText_.text = "Count: " + State.Count;

        Debug.Assert(state_ != null);
        Debug.Assert(guide_ != null);
        Debug.Assert(gameOver_ != null);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state_.state)
        {
            case State.GameState.Guide:
                gameOver_.SetActive(false);
                guide_.SetActive(true);
                break;
            case State.GameState.Flappy:
                gameOver_.SetActive(false);
                guide_.SetActive(false);
                UpdateScore();
                break;
            case State.GameState.GameOver:
                gameOver_.SetActive(true);
                guide_.SetActive(false);
                break;
        }
    }

    void UpdateScore()
    {
        scoreText_.text = "Score (Best): " + state_.Score + " (" + State.BestScore +")";
    }
}
