using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public State state = State.Move;

    public event Action HandleInteraction;

    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 500;
    PlayerInventory inv;

    private bool withinRange = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inv = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (state == State.Move) HandlePlayerMovement();
    }

    internal void SetWithinRange(bool isTrue)
    {
        withinRange = isTrue;
    }

    private void HandlePlayerMovement()
    {

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movement * moveSpeed;

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.Log("Pressing E");
            HandleInteraction();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            switch (inv.plant)
            {
                case PlantToPlant.Wheat:
                    inv.plant = PlantToPlant.Potato;
                    break;
                case PlantToPlant.Potato:
                    inv.plant = PlantToPlant.Carrot;
                    break;
                case PlantToPlant.Carrot:
                    inv.plant = PlantToPlant.Wheat;
                    break;
            }
            Debug.Log(inv.plant);
        }

    }

    internal void Water()
    {
        StartCoroutine(WateringAnimation());
    }

    private IEnumerator WateringAnimation()
    {
        rb.velocity = Vector2.zero;
        state = State.Wait;
        yield return new WaitForSecondsRealtime(0.75f);
        state = State.Move;
    }
}

public enum State
{
    Wait,
    Move
}