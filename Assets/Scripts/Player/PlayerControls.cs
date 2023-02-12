using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour
{
    public PlayerSettingsScriptable setting;

    //Make it simple and put booster gameobject here for now
    public GameObject booster;

    //Add this action for booster animations and such
    public static Action IsAccelerating;
    private float acceleration = 5.4f;
    private float rotationSpeed = 0.4f;
    private float maxSpeed = 7.0f;
    private bool isAccelerating = false;
    private bool canControl = false;

    //Pass transform here so we can customize play around with custom bullets
    public Action<Transform> WeaponShootEvent;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Initialize();
        GameManager.Instance.StartGameEvent += StartGame;
    }
    private void Initialize()
    {
        acceleration = setting.shipSpeed;
        rotationSpeed = setting.rotationSpeed;
    }

    private void StartGame()
    {
        canControl = true;
    }

    public void Fire()
    {
        WeaponShootEvent(transform);
    }

    void Update()
    {
        if (!canControl)
        {
            return;
        }

        //Basic controls, for PC only
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0,0, rotationSpeed));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            isAccelerating = true;
        } else
        {
            isAccelerating = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            WeaponShootEvent(transform);
        }
    }

    private void FixedUpdate()
    {
        if (isAccelerating && rigidBody.velocity.magnitude < maxSpeed)
        {
            IsAccelerating?.Invoke();
            rigidBody.AddForce(transform.up * acceleration);
            if (!booster.activeSelf)
            {
                booster.SetActive(true);
            }
        } else if(booster.activeSelf)
        {
            booster.SetActive(false);
        }
    }

 
}
