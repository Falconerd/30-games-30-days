using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallThrough : MonoBehaviour
{
    [SerializeField]
    private float playerCheckRayDistanceAbove = 0.5f;
    private Bounds bounds;

    #region Components
    private BoxCollider2D boxCollider;
    private PlatformEffector2D platformEffector;
    #endregion

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        platformEffector = GetComponent<PlatformEffector2D>();
        bounds = boxCollider.bounds;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && PlayerOnTop())
            boxCollider.enabled = false;
    }

    private bool PlayerOnTop()
    {
        // Shoot a ray from the left edge to the right edge and about half a unit above the platform
        // If it hits the player, we know they are on top
        var x = transform.position.x - bounds.size.x / 2;
        var y = transform.position.y + bounds.size.y / 2 + playerCheckRayDistanceAbove;
        var origin = new Vector2(x, y);
        var hit = Physics2D.Raycast(origin, Vector2.right, bounds.size.x);
        Debug.DrawRay(origin, Vector2.right * bounds.size.x, Color.red, Time.deltaTime);
        if (hit)
            if (hit.transform.tag == "Player") return true;
        return false;
    }

    private void Setup()
    {
        boxCollider.enabled = true;
    }
}
