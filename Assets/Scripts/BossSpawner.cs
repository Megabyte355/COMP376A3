using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{

    [SerializeField]
    int numberOfBalloons;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject anchorPrefab;
    [SerializeField]
    Boundary boundary;
    [SerializeField]
    AudioSource thunderSound;

    [SerializeField]
    float timeBeforeStaticStorm;
    float timer;
    Player player;

    Hud hud;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        hud = GameObject.FindGameObjectWithTag(Tags.Hud).GetComponent<Hud>();
        timer = timeBeforeStaticStorm;
    }
    // Use Awake instead of Start to allow Progress script to count balloons
    void Awake()
    {
        BoxCollider boxCollider = boundary.GetComponent<BoxCollider>();

        // Generate random position inside the Boundary Box
        float posX = Random.Range(-boxCollider.size.x / 2 * 0.85f, boxCollider.size.x / 2 * 0.85f);
        float posY = Random.Range(-boxCollider.size.y / 2 * 0.85f, boxCollider.size.y / 2 * 0.85f);
        float posZ = Random.Range(-boxCollider.size.z / 2 * 0.85f, boxCollider.size.z / 2 * 0.85f);
        Vector3 position = boundary.transform.position + new Vector3(posX, posY, posZ);

        // Instantiate anchor and balloons
        GameObject freshAnchorObject = Instantiate(anchorPrefab, position, Quaternion.identity) as GameObject;
        Anchor freshAnchor = freshAnchorObject.GetComponent<Anchor>();
        freshAnchor.InitializeBalloons(numberOfBalloons);
        freshAnchor.SetSpeed(speed);
        freshAnchor.SetRandomColor();
    }

    void Update()
    {
        if(player.IsAlive())
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                hud.CastStaticStormEffects();
                thunderSound.Play();
                GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().Kill();
                timer = timeBeforeStaticStorm;
            }
        }
    }

    public float GetTimer()
    {
        return timer;
    }
}
