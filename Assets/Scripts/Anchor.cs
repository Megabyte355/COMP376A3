using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Anchor : MonoBehaviour
{
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float speed;

    [SerializeField]
    float minDistance;
    [SerializeField]
    float maxDistance;
    [SerializeField]
    bool randomizeDirection;
    [SerializeField]
    GameObject balloonPrefab;
    [SerializeField]
    List<Balloon> anchoredBalloons = new List<Balloon>();

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

    public void SplitBalloons(Vector3 collisionDirection)
    {
        // TODO: Split balloons
        // TODO: Speed up
        // TODO: Award player points
    }

    public void Wrap(Vector3 translationVector, Space space)
    {
        foreach(Balloon b in anchoredBalloons)
        {
            b.transform.Translate(translationVector, space);
        }
        transform.Translate(translationVector, space);
    }
}
