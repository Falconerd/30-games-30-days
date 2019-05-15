using System.Collections;
using UnityEngine;

public class DropDepthCharges : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float proximityCountdownTime;
    [SerializeField]
    private float minimumSpawnTime;
    [SerializeField]
    private float maximumSpawnTime;
    [SerializeField]
    private float minimumSpawnAmount;
    [SerializeField]
    private float maximumSpawnAmount;
    [SerializeField]
    private BoxCollider2D boxCollider;

    private float proximityCountdownClock;
    private bool playerNearby;

    void Start()
    {

    }

    void Update()
    {
        if (playerNearby)
            proximityCountdownClock -= Time.deltaTime;

        if (proximityCountdownClock <= 0)
        {
            // Drop ze bombs
            StartCoroutine(Deploy());
            proximityCountdownClock = proximityCountdownTime;
        }

    }

    IEnumerator Deploy()
    {
        var spawnAmount = Random.Range(minimumSpawnAmount, maximumSpawnAmount);
        // Spawn a bunch of charges at random X positions within our own x size, at random times between some range
        for (var i = 0; i < spawnAmount; i++)
        {
            yield return new WaitForSeconds(Random.Range(minimumSpawnTime, maximumSpawnTime));
            var position = transform.position + Random.Range(-0.5f, 0.5f) * Vector3.right * boxCollider.bounds.size.x;
            Instantiate(prefab, position, Quaternion.identity);
        }
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNearby = false;
            proximityCountdownClock = proximityCountdownTime;
        }
    }
}
