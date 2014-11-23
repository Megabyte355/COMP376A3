using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    [SerializeField]
    int score;
    [SerializeField]
    int popClusterReward;
    [SerializeField]
    int popBalloonReward;
    [SerializeField]
    int popHotAirBalloonReward;

    public void hitCluster()
    {
        AddPoints(popClusterReward);
    }

    public void hitBalloon()
    {
        AddPoints(popBalloonReward);
    }

    public void hitHotAirBalloon()
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
