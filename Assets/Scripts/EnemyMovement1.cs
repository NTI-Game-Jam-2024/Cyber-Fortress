using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class EnemyMovement1 : MonoBehaviour
{
    private float moveSpeed = 3f; // Adjust this to control the speed of the enemy movement
    public Rigidbody2D rb;
    private float range = 2f;
    private float Diodrange = 4f;
    private float ownRange = 3.5f;
    private float shootDelay = 3f;
    private float shootTime = 3f;
    private float bulletSpeed = 3f;
    public GameObject bulletPrefab;

    private Vector2 targetWall;
    private Sound sound;
    private TextMeshProUGUI money;
    private bool damagePlayer = false;

    private void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Sound>();
        money = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        moveSpeed = 3f;
        targetWall = Vector2.zero;
        GameObject[] batteries = GameObject.FindGameObjectsWithTag("batteru");
        foreach (GameObject battery in batteries)
        {
            float distanceToBattery = Vector3.Distance(transform.position, battery.transform.position);
            if (distanceToBattery < ownRange)
            {
                targetWall = transform.position;
                if(shootTime <= 0)
                {
                    shootTime = shootDelay;
                    Shoot(battery);
                }
            }
        }
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towers)
        {
            float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
            if (distanceToTower < ownRange)
            {
                targetWall = transform.position;
                if (shootTime <= 0)
                {
                    shootTime = shootDelay;
                    Shoot(tower);
                }
            }
        }
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Vägg");
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject wall in walls)
        {
            float distanceToWall = Vector3.Distance(transform.position, wall.transform.position);
            if (distanceToWall < shortestDistance && distanceToWall < range + ownRange)
            {
                shortestDistance = distanceToWall;
                targetWall = wall.transform.position;
            }
        }
        GameObject[] diods = GameObject.FindGameObjectsWithTag("diod");
        foreach (GameObject diod in diods)
        {
            if (diod.GetComponent<diodspawn>().disabled) continue;
            if (Vector3.Distance(transform.position, diod.transform.position) < Diodrange)
            {
                moveSpeed = moveSpeed * diod.GetComponent<diodspawn>().slowFloat;
            }
        }
        if(targetWall == (Vector2)transform.position)
        {
            shootTime -= Time.deltaTime;
        }
        Vector2 direction = (targetWall - (Vector2)transform.position).normalized; // Calculate direction towards the player
        Vector2 movement = direction * moveSpeed * Time.fixedDeltaTime; // Calculate movement based on direction, speed, and time
        rb.MovePosition((Vector2)transform.position + movement); // Move the enemy using Rigidbody's MovePosition
    }

    private void Shoot(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
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
            damagePlayer = true;
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

    private void OnDestroy()
    {
        if (damagePlayer) return;
        money.text = (Int32.Parse(money.text) + 10).ToString();
    }
}