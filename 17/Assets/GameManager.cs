using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    internal static GameManager instance;

    public Vector3 cyanTarget;
    public Vector3 pinkTarget;

    public Color cyanColor = new Color(0, 255, 255);
    public Color pinkColor = new Color(255, 0, 255);

    int score;
    float lastTimeScored;

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    CameraShake cameraShake;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal void PlayerScored()
    {
        if (Time.time - lastTimeScored > 0.1f)
        {
            score++;
            scoreText.text = score.ToString();
            lastTimeScored = Time.time;
        }
    }

    internal void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    internal void CameraShake(float duration, float magnitude)
    {
        cameraShake.Shake(duration, magnitude);
    }
}
