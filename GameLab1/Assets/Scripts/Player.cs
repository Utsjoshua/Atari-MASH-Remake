using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 3.0f;
    private Vector3 movement;
    private float movementSqrMagnitude;
    private bool frozen = false;

    [Header("Soldier In Helicopter Counter")]
    [SerializeField] public TextMeshProUGUI SoldierNumber;
    public event EventHandler OnSoldierChanged;
    private int SoldierCounter;

    [Header("Soldier Rescued Counter")]
    [SerializeField] public TextMeshProUGUI RescuedNumber;
    private int RescuedCounter;

    [Header("Game Over UI")]
    public GameObject GameOverScreen;
    public Button RetryButton;
    public Button QuitButton;

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
            SetFrozen();
            GameOverScreen.SetActive(true);
            RetryButton.onClick.AddListener(Retry);
            QuitButton.onClick.AddListener(ExitGame);
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

    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame(){
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void SetFrozen(){
        if (frozen){
            frozen = false;
        }
        else{
            frozen = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (frozen == false){
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            movement = Vector3.ClampMagnitude(movement, 1.0f);
            movementSqrMagnitude = movement.sqrMagnitude;

            transform.Translate(movement * Speed * Time.deltaTime, Space.World);
        }
    }
}
