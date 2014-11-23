using UnityEngine;
using System.Collections;

public class Progression : MonoBehaviour
{
    [SerializeField]
    float speedUpThreshold;
    [SerializeField]
    float speedUpAmount;
    bool speedUp = false;

    [SerializeField]
    float hotAirBalloonSpawn1;
    bool hotAirSpawn1 = false;
    [SerializeField]
    float hotAirBalloonSpawn2;
    bool hotAirSpawn2 = false;
    [SerializeField]
    float hotAirBalloonSpawn3;
    bool hotAirSpawn3 = false;

    int poppedBalloons = 0;
    int totalBalloons;
    
    void Start()
    {
        totalBalloons = GameObject.FindGameObjectsWithTag(Tags.Balloon).Length;
    }

    void Update()
    {
        float progress = GetProgressPercent();
        if (!speedUp && progress >= speedUpThreshold)
        {
            speedUp = true;
            GameObject[] anchors = GameObject.FindGameObjectsWithTag(Tags.Anchor);
            foreach(GameObject a in anchors)
            {
                Anchor anchor = a.GetComponent<Anchor>();
                anchor.SpeedUp(speedUpAmount);
            }

            // TODO: Increase speed of Hot Air Balloon
        }
        if (!hotAirSpawn1 && progress >= hotAirBalloonSpawn1)
        {
            // TODO: spawn Hot Air Balloon
        }
        if (!hotAirSpawn2 && progress >= hotAirBalloonSpawn2)
        {
            // TODO: spawn Hot Air Balloon
        }
        if (!hotAirSpawn3 && progress >= hotAirBalloonSpawn3)
        {
            // TODO: spawn Hot Air Balloon
        }
    }
    public float GetProgressPercent()
    {
        return poppedBalloons / (float)totalBalloons;
    }
}
