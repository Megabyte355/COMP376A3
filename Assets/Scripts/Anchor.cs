using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour
{
    WindEffect wind;
    void Start()
    {
        wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindEffect>();
    }

    void Update()
    {
        //rigidbody.velocity = wind.GetAddedVelocity();
        rigidbody.AddForce(wind.GetAddedAcceleration(), ForceMode.Acceleration);
    }
}
