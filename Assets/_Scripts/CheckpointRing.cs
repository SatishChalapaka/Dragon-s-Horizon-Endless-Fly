using UnityEngine;

public class CheckpointRing : MonoBehaviour
{
    public int ringIndex; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.instance.PlayerPassedRing(ringIndex);
            gameObject.SetActive(false); 
        }
    }
}
