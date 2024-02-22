using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject TreePrefab;
    public GameObject SoldierPrefab;

    private int TreeAmount;
    private int SoldierAmount;

    public void SpawnTrees(){

        for (int i = 0; i < TreeAmount; i++){
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-3, 9), Random.Range(-4, 5), 0);
            Instantiate(TreePrefab, randomSpawnPosition, Quaternion.identity);
        }

    }

    public void SpawnSoldiers(){

        for (int i = 0; i < SoldierAmount; i++){
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-3, 9), Random.Range(-4, 5), 0);
            Instantiate(SoldierPrefab, randomSpawnPosition, Quaternion.identity);
        }
        
    }

    public int GetTotalSoldiers(){
        return SoldierAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        TreeAmount = Random.Range(1,18);
        SoldierAmount = Random.Range(5,13);
        SpawnTrees();
        SpawnSoldiers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
