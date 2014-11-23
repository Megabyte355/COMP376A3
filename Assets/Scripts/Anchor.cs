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
            direction = new Vector3(Random.value, Random.value, Random.value).normalized;
        }
    }

    void Update()
    {
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
            GameObject balloonObject = Instantiate(balloonPrefab, position, Quaternion.identity) as GameObject;
            Balloon b = balloonObject.GetComponent<Balloon>();
            b.CustomInstantiate(this, minDistance, Random.Range(minDistance, maxDistance));
        }
    }

    public void SetRandomColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1f);
        foreach (Balloon b in anchoredBalloons)
        {
            b.transform.FindChild("Model").renderer.material.color = newColor;
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

    public void SpeedUp(float amount)
    {
        speed += amount;
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
