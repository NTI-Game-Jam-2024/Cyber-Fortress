using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Update is called once per frame
    private float time = 0;

    private int seconds = 0;
    private int minutes = 0;
    private string minutesS = "00";
    private string secondsS = "00";

    public TextMeshProUGUI TMP;

    public TextMeshProUGUI endTMP;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            time -= 1;
            if (seconds == 59)
            {
                seconds = 0;
                minutes++;
                if (minutes < 10)
                {
                    minutesS = "0" + minutes.ToString();
                }
                else minutesS = minutes.ToString();
            }
            else
            {
                seconds++;
                if (seconds < 10)
                {
                    secondsS = "0" + seconds.ToString();
                }
                else secondsS = seconds.ToString();
            }
            if(PlayerPrefs.HasKey("HighScore"))
            {
                string oldHs = PlayerPrefs.GetString("HighScore");
                int oldHighscore = oldHs[0]*1000 + oldHs[1]*100 + oldHs[3]*10 + oldHs[4];
                if(oldHighscore < minutes*100+seconds)
                {
                    PlayerPrefs.SetString("HighScore", minutesS + ":" + secondsS);
                }
            }
            else
            {
                PlayerPrefs.SetString("HighScore", minutesS + ":" + secondsS);
            }
        }
        TMP.text = minutesS + ":" + secondsS;
        endTMP.text = "Time: " + minutesS + ":" + secondsS;
    }
}