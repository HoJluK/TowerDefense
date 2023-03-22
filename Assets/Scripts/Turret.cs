using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    [Header("General")]
    [SerializeField]
    private float range = 15f;

    [Header("Use Bullets(default)")]
    [SerializeField]
    private float fireRate = 1f;
    private float fireCountdown = 0f;
    [SerializeField]
    private GameObject bulletPrefab;

    [Header("Use Lazer")]
    [SerializeField]
    private bool useLazer = false;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private ParticleSystem impactEffect;
    [SerializeField]
    private Light impactLight;

    [Header("Unity Setup Fields")]
    [SerializeField]
    private string enemyTag = "Enemy";
    [SerializeField]
    private Transform partToRotate;
    [SerializeField]
    private float turnSpeed = 10f;
    
    [SerializeField]
    private Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); 
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null)
        {
            if (useLazer)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        LookOnTarget();

        if (useLazer)
        {
            Lazer();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

        
    }

    private void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Lazer()
    {
        if (!lineRenderer.enabled)  
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet!=null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
