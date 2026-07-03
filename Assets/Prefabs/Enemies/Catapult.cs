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
    [SerializeField, Range(1f, 180f)] private float attackAngle = 120f;
    [SerializeField] private bool stopAttackWhenBehindPlayer = true;

    private float nextAttack;
    private bool isAttacking;
    private Vector3 startForward;

    private void Start()
    {
        startForward = inverseRotation ? -transform.forward : transform.forward;

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

        bool canAttackPlayer = CanAttackPlayer();

        if (canAttackPlayer)
        {
            RotateTowardsPlayer();
        }

        float dist = Vector3.Distance(transform.position, player.position);

        if (canAttackPlayer && dist <= attackRange)
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

    private bool CanAttackPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        if (!isRotationX)
        {
            directionToPlayer.y = 0f;
        }

        if (directionToPlayer.sqrMagnitude <= 0.01f)
        {
            return false;
        }

        Vector3 forward = startForward;

        if (!isRotationX)
        {
            forward.y = 0f;
        }

        if (forward.sqrMagnitude <= 0.01f)
        {
            forward = inverseRotation ? -transform.forward : transform.forward;

            if (!isRotationX)
            {
                forward.y = 0f;
            }
        }

        if (Vector3.Angle(forward, directionToPlayer) > attackAngle * 0.5f)
        {
            return false;
        }

        if (stopAttackWhenBehindPlayer)
        {
            Vector3 playerToCatapult = transform.position - player.position;
            Vector3 playerForward = player.forward;

            if (!isRotationX)
            {
                playerToCatapult.y = 0f;
                playerForward.y = 0f;
            }

            if (playerForward.sqrMagnitude <= 0.01f)
            {
                return false;
            }

            if (Vector3.Dot(playerForward, playerToCatapult) < 0f)
            {
                return false;
            }
        }

        return true;
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
        if (!CanAttackPlayer())
        {
            return;
        }

        Fire();
    }

    private void Fire()
    {
        if (!CanAttackPlayer())
        {
            return;
        }

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
        rb.useGravity = false;
        float speed = DragonController.instance.forwardSpeed;

        float force = Mathf.Clamp(speed / 10f, 50f, 150f);

        rb.AddForce(spawnPoint.forward * force, ForceMode.Impulse);
    }
}
