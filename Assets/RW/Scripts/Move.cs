using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
  public Vector3 speedHeart = new Vector3(0, 0, 20);
  

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.Translate(speedHeart*Time.deltaTime, Space.World);
    

  }
  

  

}
