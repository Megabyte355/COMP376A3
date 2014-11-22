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
        if(thingsThatWrap.Contains(obj.tag))
        {
            Wrap(obj.gameObject);
        }
    }

    void Wrap(GameObject obj)
    {
        Vector3 translationVector = 2 * (boxCollider.center - obj.transform.position);

        if(obj.tag == Tags.Anchor)
        {
            obj.GetComponent<Anchor>().Wrap(translationVector, Space.World);
        }
        else
        {
            obj.transform.Translate(translationVector, Space.World);
        }
    }
}
