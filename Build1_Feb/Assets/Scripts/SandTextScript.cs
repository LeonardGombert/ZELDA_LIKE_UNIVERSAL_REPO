﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandTextScript : MonoBehaviour
{
    Text text;
    public static int sandAmount;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update ()
    {
        text.text = sandAmount.ToString();
    }

}
