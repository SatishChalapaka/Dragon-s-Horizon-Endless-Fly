using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 8f;
    public float cooldown = 3f;
    public int damage = 10;

    [Header("References")]
    [SerializeField] private Transform player;
    public Animator animator;
    public ProjectilePool projectilePool;

    [Header("Spawn Points")]
    public Transform bulletSpawnposition0;
    public Transform bulletSpawnposition1;
    public Transform bulletSpawnposition2;

    [Header("Barrel Type")]
    public bool isSingleBarrel = true;
    public bool isDoubleBarrel = false;

    [Header("Rotation")]
    public bool isRotationX;
    public bool inverseRotation;

    private float nextAttack;
    private bool isAttacking;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        if (player == null) return;

        RotateTowardsPlayer();

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            if (nextAttack <= 0f && !isAttacking)
            {
                StartCoroutine(FireRoutine());
            }
            else
            {
                nextAttack -= Time.deltaTime;
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction;

        if (inverseRotation)
            direction = transform.position - player.position;
        else
            direction = player.position - transform.position;

        if (isRotationX)
        {
            direction.Normalize();

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation,
                    Time.deltaTime * 5f
                );
            }
        }
        else
        {
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    Time.deltaTime * 5f
                );
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        isAttacking = true;

        if (animator != null)
        {
            animator.SetTrigger("Fire");
        }
        else
        {
            Fire();
        }

        yield return new WaitForSeconds(cooldown);

        nextAttack = 0f;
        isAttacking = false;
    }

    // Animation Event
    public void AnimationShoot()
    {
        Fire();
    }

    private void Fire()
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
    }

    private void Shoot(Transform spawnPoint)
    {
        if (spawnPoint == null || projectilePool == null)
            return;

        GameObject projectile = projectilePool.GetBullet();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = spawnPoint.rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb == null)
            rb = projectile.AddComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(spawnPoint.forward * 2000f, ForceMode.Force);
    }
}