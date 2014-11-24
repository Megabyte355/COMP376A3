using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour
{
    [SerializeField]
    float maxVelocity;
    [SerializeField]
    float maxAngularVelocity;

    AudioSource waterBalloonPopSound;
    Progression progress;

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag(Tags.Progress).GetComponent<Progression>();

        float spinX = Random.Range(-maxAngularVelocity, maxAngularVelocity);
        float spinY = Random.Range(-maxAngularVelocity, maxAngularVelocity);
        float spinZ = Random.Range(-maxAngularVelocity, maxAngularVelocity);
        rigidbody.angularVelocity = new Vector3(spinX, spinY, spinZ);

        GameObject sounds = GameObject.FindGameObjectWithTag(Tags.Sounds) as GameObject;
        waterBalloonPopSound = sounds.transform.FindChild("WaterBalloonPop").GetComponent<AudioSource>();
    }

    void Update()
    {
        if(rigidbody.velocity.magnitude >= maxVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == Tags.Dart)
        {
            progress.HitWaterBalloon();
            Destroy(gameObject);
            Destroy(col.gameObject);
            waterBalloonPopSound.Play();
        }
        else if (col.gameObject.tag == Tags.Player)
        {
            Destroy(gameObject);
            col.gameObject.GetComponent<Player>().Kill();
            waterBalloonPopSound.Play();
        }
    }
}
