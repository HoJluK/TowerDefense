using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    public int health = 100;
    public int value = 50;
    private Transform target;
    private int wavepointIndex = 0;
    public GameObject deathEffect;
     void Start()
    {
        target = WayPoints.points[0];
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health<=0)
        {
            Die();
        }
    }

     void Die()
    {
        PlayerStats.Money += value;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position)<=0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex  >= WayPoints.points.Length -1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = WayPoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
        
    }

}
