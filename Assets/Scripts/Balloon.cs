using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    float maxRandomDistance;
    [SerializeField]
    float minRandomDistance;
    AudioSource balloonPopSound;

    WindEffect wind;
    SpringJoint joint;
    Anchor anchor;
    BalloonSplitter splitter;
    LineRenderer lineRenderer;

    bool customInstantiation = false;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        wind = GameObject.FindGameObjectWithTag(Tags.Wind).GetComponent<WindEffect>();
        splitter = GameObject.FindGameObjectWithTag(Tags.BalloonSplitter).GetComponent<BalloonSplitter>();
        
        if(!customInstantiation)
        {
            joint = GetComponent<SpringJoint>();
            joint.maxDistance = Random.Range(minRandomDistance, maxRandomDistance);
            anchor = joint.connectedBody.GetComponent<Anchor>();
            anchor.AddBalloon(this);
        }

        GameObject sounds = GameObject.FindGameObjectWithTag(Tags.Sounds) as GameObject;
        balloonPopSound = sounds.transform.FindChild("BalloonPop").GetComponent<AudioSource>();
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

        // Render line
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, joint.connectedBody.transform.position);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == Tags.Dart)
        {
            splitter.SplitBalloons(this, col.rigidbody.velocity);
            Destroy(col.gameObject);
            balloonPopSound.Play();
        }
        else if(col.gameObject.tag == Tags.Player)
        {
            splitter.SplitBalloons(this, col.rigidbody.velocity);
            col.gameObject.GetComponent<Player>().Kill();
            balloonPopSound.Play();
        }
    }
}
