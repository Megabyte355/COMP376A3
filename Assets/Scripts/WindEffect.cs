using UnityEngine;
using System.Collections;

public class WindEffect : MonoBehaviour
{
    // For kinematic rigidbodies
    [SerializeField]
    float positionOffsetAmp;
    Vector3 positionOffset;

    // For non-kinematic rigidbodies
    [SerializeField]
    bool randomizeDirection;
    [SerializeField]
    Vector3 windDirection;
    [SerializeField]
    float amplitude;
    [SerializeField]
    float center;
    [SerializeField]
    float period;
    float time;
    Vector3 addedAcceleration;




    void Start()
    {
        if (randomizeDirection)
        {
            windDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)).normalized;
        }
        positionOffset = new Vector3(0, 0, 0);
    }

    void Update()
    {
        time += Time.deltaTime;
        float sin = Mathf.Sin(2 * Mathf.PI * (1 / period) * time) + center;
        float acc = amplitude * sin;
        addedAcceleration = windDirection.normalized * acc;

        float offset = positionOffsetAmp * sin;
        positionOffset = windDirection.normalized * offset;

        if (time >= period)
        {
            time -= period;
        }
    }

    // For non-kinematic rigidBodies
    public Vector3 GetAddedAcceleration()
    {
        return addedAcceleration;
    }

    // For kinematic rigidbodies
    public Vector3 GetPositionOffset()
    {
        return positionOffset;
    }
}
