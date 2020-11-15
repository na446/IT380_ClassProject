using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is responsible for moving the player left and right. 
public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight; //Determines which way the player is currently facing.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    private Vector3 m_Velocity = Vector3.zero;

    Animator animate;//reference to animator that animates player walking

    public void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animate = GetComponent<Animator>();
    }

    public void Move(float move)
    {
        // Move the character by finding the target velocity & add a smooth damp
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left (or vice versa) flip the player.
        if (move > 0 && !m_FacingRight)
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
            Flip();
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (horizontalMove != 0)
            animate.SetBool("isWalking", true);
        else
            animate.SetBool("isWalking", false);
    }

    void FixedUpdate()
    {
        // Move our character
        Move(horizontalMove * Time.fixedDeltaTime);
    }

}