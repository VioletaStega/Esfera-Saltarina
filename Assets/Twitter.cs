using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitter : MonoBehaviour
{
    public int frameInterval;

    void Update()
    {
        if(Time.frameCount % frameInterval == 0)
        {
            Debug.Log("I am a Jedi");
        }
    }
}
