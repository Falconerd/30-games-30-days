using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    internal GameObject Player;
    [SerializeField]
    private Text scoreText;
    private int score = 0;

    [SerializeField]
    private GameObject HealthGameObject;
    [SerializeField]
    private float maxHealth = 10;
    private float health;
    internal bool IsDead = false;

    [SerializeField]
    private ParticleSystem healthHitParticles;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private Text gameOverUIScoreText;

    internal static GameManager instance;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        health = maxHealth;
    }

    void Update()
    {
        if (IsDead)
        {
            if (Input.GetButtonDown("Fire1"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }

    internal void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    internal void DecreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            IsDead = true;
            gameOverUI.SetActive(true);
            gameOverUIScoreText.text = score.ToString();
        }
        healthHitParticles.Play();
        UpdateHealthSprite();
    }

    internal void IncreaseHealth()
    {
        health++;
        if (health > maxHealth)
            health = maxHealth;
        UpdateHealthSprite();
    }

    void UpdateHealthSprite()
    {
        var newScale = HealthGameObject.transform.localScale;
        newScale.x = (health / maxHealth) * 2;
        newScale.y = (health / maxHealth) * 2;
        HealthGameObject.transform.localScale = newScale;
    }

}
