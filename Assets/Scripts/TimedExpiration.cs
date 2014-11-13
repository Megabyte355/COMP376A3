using UnityEngine;
using System.Collections;

public class TimedExpiration : MonoBehaviour
{
    [SerializeField]
    float lifeSpan;
    float timeLeft;

    void Start()
    {
        timeLeft = lifeSpan;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0f)
        {
            Destroy(gameObject);
        }
    }
}
