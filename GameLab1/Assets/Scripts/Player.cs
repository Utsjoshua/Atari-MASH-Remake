using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 3.0f;
    private Vector3 movement;
    private float movementSqrMagnitude;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        //If player touches a Soldier
        Soldier soldier = collider.GetComponent<Soldier>();
        if (soldier != null)
        {
            //AddSoldier();
            //audioSource.clip = keyclip;
            //audioSource.Play();
            Debug.Log("Got Soldier");
            Destroy(soldier.gameObject);
        }

        //If player touches a Tree
        Tree tree = collider.GetComponent<Tree>();
        if (tree != null)
        {
            Debug.Log("Hit tree");
        }

        Hospital hospital = collider.GetComponent<Hospital>();
        if (hospital != null)
        {
            //ReleaseSoldiers();
            Debug.Log("Touched hospital");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (frozen == false){
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            movement = Vector3.ClampMagnitude(movement, 1.0f);
            movementSqrMagnitude = movement.sqrMagnitude;

            transform.Translate(movement * Speed * Time.deltaTime, Space.World);
        //}
    }
}
