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
    TextMesh StaticStormUI;
    [SerializeField]
    TextMesh StaticStormUIShadow;

    [SerializeField]
    GameObject GameOverLabel;
    [SerializeField]
    GameObject VictoryLabel;
    [SerializeField]
    GameObject BossLabel;
    

    [SerializeField]
    GameObject WhiteScreen;
    bool flash = false;
    float flashDuration = 0.5f;
    float flashTimer;

    Progression progress;
    Player player;
    [SerializeField]
    BossSpawner bossSpawner;

    // Cached values
    int cacheProgress = -1;
    int cacheLives = -1;
    int cacheScore = -1;
    int cachedStaticStorm = -1;

    bool bossMessageShown = false;
    float bossMessageDuration = 3f;
    float bossMessageTimer;

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
        
        if (StaticStormUI.gameObject.activeSelf)
        {
            UpdateStaticStormUI();
        }
        
        if(bossMessageShown)
        {
            bossMessageTimer -= Time.deltaTime;
            if(bossMessageTimer <= 0)
            {
                bossMessageShown = false;
                BossLabel.SetActive(false);
            }
        }

        if(flash)
        {
            flashTimer += Time.deltaTime;

            if(flashTimer < flashDuration / 4f)
            {
                WhiteScreen.SetActive(true);
            }
            else if (flashTimer < flashDuration / 2f)
            {
                WhiteScreen.SetActive(false);
            }
            else if (flashTimer < flashDuration * (3f / 4f))
            {
                WhiteScreen.SetActive(true);
            }
            else
            {
                WhiteScreen.SetActive(false);
                flash = false;
            }
        }

        // Turn static storm counter on/off depending on situation (a little hacked)
        if(player.IsAlive() && progress.IsBossSpawned() && !StaticStormCountdownIsOn())
        {
            ShowStaticStormCountdown(true);
        }
        else if (!player.IsAlive() && progress.IsBossSpawned() && StaticStormCountdownIsOn())
        {
            ShowStaticStormCountdown(false);
        }
        if(StaticStormCountdownIsOn() && progress.IsFinished())
        {
            ShowStaticStormCountdown(false);
        }
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

    void UpdateStaticStormUI()
    {
        int timeLeft = (int)bossSpawner.GetTimer();
        if(cachedStaticStorm != timeLeft)
        {
            cachedStaticStorm = timeLeft;
            StaticStormUI.text = "Static Storm in... " + timeLeft;
            StaticStormUIShadow.text = "Static Storm in... " + timeLeft;
        }
        
    }

    public void ShowBossMessage()
    {
        bossMessageShown = true;
        BossLabel.SetActive(true);
        bossMessageTimer = bossMessageDuration;
    }

    public void CastStaticStormEffects()
    {
        flash = true;
        flashTimer = 0;
    }

    public void ShowStaticStormCountdown(bool status)
    {
        StaticStormUI.gameObject.SetActive(status);
        StaticStormUIShadow.gameObject.SetActive(status);
    }

    bool StaticStormCountdownIsOn()
    {
        return StaticStormUI.gameObject.activeSelf && StaticStormUIShadow.gameObject.activeSelf;
    }
}
