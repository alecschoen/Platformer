using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField][Range(0f,1f)] private float lagAmount = 0f;

    private float previousCameraPosition;
    private Transform camera;
    private Vector3 targetPosition;

    private float ParallaxAmount => 1f - lagAmount;

    // Start is called before the first frame update
    private void Awake()
    {
        camera = Camera.main.transform;
        previousCameraPosition = camera.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 movement = CameraMovement;
        if (movement == Vector3.zero) return;
        targetPosition = new Vector3(transform.position.x + movement.x * ParallaxAmount, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }

    Vector3 CameraMovement
    {
        get
        {
            Vector3 movement = camera.position - previousCameraPosition;
            previousCameraPosition = camera.position;
            return movement;
        }
    }
}
