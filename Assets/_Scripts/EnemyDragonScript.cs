using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragonScript : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    public float speed;
    [HideInInspector] public Rigidbody rigidbody;
    public bool isRight, isLeft;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isRight)
        {
            rigidbody.velocity = new Vector3(speed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            if (rigidbody.position.x >= maxX)
            {
                isLeft = true;
                isRight = false;
            }
        }
        if (isLeft)
        {
            rigidbody.velocity = new Vector3(-speed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            if (rigidbody.position.x <= minX)
            {
                isLeft = false;
                isRight = true;
            }
        }
    }
}
