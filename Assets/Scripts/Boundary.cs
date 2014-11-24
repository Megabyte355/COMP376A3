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
        transform.FindChild("UpLabel").transform.position = new Vector3(0, boxCollider.size.y / 2, 0);
        transform.FindChild("DownLabel").transform.position = new Vector3(0, -boxCollider.size.y / 2, 0);
        transform.FindChild("LeftLabel").transform.position = new Vector3(-boxCollider.size.x / 2, 0, 0);
        transform.FindChild("RightLabel").transform.position = new Vector3(boxCollider.size.x / 2, 0, 0);
        transform.FindChild("FrontLabel").transform.position = new Vector3(0, 0, boxCollider.size.z / 2);
        transform.FindChild("BackLabel").transform.position = new Vector3(0, 0, -boxCollider.size.z / 2);
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
        Vector3 wrapPosition = obj.transform.position + translationVector;
        if(obj.tag == Tags.Anchor)
        {
            obj.GetComponent<Anchor>().Wrap(translationVector, Space.World);
        }
        else if (obj.tag == Tags.Dart)
        {
            Dart d = obj.gameObject.GetComponent<Dart>();
            d.DelayedWrap(wrapPosition);
        }
        else
        {
            obj.transform.Translate(translationVector, Space.World);
        }
    }
}
