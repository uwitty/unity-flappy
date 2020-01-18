using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject guideText_ = null;
    float lastTime_;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(guideText_ != null);
        lastTime_ = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        if (guideText_.activeSelf && time - lastTime_ > 1.0)
        {
            guideText_.SetActive(false);
            lastTime_ = time;
        }
        if (!guideText_.activeSelf && time - lastTime_ > 1.0)
        {
            guideText_.SetActive(true);
            lastTime_ = time;
        }
    }
}
