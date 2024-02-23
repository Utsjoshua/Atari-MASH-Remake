using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject TreePrefab;
    public GameObject SoldierPrefab;

    private int TreeAmount;
    private int SoldierAmount;

    private Vector3[] TreeList;
    private Vector3[] SoldierList;
    private bool CheckSoldierPosition;
    private bool Same;

    public void SpawnTrees(){

        for (int i = 0; i < TreeAmount; i++){
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-3, 9), Random.Range(-4, 5), 0);
            TreeList[i] = randomSpawnPosition;
            Instantiate(TreePrefab, randomSpawnPosition, Quaternion.identity);
        }

    }

    public void SpawnSoldiers(){

        for (int i = 0; i < SoldierAmount; i++){

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-3, 9), Random.Range(-4, 5), 0);
            CheckSoldierPosition = true;
            Same = false;

            while (CheckSoldierPosition){
                Same = false;
                randomSpawnPosition = new Vector3(Random.Range(-3, 9), Random.Range(-4, 5), 0);

                foreach (Vector3 tree in TreeList){
                    if (randomSpawnPosition == tree){
                        Debug.Log("Same with tree");
                        Same = true;
                        break;
                    }
                }

                foreach (Vector3 soldier in SoldierList){
                    if (randomSpawnPosition == soldier){
                        Debug.Log("Same with soldier");
                        Same = true;
                        break;
                    }
                }

                if (!Same){
                    CheckSoldierPosition = false;
                }
            }

            SoldierList[i] = randomSpawnPosition;
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

        TreeList = new Vector3[TreeAmount];
        SoldierList = new Vector3[SoldierAmount];

        SpawnTrees();
        SpawnSoldiers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
