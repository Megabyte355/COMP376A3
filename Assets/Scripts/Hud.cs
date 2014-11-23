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

    Progression progress;

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag(Tags.Progress).GetComponent<Progression>();
    }

    void Update()
    {
        UpdateProgressUI(progress.GetProgressPercent());
        UpdateScoreUI(progress.GetScore());
    }

    void UpdateProgressUI(float percent)
    {
        float displayed = Mathf.Round(percent * 100);
        ProgressUI.text = "Progress: " + displayed + "%";
        ProgressUIShadow.text = "Progress: " + displayed + "%";
    }

    void UpdateScoreUI(int score)
    {
        ScoreUI.text = "Score: " + score;
        ScoreUIShadow.text = "Score: " + score;
    }
}
