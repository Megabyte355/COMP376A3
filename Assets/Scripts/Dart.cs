using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    void Update()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }
}
