using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandManager : MonoBehaviour
{
    public static SandManager instance;

    void Awake()
    {
        if(instance == null)
        {            
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
