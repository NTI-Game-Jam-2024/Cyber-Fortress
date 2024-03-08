using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diodspawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject area;

    public float waitTime = 0f;
    public bool disabled = false;
    private int range = 4;
    private GameObject areaObject;
    public float slowFloat = 0.5f;
    private float buffduration = 5f;

    private void Awake()
    {
        areaObject = Instantiate(area, transform.position, Quaternion.identity);
        areaObject.transform.SetParent(transform);
        areaObject.transform.localScale = new Vector3(range * 2, range * 2, 1);
    }

    private void Update()
    {
        if (slowFloat < 0.5f) buffduration -= Time.deltaTime;
        if (buffduration < 0f)
        {
            slowFloat = 0.5f;
            buffduration = 5f;
        }
        waitTime -= (Time.deltaTime);
        if (waitTime <= 0f)
        {
            areaObject.SetActive(true);
            disabled = false;
        }
        else
        {
            disabled = true;
            areaObject.SetActive(false);
        }
    }
}