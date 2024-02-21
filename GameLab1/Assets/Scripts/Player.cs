using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 3.0f;
    private Vector3 movement;
    private float movementSqrMagnitude;

    [Header("Soldier In Helicopter Counter")]
    [SerializeField] public TextMeshProUGUI SoldierNumber;
    public event EventHandler OnSoldierChanged;
    private int SoldierCounter;

    [Header("Soldier Rescued Counter")]
    [SerializeField] public TextMeshProUGUI RescuedNumber;
    private int RescuedCounter;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        //If player touches a Soldier
        Soldier soldier = collider.GetComponent<Soldier>();
        if (soldier != null)
        {
            if (SoldierCounter < 3)
            {
                AddSoldier();
                //audioSource.clip = keyclip;
                //audioSource.Play();
                Debug.Log("Got Soldier");
                Destroy(soldier.gameObject);
            }
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
            ReleaseSoldiers();
            Debug.Log("Touched hospital");
        }
    }

    public void AddSoldier()
    {
        SoldierCounter += 1;
        OnSoldierChanged?.Invoke(this, EventArgs.Empty);
        SoldierNumber.text = SoldierCounter.ToString();
    }

    public void ReleaseSoldiers()
    {
        RescuedCounter += SoldierCounter;
        SoldierCounter = 0;
        RescuedNumber.text = RescuedCounter.ToString();
        SoldierNumber.text = SoldierCounter.ToString();
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
