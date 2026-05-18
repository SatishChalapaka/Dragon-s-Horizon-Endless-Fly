using DragonGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody rb;
    public Vector3 direction;
    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
        Invoke("Disable", 2f);
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * projectileSpeed;
    }
    
    void Disable()
    {
        //GameManager.instance.bulletDestroyedParticle.transform.SetParent(gameObject.transform);
        GameManager.instance.bulletDestroyedParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameManager.instance.bulletDestroyedParticle.Play();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
