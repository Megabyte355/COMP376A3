using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Anchor : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float minDistance;
    public float maxDistance;
    [SerializeField]
    float speedIncrement;
    [SerializeField]
    bool randomizeDirection;
    [SerializeField]
    List<Balloon> anchoredBalloons = new List<Balloon>();

    [SerializeField]
    GameObject balloonPrefab;

    WindEffect wind;

    void Start()
    {
        wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindEffect>();

        if(randomizeDirection)
        {
            direction = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f)).normalized;
        }
    }

    void Update()
    {
        //rigidbody.velocity = wind.GetAddedVelocity();
        //rigidbody.AddForce(wind.GetAddedAcceleration(), ForceMode.Acceleration);
        //transform.position = wind.GetPositionOffset() * Time.deltaTime;

        // "Cheat" for simulating wind for kinematic rigidbodies
        transform.position = Vector3.Lerp(transform.position, transform.position + wind.GetPositionOffset(), Time.deltaTime);
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    public void InitializeBalloons(int numBalloons)
    {
        for(int i = 0; i < numBalloons; i++)
        {
            float offsetX = Random.Range(-maxDistance, maxDistance);
            float offsetY = Random.Range(minDistance, maxDistance);
            float offsetZ = Random.Range(-maxDistance, maxDistance);

            Vector3 position = transform.position + new Vector3(offsetX, offsetY, offsetZ);
            //Balloon b = Instantiate(balloonPrefab, position, Quaternion.identity) as Balloon;
            //b.CustomInstantiate(this, minDistance, maxDistance);

            GameObject balloonObject = Instantiate(balloonPrefab, position, Quaternion.identity) as GameObject;
            Balloon b = balloonObject.GetComponent<Balloon>();
            b.CustomInstantiate(this, minDistance, Random.Range(minDistance, maxDistance));
        }
    }

    public void AddBalloon(Balloon b)
    {
        anchoredBalloons.Add(b);
    }

    public void RemoveBalloon(Balloon b)
    {
        anchoredBalloons.Remove(b);
    }

    public void ClearBalloonList()
    {
        anchoredBalloons.Clear();
    }

    public int GetBalloonCount()
    {
        return anchoredBalloons.Count;
    }

    public List<Balloon> GetAnchoredBalloons()
    {
        return anchoredBalloons;
    }

    public void SpeedUp()
    {
        speed += speedIncrement;
    }
    //public void SplitBalloons(Balloon targetBalloon, Vector3 collisionDirection)
    //{

    //    if (anchoredBalloons.Count == 0)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
        
    //    // Destroy target balloon
    //    anchoredBalloons.Remove(targetBalloon);
    //    Destroy(targetBalloon.gameObject);

    //    // Split balloons
    //    GameObject freshAnchorObject = Instantiate(gameObject, transform.position, Quaternion.identity) as GameObject;
    //    Anchor freshAnchor = freshAnchorObject.GetComponent<Anchor>();
    //    freshAnchor.ClearBalloonList();

    //    // Transfer half of the balloons
    //    List<Balloon> transferList = new List<Balloon>(anchoredBalloons);
    //    int half = anchoredBalloons.Count / 2;
    //    for (int i = 0; i < half; i++)
    //    {
    //        Balloon current = transferList[i];
    //        anchoredBalloons.Remove(current);
    //        freshAnchor.AddBalloon(current);
    //        current.GetComponent<SpringJoint>().connectedBody = freshAnchor.rigidbody;
    //    }
    //    transferList.Clear();

    //    freshAnchor.direction = new Vector3(0, -1, 0);


    //    if(freshAnchor.GetBalloonCount() == 0)
    //    {
    //        Destroy(freshAnchor.gameObject);
    //    }


    //    // TODO: Speed up
    //    // TODO: Award player points
    //}

    public void Wrap(Vector3 translationVector, Space space)
    {
        foreach(Balloon b in anchoredBalloons)
        {
            b.transform.Translate(translationVector, space);
        }
        transform.Translate(translationVector, space);
    }
}
