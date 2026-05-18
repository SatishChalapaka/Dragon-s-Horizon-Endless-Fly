using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayEffect : MonoBehaviour
{
    public Transform startPosition, endPosition;
    public float speed;
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x >= endPosition.position.x)
        {
            transform.position = new Vector3(startPosition.position.x, transform.position.y, transform.position.z);
        }
    }
}
