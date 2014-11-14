using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour
{
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float speed;
    [SerializeField]
    bool randomizeDirection;

    WindEffect wind;
    void Start()
    {
        wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindEffect>();

        if(randomizeDirection)
        {
            direction = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)).normalized;
        }
    }

    void Update()
    {
        //rigidbody.velocity = wind.GetAddedVelocity();
        //rigidbody.AddForce(wind.GetAddedAcceleration(), ForceMode.Acceleration);
        //transform.position = wind.GetPositionOffset() * Time.deltaTime;

        // "Cheat" for simulating wind for kinematic rigidbodies
        transform.position = Vector3.Lerp(transform.position, transform.position + wind.GetPositionOffset(), Time.deltaTime);
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

}
