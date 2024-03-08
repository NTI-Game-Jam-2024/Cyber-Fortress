using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    private Vector2 originalPosition;
    private bool isDragging = false;
    public GameObject activeTowerPrefab;
    private SpriteRenderer sr;
    private int collisions = 0;
    public AudioSource place;
    public TextMeshProUGUI TMP;
    public int towerCost;
    public float range;
    public GameObject rangeObject;
    private GameObject instantiatedRange;

    private void Start()
    {
        originalPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDragging)
        {
            if (collisions != 0)
            {
                sr.color = Color.red;
            }
            else
            {
                sr.color = Color.white;
            }
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }
        else
        {
            if (Int32.Parse(TMP.text) < towerCost)
            {
                sr.color = Color.gray;
            }
            else
            {
                sr.color = Color.white;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Int32.Parse(TMP.text) >= towerCost)
        {
            isDragging = true;
            instantiatedRange = Instantiate(rangeObject, transform.position, Quaternion.identity);
            instantiatedRange.transform.SetParent(transform);
            instantiatedRange.transform.localScale = new Vector3(range * 2, range * 2, 1);
        }
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;
        Destroy(instantiatedRange);
        isDragging = false;
        if (collisions == 0)
        {
            InstantiateTower();
            place.Play();
            TMP.text = (Int32.Parse(TMP.text) - towerCost).ToString();
        }
        transform.position = originalPosition;
    }

    private void InstantiateTower()
    {
        Instantiate(activeTowerPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisions++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions--;
    }
}