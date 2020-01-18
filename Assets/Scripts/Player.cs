using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public State state_ = null;

    Rigidbody2D rigidbody_ = null;
    ConstantForce2D flapForce_ = null;

    int fingerId_ = -1;
    float lastOperationTime_ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(state_ != null);
        rigidbody_ = GetComponent<Rigidbody2D>();
        flapForce_ = GetComponent<ConstantForce2D>();
        flapForce_.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state_.state)
        {
            case State.GameState.Guide:
                UpdateGuide();
                break;
            case State.GameState.Flappy:
                UpdateFlappy();
                break;
            case State.GameState.GameOver:
                UpdateGameOver();
                break;
        }
    }

    private void UpdateGuide()
    {
        rigidbody_.constraints = RigidbodyConstraints2D.FreezeAll;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            State.IsTouchMode = false;
            StartGame();
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                State.IsTouchMode = true;
                fingerId_ = touch.fingerId;
                StartGame();
                break;
            }
        }
    }

    private void UpdateFlappy()
    {
        rigidbody_.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (State.IsTouchMode)
        {
            foreach (Touch touch in Input.touches)
            {
                if (fingerId_ == -1 && touch.phase == TouchPhase.Began)
                {
                    fingerId_ = touch.fingerId;
                    StartFlapping();
                    break;
                }
                else if (fingerId_ != -1 && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    fingerId_ = -1;
                    StopFlapping();
                    break;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartFlapping();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopFlapping();
            }
        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            RestartScene();
        }
    }

    private void UpdateGameOver()
    {
        //StopFlapping();
        flapForce_.enabled = false;

        if (Time.time - lastOperationTime_ < 1.0f)
        {
            return;
        }

        if (State.IsTouchMode)
        {
            foreach (Touch touch in Input.touches)
            {
                if (fingerId_ != -1 && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    fingerId_ = -1;
                    break;
                }
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    fingerId_ = -1;
                    RestartScene();
                    break;
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                RestartScene();
            }
        }
    }

    private void StartGame()
    {
        flapForce_.enabled = true;
        state_.state = State.GameState.Flappy;
    }

    private static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartFlapping()
    {
        flapForce_.enabled = true;
        lastOperationTime_ = Time.time;
    }

    private void StopFlapping()
    {
        flapForce_.enabled = false;
        lastOperationTime_ = Time.time;
    }

    private void Reset()
    {
        rigidbody_.velocity = Vector3.zero;
        transform.position  = Vector3.zero;
    }
}
