using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleColor { Cyan, Pink }

    [SerializeField]
    private ObstacleColor color = ObstacleColor.Cyan;

    [SerializeField]
    private GameObject pinkParticlesPrefab;
    [SerializeField]
    private GameObject cyanParticlesPrefab;

    private GameObject particlesPrefab;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Setup();
    }

    internal void SwapColor()
    {
        if (color == ObstacleColor.Cyan)
        {
            color = ObstacleColor.Pink;
        }
        else
        {
            color = ObstacleColor.Cyan;
        }
        Setup();
    }

    internal void SetColor(ObstacleColor obstacleColor)
    {
        color = obstacleColor;
        Setup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Summat hit me");
        if (collision.gameObject.tag == "Player")
        {
            if ((color == ObstacleColor.Cyan && collision.gameObject.name == "Cyan") ||
                (color == ObstacleColor.Pink && collision.gameObject.name == "Pink"))
            {
                GameManager.instance.PlayerScored();
            }
            else
            {
                GameManager.instance.PlayerDied();
            }
            Instantiate(particlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.instance.CameraShake(0.1f, 0.2f);
        }
    }

    void Setup()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (color == ObstacleColor.Cyan)
        {
            // spriteRenderer.color = GameManager.instance.cyanColor;
            particlesPrefab = cyanParticlesPrefab;
        }
        else
        {
            // spriteRenderer.color = GameManager.instance.pinkColor;
            particlesPrefab = pinkParticlesPrefab;
        }
    }
}
