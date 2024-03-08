using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Sound sound;
    private void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Sound>();
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tower" || collision.transform.tag == "batteru")
        {
            sound.explosion.Play();
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Vägg")
        {
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            if (sr.color.g < 0)
            {
                sound.brick.Play();
                Destroy(collision.gameObject);
            }
            sr.color = new Color(sr.color.r, sr.color.g - 0.1f, sr.color.b - 0.1f, sr.color.a);
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Player")
        {
            GameObject hp = GameObject.FindGameObjectWithTag("hp");
            hp.transform.localScale = new Vector3(hp.transform.localScale.x - 0.1f, 1, 1);
            if (hp.transform.localScale.x <= 0)
            {
                sound.Defeat();
                Time.timeScale = 0;
            }
            Destroy(gameObject);
        }
        if (collision.transform.tag == "diod")
        {
            if (!collision.gameObject.GetComponent<diodspawn>().disabled)
            {
                sound.electric.Play();
            }
            collision.gameObject.GetComponent<diodspawn>().waitTime = 5f;
        }
    }
}
