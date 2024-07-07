using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private int jumps = 0;
    private int maxJumps = 2;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool saved = false;
    private Vector2 savedVelo;
    private Vector2 savedPos;


    // Start is called before the first frame update
    void Start()
    {
    }
    
    void Update()
    {
        //horizontal will be 1 or -1 depending on what you press
        horizontal = Input.GetAxisRaw("Horizontal");
        
        //if want jump and Touching ground
        jumps = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) ? maxJumps : jumps;
        if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            //leave the x alone and increase the jumping power
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumps--;
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            //if you are going up and you release the jump button then the rb velocity will start to approach zero, allowing gravity to take over
            //and resulting in an early stop of the jump
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .05f);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!saved)
            {
                savedVelo = rb.velocity * 2;
                savedPos = transform.position;
                saved = true;
            }
            else
            {
                transform.position = savedPos;
                rb.velocity += savedVelo;
                saved = false;
            }
        }
        
    }
    private void FixedUpdate()
    {
        //this is setting the rigidbody velocity to the raw val and multypling to be faster
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //this leaves the gravity of the rigidbody alone
    }
}
