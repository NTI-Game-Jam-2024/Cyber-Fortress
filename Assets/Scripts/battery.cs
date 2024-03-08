using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class battery : MonoBehaviour
{
    private GameObject areaObject;
    public GameObject area;
    private int range = 5;
    private float buffTime = 5f;
    private float buffDelay = 10f;
    private Sound sound;
    private float buffduration = 0f;
    private bool off = true;

    private void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Sound>();
        areaObject = Instantiate(area, transform.position, Quaternion.identity);
        areaObject.transform.SetParent(transform);
        areaObject.transform.localScale = new Vector3(range * 2, range * 2, 1);
        areaObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 0.05f);
    }

    private void Update()
    {
        buffTime -= Time.deltaTime;
        buffduration -= Time.deltaTime;
        if (buffduration <= 0 && !off)
        {
            off = true;
            areaObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 0.05f);
        }
        if (buffTime <= 0f)
        {
            off = false;
            areaObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 0.2f);
            sound.poweup.Play();
            buffTime = buffDelay;
            buffduration = 5f;
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Vägg");
            foreach (GameObject wall in walls)
            {
                float distanceToWall = Vector3.Distance(transform.position, wall.transform.position);
                if (distanceToWall < range)
                {
                    SpriteRenderer sr = wall.gameObject.GetComponent<SpriteRenderer>();
                    if (sr.color.g == 1) continue;
                    if (sr.color.g == 0.9)
                    {
                        sr.color = new Color(sr.color.r, sr.color.g + 0.1f, sr.color.b + 0.1f, sr.color.a);
                    }
                    else
                    {
                        sr.color = new Color(sr.color.r, sr.color.g + 0.2f, sr.color.b + 0.2f, sr.color.a);
                    }
                }
            }
            GameObject[] diods = GameObject.FindGameObjectsWithTag("diod");
            foreach (GameObject diod in diods)
            {
                float distanceToDiod = Vector3.Distance(transform.position, diod.transform.position);
                if (distanceToDiod < range)
                {
                    diod.gameObject.GetComponent<diodspawn>().slowFloat = diod.gameObject.GetComponent<diodspawn>().slowFloat * 0.75f;
                }
            }
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            foreach (GameObject tower in towers)
            {
                float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
                if (distanceToTower < range)
                {
                    tower.gameObject.GetComponent<Tower>().fireRate = tower.gameObject.GetComponent<Tower>().fireRate * 0.75f;
                }
            }
        }
    }
}