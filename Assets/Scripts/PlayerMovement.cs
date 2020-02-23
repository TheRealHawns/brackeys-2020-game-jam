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
    Animator anim;
    SpriteRenderer spr;

    private bool withinRange = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inv = GetComponent<PlayerInventory>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
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
        anim.SetFloat("Speed", rb.velocity.magnitude);
        if (rb.velocity.x != 0 && rb.velocity.x < 0)
        {
            spr.flipX = true;
        } else
        { if (rb.velocity.x != 0 && rb.velocity.x > 0)
            spr.flipX = false;
        }


        if (rb.velocity != Vector2.zero)
        {
            SoundManager.PlaySound(SoundManager.Sound.Walk);
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            HandleInteraction?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            inv.plant = (PlantToPlant)(((int)inv.plant + 1) % (int)PlantToPlant.MAX_LENGTH);
            var seedIcon = GameObject.Find("SeedToPlantBG").GetComponent<SeedIcon>();
            SoundManager.PlaySound(SoundManager.Sound.Buy);
            seedIcon.SwitchIcon(inv.plant);
        }

    }

    internal void Water()
    {
        StartCoroutine(WateringAnimation());
    }

    private IEnumerator WateringAnimation()
    {
        anim.SetTrigger("Water");
        rb.velocity = Vector2.zero;
        state = State.Wait;
        yield return new WaitForSecondsRealtime(1f);
        state = State.Move;
    }
}

public enum State
{
    Wait,
    Move
}