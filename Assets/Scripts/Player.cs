using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    MouseLook mouse;
    DartGun dartGun;
    BoxCollider boxCollider;

    [SerializeField]
    float invincibleTime;
    float invincibleTimer;
    bool invincible;

    [SerializeField]
    float respawnTime;
    float respawnTimer;
    bool respawning;

    [SerializeField]
    Vector3 respawnPosition;
    [SerializeField]
    int lives;

    void Start()
    {
        lives--;
        mouse = GetComponent<MouseLook>();
        dartGun = GetComponent<DartGun>();
        boxCollider = GetComponent<BoxCollider>();

        invincible = true;
        invincibleTimer = invincibleTime;
    }

    public void Kill()
    {
        if(!invincible)
        {
            lives--;
            mouse.enabled = false;
            dartGun.enabled = false;
            boxCollider.enabled = false;

            rigidbody.isKinematic = false;
            rigidbody.angularVelocity = new Vector3(Random.value, Random.value, Random.value).normalized;

            respawning = true;
            respawnTimer = respawnTime;
        }
    }

    public void Revive()
    {
        if(lives >= 0)
        {
            mouse.enabled = true;
            dartGun.enabled = true;
            boxCollider.enabled = true;
            invincible = true;
            invincibleTimer = invincibleTime;

            transform.position = respawnPosition;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
        }
        else
        {
            // TODO: Show Game Over
        }
    }

    void Update()
    {
        if(invincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0)
            {
                invincible = false;
            }
        }

        if (respawning)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0f)
            {
                respawning = false;
                Revive();
            }
        }
    }

    public int GetLives()
    {
        return lives;
    }
}
