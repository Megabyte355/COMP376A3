﻿using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float throwCooldown;
    float timer;

    [SerializeField]
    GameObject waterBalloonPrefab;

    [SerializeField]
    float maxDegreeDeviation;

    Progression progress;
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>();
        progress = GameObject.FindGameObjectWithTag(Tags.Progress).GetComponent<Progression>();
        timer = throwCooldown;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            // Throw water balloon at player
            Vector3 hotAirToPlayer = (playerTransform.position - transform.position).normalized;

            Vector3 rotationAxis = new Vector3(Random.value, Random.value, Random.value).normalized;
            float degree = Random.Range(0f, maxDegreeDeviation);
            Vector3 shootDirection = Quaternion.AngleAxis(degree, rotationAxis) * hotAirToPlayer;

            GameObject waterBalloonObject = Instantiate(waterBalloonPrefab, transform.position, Quaternion.identity) as GameObject;
            // TODO: Decide if we should keep this like this
            //WaterBalloon wb = waterBalloonObject.GetComponent<WaterBalloon>();
            waterBalloonObject.rigidbody.AddForce(shootDirection * 500f);
            timer = throwCooldown;
        }
    }

    public void SetColor(Color c)
    {
        transform.FindChild("Sphere").renderer.material.color = c;
    }

    public void SetDirection(Vector3 d)
    {
        direction = d;
        UpdateVelocity();
    }

    public void SetSpeed(float spd)
    {
        speed = spd;
        UpdateVelocity();
    }

    public void SpeedUp(float amount)
    {
        speed += amount;
        UpdateVelocity();
    }

    void UpdateVelocity()
    {
        rigidbody.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == Tags.Dart)
        {
            progress.HitHotAirBalloon();
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
        else if (col.gameObject.tag == Tags.Player)
        {
            col.gameObject.GetComponent<Player>().Kill();
        }
    }
}
