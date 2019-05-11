using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateScenery : MonoBehaviour
{
    [SerializeField]
    private float zDistance = 20;
    [SerializeField]
    private GameObject centreScenery;
    [SerializeField]
    private GameObject leftScenery;
    [SerializeField]
    private GameObject rightScenery;

    private GameObject currentScenery;

    private BoxCollider2D boxCollider;

    void Start()
    {
        currentScenery = centreScenery;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Check which way we went
            var direction = Mathf.Sign(other.transform.position.x - boxCollider.offset.x);
            // We went right
            if (direction == 1)
            {
                // Grab the scenery on the left
                // Move it to the right
                var x = leftScenery.transform.position.x + boxCollider.size.x * 3;
                leftScenery.transform.position = new Vector3(x, 0, zDistance);
                // Tmp variables to make it easier for my stupid brain
                var a = leftScenery;
                var b = centreScenery;
                var c = rightScenery;
                centreScenery = c;
                leftScenery = b;
                rightScenery = a;
                boxCollider.offset = new Vector3(boxCollider.offset.x + boxCollider.size.x, 0, zDistance);
            }
            else if (direction == -1)
            {
                // Grab the scenery on the right 
                // Move it to the left
                var x = rightScenery.transform.position.x - boxCollider.size.x * 3;
                rightScenery.transform.position = new Vector3(x, 0, zDistance);
                // Tmp variables to make it easier for my stupid brain
                var a = leftScenery;
                var b = centreScenery;
                var c = rightScenery;
                centreScenery = a;
                leftScenery = c;
                rightScenery = b;
                boxCollider.offset = new Vector3(boxCollider.offset.x - boxCollider.size.x, 0, zDistance);
            }
        }
    }
}
