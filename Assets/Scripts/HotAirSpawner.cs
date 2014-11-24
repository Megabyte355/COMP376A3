using UnityEngine;
using System.Collections;

public class HotAirSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject hotAirPrefab;

    Boundary boundary;
    void Start()
    {
        boundary = GameObject.FindGameObjectWithTag(Tags.Boundary).GetComponent<Boundary>();
    }

    public void Spawn()
    {
        BoxCollider boundaryCollider = boundary.GetComponent<BoxCollider>();
        Vector3 center = boundaryCollider.center;
        Vector3 size = boundaryCollider.size;

        float randomX = Random.Range(-1f, 1f) * size.x / 2 * 0.80f;
        float randomY = Random.value * size.y / 2 * 0.80f;
        float randomZ = Random.Range(-1f, 1f) * size.z / 2 * 0.80f;
        Vector3 randomSpot = new Vector3(randomX, randomY, randomZ) + center + boundaryCollider.transform.position;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        GameObject hotAirObject = Instantiate(hotAirPrefab, randomSpot, Quaternion.identity) as GameObject;
        HotAirBalloon hotAirBalloon = hotAirObject.GetComponent<HotAirBalloon>();

        // Randomize color
        Color newColor = new Color(Random.value, Random.value, Random.value, 1f);
        hotAirBalloon.SetColor(newColor);

        // Set direction
        hotAirBalloon.SetDirection(randomDirection);
    }


}
