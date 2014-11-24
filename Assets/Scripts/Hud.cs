using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour
{
    [SerializeField]
    TextMesh ProgressUI;
    [SerializeField]
    TextMesh ProgressUIShadow;
    [SerializeField]
    TextMesh ScoreUI;
    [SerializeField]
    TextMesh ScoreUIShadow;
    [SerializeField]
    TextMesh LivesUI;
    [SerializeField]
    TextMesh LivesUIShadow;

    [SerializeField]
    GameObject GameOverLabel;
    [SerializeField]
    GameObject VictoryLabel;

    Progression progress;
    Player player;

    // Cached values
    int cacheProgress = -1;
    int cacheLives = -1;
    int cacheScore = -1;

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag(Tags.Progress).GetComponent<Progression>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();

        // Display cached values
        UpdateProgressUI(progress.GetProgressPercent());
        UpdateScoreUI(progress.GetScore());
        UpdateLivesUI(player.GetLives());
    }

    void Update()
    {
        UpdateProgressUI(progress.GetProgressPercent());
        UpdateScoreUI(progress.GetScore());
        UpdateLivesUI(player.GetLives());
    }

    void UpdateProgressUI(float percent)
    {
        int displayed = (int)Mathf.Round(percent * 100);
        if (displayed != cacheProgress)
        {
            cacheProgress = displayed;
            ProgressUI.text = "Progress: " + cacheProgress + "%";
            ProgressUIShadow.text = "Progress: " + cacheProgress + "%";
        }

        // Prevent Victory and Game Over to show up at same time
        if (progress.IsFinished() && !GameOverLabel.activeSelf)
        {
            VictoryLabel.SetActive(true);
        }
    }

    void UpdateScoreUI(int score)
    {
        if (cacheScore != score)
        {
            cacheScore = score;
            ScoreUI.text = "Score: " + cacheScore;
            ScoreUIShadow.text = "Score: " + cacheScore;
        }
    }

    void UpdateLivesUI(int lives)
    {
        if (cacheLives != lives && lives >= 0)
        {
            cacheLives = lives;
            LivesUI.text = "Lives: " + cacheLives;
            LivesUIShadow.text = "Lives: " + cacheLives;
        }
        if (lives < 0 && !GameOverLabel.activeSelf)
        {
            GameOverLabel.SetActive(true);
            LivesUI.gameObject.SetActive(false);
        }
    }
}
