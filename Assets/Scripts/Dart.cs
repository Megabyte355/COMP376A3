using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float wrapDelay;
    float timer;
    bool wrapping = false;

    Vector3 wrapPosition;

    void Start()
    {
        rigidbody.velocity = transform.forward * moveSpeed;
    }

    void Update()
    {
        if(wrapping)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                wrapping = false;
                transform.position = wrapPosition;
            }
        }
    }

    public void DelayedWrap(Vector3 wrappedPosition)
    {
        wrapPosition = wrappedPosition;
        wrapping = true;
        timer = wrapDelay;
    }
}
