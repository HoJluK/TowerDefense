using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float speed = 70f;
    [SerializeField]
    private float explosionRarius = 0f;
    [SerializeField]
    private GameObject impactEffect;

    public int damage = 50;

    public void Seek(Transform _target)
    {
        target = _target;        
    }
    
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude<=distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    
    }

    private void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        if (explosionRarius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRarius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e!=null)
        {
            e.TakeDamage(damage);
        }
           
    }

     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRarius);
    }

}
