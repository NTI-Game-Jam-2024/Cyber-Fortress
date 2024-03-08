using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class highScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI TMP;
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            TMP.text = "Highscore: " + PlayerPrefs.GetString("HighScore");
        }
    }
}
