using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NamedSound
{
  public string name;
  public AudioSource source;
}
public class SoundManager : MonoBehaviour
{
  [SerializeField] NamedSound[] sounds;

  public Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();
  void Start()
  {
    for (int i = 0; i < sounds.Length; i++)
    {
      if (!sources.ContainsKey(sounds[i].name))
        sources.Add(sounds[i].name, sounds[i].source);
    }
  }


  internal AudioSource GetAudioSource(string clipName)
  {
    return sources[clipName];
  }
}
