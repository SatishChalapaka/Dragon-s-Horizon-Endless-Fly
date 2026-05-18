using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform playerTransform;
    public float distance;
    // Start is called before the first frame update
    private void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {

        //if (DragonController.instance.isMove)
        //{
        //    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //    distance = Vector3.Distance(transform.position, playerTransform.transform.position);
        //    if (distance <= 2)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 20f * Time.deltaTime);
        //    }
        //    else if (distance <= 3)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 30f * Time.deltaTime);
        //    }
        //    else if (distance <= 4)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 50f * Time.deltaTime);
        //    }
        //}
        if (DragonController.instance.isMove)
        {
            distance = Vector3.Distance(transform.position, playerTransform.transform.position);
            if (distance <= 1.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position,DragonController.instance.forwardSpeed * 0.04f * Time.deltaTime);
            }
            //else if (distance <= 3)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 30f * Time.deltaTime);
            //}
            //else if (distance <= 4)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 50f * Time.deltaTime);
            //}
        }

    }
}
