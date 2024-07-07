using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D sb;
    private Vector2 movement;
    private int moveSpeed = 5;
    void Start()
    {
        transform.position = new Vector3(0, 3, 0);
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void PlayerInput()
    {
        movement = InputManager.Instance.playerControls.Movement.Move.ReadValue<Vector2>();
    }
    private void Move()
    {
        sb.MovePosition(sb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnEnable()
    {
        InputManager.Instance.playerControls.Movement.Jump.started += Jump;
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        StartCoroutine(JumpCooldown());
    }

    private IEnumerator JumpCooldown()
    {
        //todo fix the jump not working and look into IEnumerator
        //todo fix the sliding on the floor
        //todo look into callback and context
        sb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(5f);
    }
}
