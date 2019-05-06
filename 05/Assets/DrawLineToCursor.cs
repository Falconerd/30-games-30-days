using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineToCursor : MonoBehaviour
{
  LineRenderer line;
  Vector3 mousePosition;
  // Start is called before the first frame update
  void Start()
  {
    line = GetComponent<LineRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    line.SetPositions(new Vector3[] { transform.position, mousePosition });
  }
}
