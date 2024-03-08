using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    private float swapTime = 0.2f;
    private float waitTime = 0f;
    public SpriteRenderer sr;
    private int index = 0;

    // Update is called once per frame
    private void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0f)
        {
            waitTime = swapTime;
            sr.sprite = sprites[index];
            index++;
            if (index == 5)
            {
                index = 0;
            }
        }
    }
}