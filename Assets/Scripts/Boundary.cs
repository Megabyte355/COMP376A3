using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boundary : MonoBehaviour
{
    [SerializeField]
    List<string> thingsThatWrap;
    BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerExit(Collider obj)
    {
        if(thingsThatWrap.Contains(obj.name))
        {
            Wrap(obj.gameObject);
        }
    }

    void Wrap(GameObject obj)
    {
        Vector3 translationVector = boxCollider.center - obj.transform.position;
        obj.transform.Translate(2 * translationVector, Space.World);
    }
}
