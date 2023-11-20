using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [SerializeField] float leftLimit;
    [SerializeField] float bottomLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float topLimit;

    [SerializeField] float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, leftLimit, rightLimit);
        targetPosition.y = Mathf.Clamp(targetPosition.y, bottomLimit, topLimit);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
