using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Transform shootTransform;

    [SerializeField]
    float speed = 1f;

    Vector2 direction;

    private float targetHeight;

    bool canShoot = true;

    // -1.3 - 1

    private void Start()
    {
        direction = Random.Range(0, 100) > 50 ? Vector2.up : Vector2.down;
        SelectHeight();
    }

    private void Update()
    {


        if ((direction == Vector2.up && transform.position.y > targetHeight) ||
            direction == Vector2.down && transform.position.y < targetHeight)
        {
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot());
            }
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1);
        Instantiate(prefab, shootTransform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        SelectHeight();
        canShoot = true;
    }

    void SelectHeight()
    {
        targetHeight = Random.Range(-1.3f, 1f);
        direction = Vector2.up * Mathf.Sign(targetHeight - transform.position.y);
        Debug.Log("SelectHeight: " + targetHeight + " | " + direction);
    }
}
