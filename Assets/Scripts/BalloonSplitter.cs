using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonSplitter : MonoBehaviour
{
    [SerializeField]
    GameObject anchorPrefab;
    [SerializeField]
    float maxDegreeChange;

    public void SplitBalloons(Balloon targetBalloon, Vector3 collisionDirection)
    {
        Anchor anchor = targetBalloon.GetComponent<SpringJoint>().connectedBody.GetComponent<Anchor>();

        // Destroy target balloon
        anchor.RemoveBalloon(targetBalloon);
        Destroy(targetBalloon.gameObject);
        if(anchor.GetBalloonCount() == 0)
        {
            // Destroy last balloon
            Destroy(anchor.gameObject);
        }
        else if(anchor.GetBalloonCount() == 1)
        {
            // Simply change direction of anchor
            anchor.direction = GetRandomRotation(maxDegreeChange) * collisionDirection;

            // Speed up
            anchor.SpeedUp();

            // Disable line renderer - It looks weird!
            anchor.GetAnchoredBalloons()[0].GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            // Instantiate new anchor
            GameObject freshAnchorObject = Instantiate(anchorPrefab, anchor.gameObject.transform.position, Quaternion.identity) as GameObject;
            Anchor freshAnchor = freshAnchorObject.GetComponent<Anchor>();
            freshAnchor.speed = anchor.speed;
            freshAnchor.minDistance = anchor.minDistance;
            freshAnchor.maxDistance = anchor.maxDistance;

            // Transfer half of the balloons
            List<Balloon> anchoredBalloons = anchor.GetAnchoredBalloons();
            int half = anchoredBalloons.Count / 2;
            Color newColor = new Color(Random.value, Random.value, Random.value, 1f);
            for (int i = anchoredBalloons.Count - 1; i >= half; i--)
            {
                Balloon current = anchoredBalloons[i];
                current.transform.FindChild("Model").renderer.material.color = newColor;
                anchor.RemoveBalloon(current);
                freshAnchor.AddBalloon(current);
                current.GetComponent<SpringJoint>().connectedBody = freshAnchor.rigidbody;
            }

            // Change direction
            anchor.direction = GetRandomRotation(maxDegreeChange) * collisionDirection;
            freshAnchor.direction = GetRandomRotation(maxDegreeChange) * collisionDirection;

            // Speed up
            anchor.SpeedUp();
            freshAnchor.SpeedUp();

            // Disable line renderer - It looks weird!
            if(anchor.GetBalloonCount() <= 1)
            {
                anchor.GetAnchoredBalloons()[0].GetComponent<LineRenderer>().enabled = false;
            }
            if(freshAnchor.GetBalloonCount() <= 1)
            {
                freshAnchor.GetAnchoredBalloons()[0].GetComponent<LineRenderer>().enabled = false;
            }
        }

        // TODO: Award player points
    }

    Quaternion GetRandomRotation(float maxDegree)
    {
        Vector3 rotationAxis = new Vector3(Random.value, Random.value, Random.value).normalized;
        float degree = Random.Range(0f, maxDegree);
        return Quaternion.AngleAxis(degree, rotationAxis);
    }
}
