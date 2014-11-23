using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    Vector3 direction;

    
    public void SetColor(Color c)
    {
        transform.FindChild("Sphere").renderer.material.color = c;
    }

    public void SetDirection(Vector3 d)
    {
        direction = d;
        UpdateVelocity();
    }

    public void SetSpeed(float spd)
    {
        speed = spd;
        UpdateVelocity();
    }

    public void SpeedUp(float amount)
    {
        speed += amount;
        UpdateVelocity();
    }

    void UpdateVelocity()
    {
        rigidbody.velocity = direction * speed;
    }
}
