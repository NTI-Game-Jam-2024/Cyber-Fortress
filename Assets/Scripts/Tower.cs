using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;
    public LineRenderer lineRenderer;

    private GameObject targetEnemy;
    private Vector2 enemyPosition;
    private float fireCountdown = 0f;
    private float sustainTime = 0f;
    private float sustainDelay = 0.1f;
    private Sound sound;
    private float buffduration = 5f;

    private void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Sound>();
    }

    private void Update()
    {
        if (fireRate < 1f) buffduration -= Time.deltaTime;
        if (buffduration < 0f)
        {
            fireRate = 1f;
            buffduration = 5f;
        }
        if (fireCountdown <= 0f)
        {
            FindTarget();
            if (targetEnemy == null)
            {
                return;
            }
            fireCountdown = fireRate;
            sustainTime = sustainDelay;
            enemyPosition = targetEnemy.transform.position;
            Destroy(targetEnemy);
            sound.laser.Play();
        }

        sustainTime -= Time.deltaTime;
        fireCountdown -= Time.deltaTime;

        if (sustainTime > 0)
        {
            DrawLaser();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void FindTarget()
    {
        targetEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy < range)
            {
                shortestDistance = distanceToEnemy;
                targetEnemy = enemy;
            }
        }
    }

    private void DrawLaser()
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, enemyPosition);
    }
}