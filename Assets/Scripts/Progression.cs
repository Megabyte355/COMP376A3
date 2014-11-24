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

    [SerializeField]
    int score;
    [SerializeField]
    float progress;
    [SerializeField]
    int popClusterReward;
    [SerializeField]
    int popBalloonReward;
    [SerializeField]
    int popHotAirBalloonReward;

    public int poppedBalloons = 0;
    public int totalBalloons;

    HotAirSpawner hotAirSpawner;
    void Start()
    {
        hotAirSpawner = GameObject.FindGameObjectWithTag(Tags.HotAirSpawner).GetComponent<HotAirSpawner>();
        totalBalloons = GameObject.FindGameObjectsWithTag(Tags.Balloon).Length;
        progress = 0f;
    }

    void Update()
    {
        progress = (float)poppedBalloons / totalBalloons;
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
            GameObject[] hotAirs = GameObject.FindGameObjectsWithTag(Tags.HotAirBalloon);
            foreach (GameObject h in hotAirs)
            {
                HotAirBalloon hotAirBalloon = h.GetComponent<HotAirBalloon>();
                hotAirBalloon.SpeedUp(speedUpAmount);
            }
        }
        if (!hotAirSpawn1 && progress >= hotAirBalloonSpawn1)
        {
            // TODO: spawn Hot Air Balloon
            hotAirSpawn1 = true;
            hotAirSpawner.Spawn();
        }
        if (!hotAirSpawn2 && progress >= hotAirBalloonSpawn2)
        {
            // TODO: spawn Hot Air Balloon
            hotAirSpawn2 = true;
            hotAirSpawner.Spawn();
        }
        if (!hotAirSpawn3 && progress >= hotAirBalloonSpawn3)
        {
            // TODO: spawn Hot Air Balloon
            hotAirSpawn3 = true;
            hotAirSpawner.Spawn();
        }

    }

    public float GetProgressPercent()
    {
        return progress;
    }

    public void HitCluster()
    {
        AddPoints(popClusterReward);
        poppedBalloons++;
    }

    public void HitBalloon()
    {
        AddPoints(popBalloonReward);
        poppedBalloons++;
    }

    public void HitWaterBalloon()
    {
        AddPoints(popBalloonReward);
    }

    public void HitHotAirBalloon()
    {
        AddPoints(popHotAirBalloonReward);
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }
}
