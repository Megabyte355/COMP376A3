using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public float maxRangeMouseY = 75f;
    public float turnSpeed = 2f;
    public float moveSpeed = 4f;

    float rotationX = 0;
    float rotationY = 0f;

    void Update()
    {
        Screen.lockCursor = true;
        UpdateRotation();
        UpdateMovement();
    }

    void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX += mouseX * turnSpeed;
        rotationY += mouseY * turnSpeed;
        rotationY = Mathf.Clamp(rotationY, -maxRangeMouseY, maxRangeMouseY);

        transform.rotation = Quaternion.Euler(-rotationY, rotationX, 0);
    }

    void UpdateMovement()
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");
        float axisY = Input.GetAxis("Lateral");

        Vector3 translation = new Vector3(axisX, axisY, axisZ);
        transform.Translate(translation * moveSpeed * Time.deltaTime);
    }
}
