using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
  [SerializeField] GameObject tailPrefab;
  [SerializeField] AudioSource hitSound;
  [SerializeField] AudioSource eatSound;
  List<GameObject> tail = new List<GameObject>();
  LineRenderer line;
  GameManager gameManager;
  void Start()
  {
    line = transform.Find("Line").GetComponent<LineRenderer>();
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  void Update()
  {
    if (gameManager.GameOver) return;
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // Interesting tween
    Vector3 moveAmount = (mousePosition - transform.position) * .1f;
    moveAmount.z = transform.position.z;
    transform.position += moveAmount;
    var newRotation = Quaternion.LookRotation((transform.position - mousePosition).normalized, Vector3.forward);
    newRotation.x = newRotation.y = 0;
    transform.rotation = newRotation;

    UpdateLine();
  }

  void UpdateLine()
  {
    var positions = new Vector3[1 + tail.Count];
    positions[0] = transform.position;
    for (var i = 1; i < tail.Count + 1 && tail.Count > 0; i++)
    {
      positions[i] = tail[i - 1].transform.position;
    }
    line.positionCount = positions.Length;
    line.SetPositions(positions);
  }

  internal void SpawnTailSegment()
  {
    eatSound.Play();
    gameManager.UpdateScore(tail.Count);
    // GameObject parent = tail.Any() ? tail[tail.Count - 1] : gameObject;
    var parent = tail.LastOrDefault() ?? gameObject;

    var tailObject = Instantiate(tailPrefab, parent.transform.position, Quaternion.identity);
    tailObject.GetComponent<Tail>().SetParent(parent);
    tail.Add(tailObject);

    // Get every tail segment and trigger spawn anim
    for (var i = 0; i < tail.Count; i++)
    {
      tail[i].GetComponent<Tail>().QueueSpawnAnim(i * 0.1f);
      // var newScale = tail[i].transform.localScale;
      // @TODO Scale the tails so they are large to small
    }
  }

  internal void KillTailFrom(GameObject hit)
  {
    hitSound.Play();
    var index = tail.IndexOf(hit);
    // Destroy all Tail GameObjects after this index
    // Maybe we could work backwards up the tail until we hit this index, then stop
    for (var i = tail.Count - 1; i >= index; i--)
    {
      tail[i].GetComponent<Tail>().Kill();
    }
    tail = tail.GetRange(0, index);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      gameManager.TriggerGameOver();
    }
  }
}
