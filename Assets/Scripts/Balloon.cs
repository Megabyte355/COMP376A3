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
    Anchor anchor;
    BalloonSplitter splitter;

    bool customInstantiation = false;
    void Start()
    {
        wind = GameObject.FindGameObjectWithTag(Tags.Wind).GetComponent<WindEffect>();
        splitter = GameObject.FindGameObjectWithTag(Tags.BalloonSplitter).GetComponent<BalloonSplitter>();
        
        if(!customInstantiation)
        {
            joint = GetComponent<SpringJoint>();
            joint.maxDistance = Random.Range(minRandomDistance, maxRandomDistance);
            anchor = joint.connectedBody.GetComponent<Anchor>();
            anchor.AddBalloon(this);
        }
    }

    public void CustomInstantiate(Anchor a, float minDistance, float maxDistance)
    {
        customInstantiation = true;
        anchor = a;
        joint = GetComponent<SpringJoint>();
        joint.minDistance = minDistance;
        joint.maxDistance = maxDistance;
        joint.connectedBody = a.rigidbody;
        a.AddBalloon(this);
    }

    void Update()
    {
        rigidbody.AddForce(wind.GetAddedAcceleration(), ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == Tags.Dart)
        {
            splitter.SplitBalloons(this, col.rigidbody.velocity);
            Destroy(col.gameObject);
        }
    }
}
