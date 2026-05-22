using UnityEngine;

public class TileReset : MonoBehaviour
{
    private void OnEnable()
    {
        Transform[] children =
            GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.CompareTag("Coin"))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}