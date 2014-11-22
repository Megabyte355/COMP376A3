using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    int numberOfAnchors;
    [SerializeField]
    int minBalloonsPerAnchor;
    [SerializeField]
    int maxBalloonsPerAnchor;

    [SerializeField]
    GameObject anchorPrefab;
    [SerializeField]
    Boundary boundary;
    void Start()
    {
        int anchorsInstantiated = 0;
        while (anchorsInstantiated < numberOfAnchors)
        {
            BoxCollider boxCollider = boundary.GetComponent<BoxCollider>();
            float posX = Random.Range(-boxCollider.size.x/2 * 0.85f, boxCollider.size.x/2 * 0.85f);
            float posY = Random.Range(-boxCollider.size.y/2 * 0.85f, boxCollider.size.y/2 * 0.85f);
            float posZ = Random.Range(-boxCollider.size.z/2 * 0.85f, boxCollider.size.z/2 * 0.85f);
            Vector3 position = boundary.transform.position + new Vector3(posX, posY, posZ);
            Debug.Log(position);
            GameObject freshAnchorObject = Instantiate(anchorPrefab, position, Quaternion.identity) as GameObject;
            Anchor freshAnchor = freshAnchorObject.GetComponent<Anchor>();
            freshAnchor.InitializeBalloons(Random.Range(minBalloonsPerAnchor, maxBalloonsPerAnchor));

            anchorsInstantiated++;
        }
    }
}
