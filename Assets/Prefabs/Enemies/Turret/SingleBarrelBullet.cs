using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBarrelBullet : MonoBehaviour
{
    private void Start()
    {
        Invoke("Destroy", 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.GetComponent<DragonLivesController>())
        {
            DragonLivesController livesController = GameObject.FindGameObjectWithTag("Player").GetComponent<DragonLivesController>();
            if (livesController != null && livesController.TryTakeHit())
            {
                return;
            }
            Destroy(gameObject);
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
