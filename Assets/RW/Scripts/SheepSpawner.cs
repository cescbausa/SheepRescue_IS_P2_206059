using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject sheepPrefab;
    public bool canSpawn = true;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;
    public float decreaseSpawnCoef = 0.5F;

private List<GameObject> sheepList = new List<GameObject>(); // 5

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(Faster());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<MoveSheep>().SetSpawner(this);
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private IEnumerator Faster()
    {
        while(canSpawn)
        {
            timeBetweenSpawns = timeBetweenSpawns*decreaseSpawnCoef;
            yield return new WaitForSeconds(timeBetweenSpawns*5);
        }
    }
    
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }
    
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }




}
