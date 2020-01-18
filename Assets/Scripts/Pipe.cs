using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject upper_ = null;
    public GameObject under_ = null;

    bool passed_ = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(upper_ != null);
        Debug.Assert(under_ != null);

        upper_.GetComponent<Rigidbody2D>().velocity = new Vector2(-2.0f, 0.0f);
        under_.GetComponent<Rigidbody2D>().velocity = new Vector2(-2.0f, 0.0f);

        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (upper_.transform.position.x < -8.0f)
        {
            Destroy(gameObject);
        }
        if (!passed_ && upper_.transform.position.x < 0.0f && under_.transform.position.x < 0.0f)
        {
            FindObjectOfType<State>().IncrementScore();
            passed_ = true;
        }
    }

    void RandomPos()
    {
        float pos = Random.Range(-3.5f, 3.5f);
        float gap = Random.Range(2.0f, 4.0f);
        upper_.transform.position = new Vector3(transform.position.x,   5.0f + gap / 2.0f + pos, 0.0f);
        under_.transform.position = new Vector3(transform.position.x, - 5.0f - gap / 2.0f + pos, 0.0f);
    }
}
