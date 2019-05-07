using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePitch : MonoBehaviour
{
  [SerializeField] float min;
  [SerializeField] float max;
  AudioSource source;

  void Start()
  {
    source = GetComponent<AudioSource>();
    source.pitch = Random.Range(min, max);
  }
}
