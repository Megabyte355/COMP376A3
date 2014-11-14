using UnityEngine;
using System.Collections;

public class WindEffect : MonoBehaviour
{
    [SerializeField]
    bool randomizeDirection;
    [SerializeField]
    Vector3 windDirection;
    [SerializeField]
    float amplitude;
    [SerializeField]
    float period;
    float time;
    Vector3 addedAcceleration;


    void Awake()
    {
        if(randomizeDirection)
        {
            windDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)).normalized;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        float acc = amplitude * Mathf.Sin(2 * Mathf.PI * (1/period) * time);
        addedAcceleration = windDirection.normalized * acc;
        if(time >= period)
        {
            time -= period;
        }
    }

    public Vector3 GetAddedAcceleration()
    {
        return addedAcceleration;
    }
}
