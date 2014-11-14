using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    float maxRandomDistance;
    [SerializeField]
    float minRandomDistance;

    WindEffect wind;
    SpringJoint joint;
    void Start()
    {
        wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindEffect>();
        joint = GetComponent<SpringJoint>();

        joint.maxDistance = Random.Range(minRandomDistance, maxRandomDistance);
    }

    void Update()
    {
        rigidbody.AddForce(wind.GetAddedAcceleration(), ForceMode.Acceleration);
    }
}
