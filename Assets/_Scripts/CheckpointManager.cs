using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    public Transform defaultCheckPoint;
    public string checkPointTag = "CheckPoint";
    public string checkPointNameContains = "checkpoint";
    public Vector3 respawnOffset = Vector3.zero;
    public bool useCheckPointRotation = true;

    private Transform currentCheckPoint;

    public static CheckPointManager GetOrCreate()
    {
        if (instance != null)
        {
            return instance;
        }

        GameObject managerObject = new GameObject("CheckPointManager");
        return managerObject.AddComponent<CheckPointManager>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentCheckPoint = defaultCheckPoint;
    }

    public void SetCheckPoint(Transform checkPoint)
    {
        if (checkPoint == null)
        {
            return;
        }

        currentCheckPoint = checkPoint;
    }

    public bool TryRespawn(Transform playerTransform, Rigidbody playerRigidbody)
    {
        Transform checkPoint = GetRespawnCheckPoint(playerTransform);
        if (checkPoint == null || playerTransform == null)
        {
            return false;
        }

        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }

        playerTransform.position = checkPoint.position + respawnOffset;

        if (useCheckPointRotation)
        {
            playerTransform.rotation = checkPoint.rotation;
        }

        return true;
    }

    public bool TryMoveToNearestCheckPoint(Transform playerTransform, Rigidbody playerRigidbody)
    {
        Transform checkPoint = FindNearestCheckPoint(playerTransform);
        if (checkPoint == null || playerTransform == null)
        {
            return false;
        }

        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }

        playerTransform.position = checkPoint.position + respawnOffset;

        if (useCheckPointRotation)
        {
            playerTransform.rotation = checkPoint.rotation;
        }

        return true;
    }

    private Transform GetRespawnCheckPoint(Transform playerTransform)
    {
        if (currentCheckPoint != null)
        {
            return currentCheckPoint;
        }

        if (defaultCheckPoint != null)
        {
            return defaultCheckPoint;
        }

        return FindClosestPreviousCheckPoint(playerTransform);
    }

    private Transform FindClosestPreviousCheckPoint(Transform playerTransform)
    {
        if (playerTransform == null)
        {
            return null;
        }

        Transform bestCheckPoint = null;
        float bestZ = float.NegativeInfinity;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < allObjects.Length; i++)
        {
            GameObject currentObject = allObjects[i];
            if (!IsCheckPoint(currentObject))
            {
                continue;
            }

            float checkPointZ = currentObject.transform.position.z;
            if (checkPointZ <= playerTransform.position.z && checkPointZ > bestZ)
            {
                bestZ = checkPointZ;
                bestCheckPoint = currentObject.transform;
            }
        }

        return bestCheckPoint;
    }

    private Transform FindNearestCheckPoint(Transform playerTransform)
    {
        if (playerTransform == null)
        {
            return null;
        }

        Transform bestCheckPoint = null;
        float bestDistance = float.PositiveInfinity;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < allObjects.Length; i++)
        {
            GameObject currentObject = allObjects[i];
            if (!IsCheckPoint(currentObject))
            {
                continue;
            }

            float distance = (currentObject.transform.position - playerTransform.position).sqrMagnitude;
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestCheckPoint = currentObject.transform;
            }
        }

        return bestCheckPoint;
    }

    private bool IsCheckPoint(GameObject currentObject)
    {
        if (currentObject == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(checkPointTag) && currentObject.tag == checkPointTag)
        {
            return true;
        }

        return !string.IsNullOrEmpty(checkPointNameContains)
            && currentObject.name.ToLower().Contains(checkPointNameContains.ToLower());
    }
}
