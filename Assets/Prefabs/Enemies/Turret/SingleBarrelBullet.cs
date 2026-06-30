using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBarrelBullet : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), 10f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DragonLivesController>())
        {
            DragonLivesController livesController =
                other.GetComponent<DragonLivesController>();

            if (livesController != null)
            {
                livesController.TryTakeHit();
            }

            ProjectilePool.Instance.ReturnBullet(gameObject);

            if (livesController != null && livesController.TryTakeHit())
            {
                return;
            }

           DragonController.instance.GameFailed();
        }
    }
    private void ReturnToPool()
    {
        ProjectilePool.Instance.ReturnBullet(gameObject);
    }
}
