using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public float maxRangeMouseY = 75f;
    public float turnSpeed = 2f;
    public float moveSpeed = 4f;

    
    float rotationX = 0;
    float rotationY = 0f;

    void Awake()
    {
        //head = transform.Find("Head").transform;
    }

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
        //rotationX = Mathf.Clamp(rotationX, -maxRangeMouseY, maxRangeMouseY);

        // TODO Add code to update and use rotationX and rotationY
        rotationX += mouseX * turnSpeed;
        rotationY += mouseY * turnSpeed;
        rotationY = Mathf.Clamp(rotationY, -maxRangeMouseY, maxRangeMouseY);

        transform.rotation = Quaternion.Euler(-rotationY, rotationX, 0);
    }

    void UpdateMovement()
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");
        Vector3 translation = new Vector3(axisX, 0, axisZ);
        transform.Translate(translation * moveSpeed * Time.deltaTime);
    }
}
