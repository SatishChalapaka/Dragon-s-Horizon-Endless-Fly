using DG.Tweening;
using UnityEngine;

public class DragonIdleRotate : MonoBehaviour
{
[Header("Settings")]
    public float rotationSpeed = 0.3f;
    public float resetDelay = 2f;
    public float resetSpeed = 3f;

    private Quaternion originalRotation;
    private Vector2 lastPosition;
    private bool dragging;
    private float timer;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        // Mouse
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            timer = 0;
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastPosition;

            transform.Rotate(0, -delta.x * rotationSpeed, 0, Space.World);

            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

#else

        // Touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragging = true;
                timer = 0;
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - lastPosition;

                transform.Rotate(0, -delta.x * rotationSpeed, 0, Space.World);

                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                dragging = false;
            }
        }

#endif

        // Reset after delay
        if (!dragging)
        {
            timer += Time.deltaTime;

            if (timer >= resetDelay)
            {
                transform.localRotation = Quaternion.Slerp(
                    transform.localRotation,
                    originalRotation,
                    Time.deltaTime * resetSpeed);
            }
        }
    }
}