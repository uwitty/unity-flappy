using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public GameObject pipePrefab_ = null;
    public State state_ = null;
    float lastPipeTime_ = -3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(pipePrefab_ != null);
        Debug.Assert(state_ != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (state_.state != State.GameState.Guide && Time.time - lastPipeTime_ > 3.0f)
        {
            GameObject pipe = Instantiate(pipePrefab_, transform);
            pipe.transform.Translate(8.0f, 0.0f, 0.0f);

            lastPipeTime_ = Time.time;
        }
    }
}
