using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GetComponent<MoveTowards>().Target = GameObject.Find("Player").transform;
  }
}
