using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingHay : MonoBehaviour
{
  public float limitHayMov = 21;
  public float movementSpeed;
  public GameObject hayBalePrefab;
  public Transform haySpawnpoint;
  public float shootInterval;
  public float shootIntervalUpgrade;
  private float shootTimer;
  public Transform modelParent;
  public GameObject blueModelPrefab;
  public GameObject yellowModelPrefab;
  public GameObject redModelPrefab;
  public GameObject hayPrefab;
  public static MovingHay Instance;
  public int sheepToUpgrade;
  // Start is called before the first frame update
  void Start()
  {
    LoadModel();
  }
  private void LoadModel()
  {
      Destroy(modelParent.GetChild(0).gameObject);

      switch (GameSettings.hayMachineColor)
      {
          case HayMachineColor.Blue:
              Instantiate(blueModelPrefab, modelParent);
          break;

          case HayMachineColor.Yellow:
              Instantiate(yellowModelPrefab, modelParent);
          break;

          case HayMachineColor.Red:
              Instantiate(redModelPrefab, modelParent);
          break;
      }
  }
  void Awake()
  {
    Instance = this;
  }

  // Update is called once per frame
  void Update()
  {
    UpdateMovement();
    UpdateShooting();
  }
  void UpdateMovement()
  {
    float horizontalInput = Input.GetAxisRaw("Horizontal");

    if (horizontalInput < 0 && transform.position.x > -limitHayMov)
    {
      transform.Translate(-transform.right*movementSpeed*Time.deltaTime);
    }
    if (horizontalInput > 0 && transform.position.x < limitHayMov)
    {
      transform.Translate(transform.right*movementSpeed*Time.deltaTime);
    }
    //if (Input.GetKeyDown(KeyCode.Space))
    //{
    //  Instantiate(hayPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    //}
    
  }
  private void UpdateShooting()
    {
      shootTimer -= Time.deltaTime;

      if (shootTimer <= 0 && Input.GetKey(KeyCode.Space) && GameStateManager.Instance.sheepSaved < sheepToUpgrade)
      {
          shootTimer = shootInterval;
          ShootHay();
      }
      else if(shootTimer <= 0 && Input.GetKey(KeyCode.Space) && GameStateManager.Instance.sheepSaved >= sheepToUpgrade)
      {
          shootTimer = shootIntervalUpgrade;
          ShootHay();
      }
      
    }

    private void ShootHay()
    {
      Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
      SoundManager.Instance.PlayShootClip();

    }


}
