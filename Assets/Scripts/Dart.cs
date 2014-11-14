using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    void Start()
    {
        rigidbody.velocity = transform.forward * moveSpeed;
    }
}
