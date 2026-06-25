using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValcanoShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    [Header("Attack Settings")]
    public float attackRange = 8f;
    public float cooldown = 3f;
    public int damage = 10;

    [Header("References")]
    [SerializeField] private Transform player;
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
    private IEnumerator FireRoutine()
    {
        isAttacking = true;

        Instantiate(projectilePrefab, transform.position,  transform.rotation);

        yield return new WaitForSeconds(cooldown);

        nextAttack = 0f;
        isAttacking = false;
    }

}
