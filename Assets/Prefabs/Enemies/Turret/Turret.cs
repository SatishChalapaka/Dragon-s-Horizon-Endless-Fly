using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour
{
    public float attackRange = 8f;
    public float cooldown = 3f;
    public int damage = 10;
    [SerializeField] private Transform player;
    private float nextAttack;
    public Transform bulletSpawnposition0,bulletSpawnposition1, bulletSpawnposition2;
    public bool isSingleBarrel, isDoubleBarrel;
    public ProjectilePool projectilePool;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        
        float dist = Vector3.Distance(transform.position, player.position);

        Vector3 direction = (player.position - transform.position).normalized;
        //direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (dist <= attackRange)
        {
            if (nextAttack <= 0)
            {
                Fire();
            }
            else
            {
                nextAttack -= Time.deltaTime;
            }
        }

    }
    void Shoot(Transform spawnPoint)
    {
        GameObject fx = projectilePool.GetBullet();

        fx.transform.position = spawnPoint.position;
        fx.transform.rotation = spawnPoint.rotation;

        Rigidbody rb = fx.GetComponent<Rigidbody>();
        if (rb == null) rb = fx.AddComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(spawnPoint.forward * 2000f);
    }
    //void Shoot(Transform spawnPoint)
    //{
    //    GameObject fx = BulletPool.Instance.GetBullet();

    //    fx.transform.position = spawnPoint.position;
    //    fx.transform.rotation = spawnPoint.rotation;
    //    Rigidbody rb = fx.GetComponent<Rigidbody>(); 
    //    if (rb == null) rb = fx.AddComponent<Rigidbody>();

    //    rb.useGravity = false;
    //    rb.isKinematic = false;
    //    rb.AddForce(spawnPoint.forward * 1000f);
    //}
    void Fire()
    {
        if (isSingleBarrel)
        {
            Shoot(bulletSpawnposition0);
        }
        else if (isDoubleBarrel)
        {
            Shoot(bulletSpawnposition1);
            Shoot(bulletSpawnposition2);
        }

        nextAttack = cooldown;
    }

}
