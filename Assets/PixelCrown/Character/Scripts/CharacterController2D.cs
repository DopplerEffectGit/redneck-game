using System;
using UnityEngine;


namespace PixelCrown
{
    namespace Character
    {
        // Activation structure
        [Serializable]
        public class Activation
        {
            [SerializeField]
            [Tooltip("Enable jump")]
            public bool enableJump = true;

            [SerializeField]
            [Tooltip("Enable walk")]
            public bool enableWalk = true;

            [SerializeField]
            [Tooltip("Enable always run")]
            public bool enableAlwaysRun = true;

            [SerializeField]
            [Tooltip("Enable long jump. character can jump higher by keep pressing the jump button")]
            public bool enableLongJump = true;

            [SerializeField]
            [Tooltip("Enable character double jump")]
            public bool enableDoubleJump = true;

            [SerializeField]
            [Tooltip("Enable wall surfing. character can slow down fall wall by going forward a wall")]
            public bool enableWallSurfing = true;

            [SerializeField]
            [Tooltip("Enable wall jump. character can jump again if against a wall")]
            public bool enableWallJump = true;

            [SerializeField]
            [Tooltip("Enable character crouch")]
            public bool enableCrouch = true;

            [SerializeField]
            [Tooltip("Enable changing direction during jump")]
            public bool enableAirControl = true;
        }

        // Movement structure
        [Serializable]
        public class Movement
        {
            [Tooltip("Run speed")]
            public float runSpeed = 600.0f;

            [Tooltip("Walk speed")]
            public float walkSpeed = 300.0f;

            [Tooltip("Force up for the initial jump")]
            public float jumpForce = 500.0f;

            [Tooltip("Force up for the long jump")]
            public float longJumpForce = 1200.0f;

            [Tooltip("Time for the long jump in seconds")]
            public float longJumpTime = 0.6f;

            [Tooltip("Maximum number of double jump")]
            public int doubleJumpMax = 1;

            [Range(0, 1)]
            [Tooltip("Max speed applied to crouching movement relative to walking speed. 1 = 100%")]
            public float crouchWalkSpeed = 0.5f;

            [Range(0, 1.0f)]
            [Tooltip("Smooth of the character movement when on the ground")]
            public float groundMovementSmoothing = 0.15f;

            [Range(0, 1.0f)]
            [Tooltip("Smooth of the character movement when in the air")]
            public float airMovementSmoothing = 0.15f;

            [Range(0, 2.0f)]
            [Tooltip("Air direction control when changing direction")]
            public float airMovement = 0.5f;

            [Range(0, 2.0f)]
            [Tooltip("Friction factor of the character on the ground")]
            public float groundFriction = 1.0f;

            [Range(0, 4.0f)]
            [Tooltip("Change falling gravity when falling. Normal gravity is 1")]
            public float jumpingGravity = 2.0f;

            [Range(0, 4.0f)]
            [Tooltip("Change falling gravity when falling. Normal gravity is 1")]
            public float fallingGravity = 2.75f;

            [Range(0, 2.0f)]
            [Tooltip("Friction factor of the character in the air")]
            public float airFriction = 0.2f;

            [Tooltip("List of animator components that handles the character animation. Optional")]
            public Animator[] animators;
        }


        // Detection structure
        [Serializable]
        public class Detection
        {
            [SerializeField]
            [Tooltip("Which layer is ground for ground and wall detection. optional")]
            public LayerMask whatIsGround;

            [SerializeField]
            [Tooltip("Object to detect ground to be placed at the bottom of the object sprite. optional")]
            public Transform groundCheckObject;

            [SerializeField]
            [Tooltip("Object to detect ceiling to be placed at the top of the object sprite. optional")]
            public Transform ceilingCheckObject;

            [SerializeField]
            [Tooltip("Object to spawn step effects and landing effects. optional")]
            public Transform stepCheckObject;

            [SerializeField]
            [Tooltip("Object to detect wall to be placed in front of character sprite. optional")]
            public Transform frontWallCheckObject;

            [SerializeField]
            [Tooltip("Collider used when crouching. optional")]
            public Collider2D crouchCollider;

            [SerializeField]
            [Tooltip("Collider used when standing. optional")]
            public Collider2D standCollider;

            [SerializeField]
            [Tooltip("Detection radius for ground and wall detection")]
            public float detectRadius = 0.1f;
        }

        
        // Effect structure
        [Serializable]
        public class Effect
        {
            [SerializeField]
            [Tooltip("Emitted effect in stepCheckObject position when walking. optional")]
            public GameObject dustWalkingEmitter;

            [SerializeField]
            [Tooltip("Emitted effect in stepCheckObject position when jumping. optional")]
            public GameObject dustJumpingEmitter;

            [Tooltip("Emitted effect in stepCheckObject when landing on ground. optional")]
            public GameObject dustLandingEmitter;

            [Tooltip("Emitted effect in frontWallCheckObject position when landing on ground. optional")]
            public GameObject dustWallSurfingEmitter;

            [Tooltip("Emitted effect in stepCheckObject position when double jumping. optional")]
            public GameObject dustDoubleJumpEmitter;

            [Tooltip("Time between effects when walking in seconds")]
            public float dustTime = 0.2f;
        }


        public class CharacterController2D : MonoBehaviour
        {
            [SerializeField]
            public PixelCrown.Character.Movement m_movement;

            [SerializeField]
            public PixelCrown.Character.Activation m_activation;

            [SerializeField]
            public PixelCrown.Character.Detection m_detection;

            [SerializeField]
            public PixelCrown.Character.Effect m_effect;


            // True if the character is crouching
            protected bool m_isCrouching = false;

            // True if the character is not on ground and going up
            protected bool m_isJumping = false;

            // True if the character is not on ground and going up
            protected bool m_isWalking = false;

            // True if the character is not on ground and going up
            protected bool m_isRunning = false;

            // True if the character is not on the ground and going down
            protected bool m_isFalling = false;

            // True if the character is wall surfing
            protected bool m_isWallSurfing = false;


            // For determining which way the character is currently facing. Use this from the animator to check for
            // the character direction
            protected bool m_isFacingRight = true;

            // True if character is on ground
            protected bool m_isGrounded = false;

            // Reference to velocity
            protected Vector3 velocity = Vector3.zero;

            // Last known position
            protected Vector3 lastPosition;

            // True if front wall is detected
            protected bool frontWallDetected = false;

            private float longJumpEndTime = 0.0f;
            private bool releaseJump = true;
            private float releaseJumpTime = 0.0f;
            private int doubleJumpCount = 0;
            private float jumpStartTime = 0.0f;
            private float dustEndTime = 0.0f;
            private bool canDoubleJump = false;
            private bool canWallJump = false;

            // Local rigid body of character to apply movement
            protected Rigidbody2D localRigidbody2D;


            public void Start()
            {
                localRigidbody2D = GetComponent<Rigidbody2D>();
                if (localRigidbody2D)
                {
                    localRigidbody2D.sharedMaterial = new PhysicsMaterial2D();
                    localRigidbody2D.sharedMaterial.bounciness = 0.0f;
                    localRigidbody2D.sharedMaterial.friction = 0.0f;
                }
            }

            // Check for wall in front of character
            private void CheckWalls()
            {
                Collider2D[] colliders;

                // if the front wall check object is set
                if (m_detection.frontWallCheckObject)
                {
                    frontWallDetected = false;

                    // Get everything in the detect radius that is ground
                    colliders = Physics2D.OverlapCircleAll(m_detection.frontWallCheckObject.position, m_detection.detectRadius, m_detection.whatIsGround);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        // unless it is the gameObject
                        if (colliders[i].gameObject != gameObject)
                        {
                            // front wall is detected
                            frontWallDetected = true;

                            // stop here
                            break;
                        }
                    }
                }
            }

            // Check for ground below character
            private void CheckGround()
            {
                // No ground check object
                if (!m_detection.groundCheckObject)
                {
                    // Always on ground
                    m_isGrounded = true;
                    return;
                }

                // If going up
                if (!m_isGrounded && localRigidbody2D.velocity.y > 0)
                {
                    // Dont check ground
                    return;
                }

                bool wasGrounded = m_isGrounded;
                m_isGrounded = false;

                // detects colliders below character
                Collider2D[] colliders = Physics2D.OverlapCircleAll(m_detection.groundCheckObject.position, m_detection.detectRadius, m_detection.whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    // If collider is not gameObject
                    if (colliders[i].gameObject != gameObject)
                    {
                        m_isGrounded = true;

                        // If was grounded and objects are set
                        if (m_effect.dustLandingEmitter && m_detection.stepCheckObject && !wasGrounded)
                        {
                            // spawn dust landing effect
                            Instantiate(m_effect.dustLandingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                        }
                        break;
                    }
                }
            }


            protected void Flip()
            {
                // Switch object facing
                m_isFacingRight = !m_isFacingRight;
                transform.Rotate(0f, 180f, 0f);
            }


            // Move the character toward a direction or make it crouch / jump
            public void Move(float direction, bool crouch, bool jump, bool walkToggle)
            {
                bool running = false;
                bool walking = false;
                bool crouching = false;

                if (!m_activation.enableAlwaysRun && walkToggle || m_activation.enableAlwaysRun && !walkToggle)
                {
                    running = true;
                    walking = false;
                }
                else
                {
                    running = false;
                    walking = true;
                }

                // Dont enable jump
                if (jump && !m_activation.enableJump)
                {
                    jump = false;
                }

                // We can't crouch and jump at the same time
                if (crouch && jump)
                {
                    crouch = false;
                    crouching = false;
                }

                // Check the ground
                CheckGround();

                // Check the walls
                CheckWalls();

                // Check when the jump button is released
                if (m_activation.enableJump && !jump && !releaseJump && releaseJumpTime < Time.time + 0.1f)
                {
                    releaseJump = true;
                }

                // Set the speed
                float speed = 0;
                if (running)
                {
                    speed = direction * m_movement.runSpeed * Time.deltaTime * 100;
                }
                else
                {
                    speed = direction * m_movement.walkSpeed * Time.deltaTime * 100;
                }

                // Check if character can stand
                if (m_detection.ceilingCheckObject && !crouch && m_isGrounded)
                {
                    // Keep character crouching
                    if (Physics2D.OverlapCircle(m_detection.ceilingCheckObject.position, m_detection.detectRadius, m_detection.whatIsGround))
                    {
                        crouch = true;
                        crouching = true;

                        // Also cannot jump when crouched
                        jump = false;
                    }
                }

                // only control the character if grounded
                if (m_isGrounded)
                {
                    // If crouching
                    if (m_activation.enableCrouch && crouch)
                    {
                        crouching = true;
                        walking = true;
                        running = false;
                        speed = direction * m_movement.walkSpeed * m_movement.crouchWalkSpeed * Time.deltaTime * 100;

                        // Change colliders when crouching
                        if (m_detection.crouchCollider != null)
                        {
                            m_detection.crouchCollider.isTrigger = false;
                        }
                        if (m_detection.standCollider != null)
                        {
                            m_detection.standCollider.isTrigger = true;
                        }
                    }
                    else
                    {
                        crouching = false;

                        // Change colliders when crouching
                        if (m_detection.standCollider != null)
                        {
                            m_detection.standCollider.isTrigger = false;
                        }
                        if (m_detection.crouchCollider != null)
                        {
                            m_detection.crouchCollider.isTrigger = true;
                        }
                    }

                    if (speed != 0)
                    {
                        // Move the character by finding the target velocity
                        Vector2 targetVelocity = new Vector2(speed * Time.deltaTime, localRigidbody2D.velocity.y);

                        // And then smoothing it out and applying it to the character
                        localRigidbody2D.velocity = Vector3.SmoothDamp(localRigidbody2D.velocity, targetVelocity, ref velocity, m_movement.groundMovementSmoothing);
                    }


                    // Simulated horizontal friction
                    if (localRigidbody2D.velocity.x != 0)
                    {
                        Vector2 friction = Vector2.zero;
                        friction.x = localRigidbody2D.velocity.x - localRigidbody2D.velocity.x * 10.0f * Time.deltaTime * m_movement.groundFriction;
                        friction.y = localRigidbody2D.velocity.y;
                        localRigidbody2D.velocity = friction;
                    }

                    // Set gravity
                    localRigidbody2D.gravityScale = 1.0f;

                    // If the input is moving the character right and the character is facing left...
                    if (speed > 0 && !m_isFacingRight)
                    {
                        // Flip the character
                        Flip();
                    }
                    // Otherwise if the input is moving the character left and the character is facing right...
                    else if (speed < 0 && m_isFacingRight)
                    {
                        // Flip the character
                        Flip();
                    }
                }

                if (!m_isGrounded)
                {
                    m_detection.crouchCollider.isTrigger = true;
                    m_detection.standCollider.isTrigger = false;

                    // only control the character if airControl is turned on
                    if (m_activation.enableAirControl)
                    {
                        if (speed != 0)
                        {
                            // Move the character
                            Vector3 targetVelocity = new Vector2(speed * Time.deltaTime * m_movement.airMovement, localRigidbody2D.velocity.y);

                            // And then smoothing it out and applying it to the character
                            localRigidbody2D.velocity = Vector3.SmoothDamp(localRigidbody2D.velocity, targetVelocity, ref velocity, m_movement.airMovementSmoothing);
                        }
                    }

                    // Simulated air friction
                    if (localRigidbody2D.velocity.x != 0)
                    {
                        Vector2 friction = Vector2.zero;
                        friction.x = localRigidbody2D.velocity.x - localRigidbody2D.velocity.x * 5.0f * Time.deltaTime * m_movement.airFriction;
                        friction.y = localRigidbody2D.velocity.y;
                        localRigidbody2D.velocity = friction;
                    }

                    // Simulated air push
                    if (lastPosition != null)
                    {
                        Vector3 targetVelocity = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);
                        localRigidbody2D.velocity = Vector3.SmoothDamp(localRigidbody2D.velocity, targetVelocity * 50.0f, ref velocity, 0.15f);
                    }

                    // Set falling gravity
                    if (localRigidbody2D.velocity.y < 0)
                    {
                        localRigidbody2D.gravityScale = m_movement.fallingGravity;
                    }
                    else
                    {
                        localRigidbody2D.gravityScale = m_movement.jumpingGravity;
                    }

                    // If the input is moving the character right and the character is facing left
                    if (speed > 0 && !m_isFacingRight)
                    {
                        // Flip the character
                        Flip();
                    }
                    // Otherwise if the input is moving the character left and the character is facing right
                    else if (speed < 0 && m_isFacingRight)
                    {
                        // Flip the character
                        Flip();
                    }
                }

                // Stops long jump if character is going down
                if (!m_isGrounded && Time.time > longJumpEndTime && localRigidbody2D.velocity.y <= 0)
                {
                    longJumpEndTime = Time.time;
                }

                bool canJump = true;
                if (Time.time - jumpStartTime < 0.2f)
                {
                    canJump = false;
                }

                // Character jump
                if (m_activation.enableJump && releaseJump && m_isGrounded && canJump && jump)
                {
                    // Add a vertical force to the character
                    m_isGrounded = false;
                    releaseJump = false;
                    canDoubleJump = false;
                    releaseJumpTime = Time.time;
                    jumpStartTime = Time.time;
                    longJumpEndTime = Time.time + m_movement.longJumpTime;
                    doubleJumpCount = m_movement.doubleJumpMax;

                    localRigidbody2D.AddForce(new Vector2(0f, m_movement.jumpForce));
                    //localRigidbody2D.velocity += new Vector2(0f, movement.jumpForce * Time.deltaTime);

                    // add jump effect
                    if (m_effect.dustJumpingEmitter && m_detection.stepCheckObject)
                    {
                        Instantiate(m_effect.dustJumpingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                    }
                }
                else if (m_activation.enableJump && !m_isGrounded && jump)
                {
                    // Check for wall jump
                    if (m_activation.enableWallJump && canWallJump && canJump)
                    {
                        // Add wall jump
                        float horizontalForce = 0;
                        if (!m_isFacingRight)
                        {
                            horizontalForce = m_movement.jumpForce;
                        }
                        else
                        {
                            horizontalForce = -m_movement.jumpForce;
                        }

                        localRigidbody2D.AddRelativeForce(new Vector2(horizontalForce * 0.8f, m_movement.jumpForce * 0.8f));

                        // add wall jump effect
                        if (m_effect.dustLandingEmitter && m_detection.stepCheckObject)
                        {
                            Instantiate(m_effect.dustLandingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                        }

                        canWallJump = false;
                        releaseJump = false;
                        canDoubleJump = false;
                        releaseJumpTime = Time.time;
                        jumpStartTime = Time.time;
                        longJumpEndTime = Time.time + m_movement.longJumpTime;
                        doubleJumpCount = m_movement.doubleJumpMax;
                    }
                    // Check for double jump
                    else if (m_activation.enableDoubleJump && canJump && canDoubleJump)
                    {
                        // Double jump factor is from 25% to 100% after 1s
                        float doubleJumpFactor = Mathf.Max(0.25f, Mathf.Min(1.0f, Time.time - jumpStartTime));

                        // Add double jump
                        localRigidbody2D.AddRelativeForce(new Vector2(0f, m_movement.jumpForce * doubleJumpFactor));

                        // double jump effect
                        if (m_effect.dustDoubleJumpEmitter && m_detection.stepCheckObject)
                        {
                            Instantiate(m_effect.dustDoubleJumpEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                        }

                        releaseJump = false;
                        canDoubleJump = false;
                        releaseJumpTime = Time.time;
                        jumpStartTime = Time.time;
                        longJumpEndTime = Time.time + m_movement.longJumpTime;
                        doubleJumpCount--;
                    }
                    // Check long jump
                    else if (m_activation.enableLongJump && Time.time < longJumpEndTime)
                    {
                        // Add long jump
                        localRigidbody2D.AddRelativeForce(new Vector2(0f, Mathf.Lerp(0.0f, m_movement.longJumpForce, (longJumpEndTime - Time.time) / m_movement.longJumpTime) * Time.deltaTime));
                    }
                }

                if (!m_isGrounded && !jump)
                {
                    if (doubleJumpCount > 0)
                    {
                        canDoubleJump = true;
                    }
                }

                m_isWallSurfing = false;

                // Wall surfing
                if (!m_isGrounded && localRigidbody2D.velocity.y < 0 && frontWallDetected && Mathf.Abs(direction) > 0)
                {
                    if (m_activation.enableWallSurfing)
                    {
                        localRigidbody2D.AddRelativeForce(new Vector2(0f, Mathf.Abs(localRigidbody2D.velocity.y) * 10));
                        m_isWallSurfing = true;

                        if (m_effect.dustWallSurfingEmitter && m_detection.frontWallCheckObject && Time.time > dustEndTime)
                        {
                            Instantiate(m_effect.dustWallSurfingEmitter, m_detection.frontWallCheckObject.position, m_detection.frontWallCheckObject.rotation);
                            dustEndTime = Time.time + m_effect.dustTime;
                        }
                    }

                    if (m_activation.enableWallJump && canJump)
                    {
                        canWallJump = true;
                    }
                    else
                    {
                        canWallJump = false;
                    }
                }
                else
                {
                    canWallJump = false;
                }

                // Walking dust
                if (m_effect.dustWalkingEmitter && m_isGrounded && Time.time > dustEndTime && m_detection.stepCheckObject != null && Mathf.Abs(localRigidbody2D.velocity.x) > 0.75f)
                {
                    Instantiate(m_effect.dustWalkingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                    dustEndTime = Time.time + m_effect.dustTime;
                }

                // Set the variables for animator
                if (!m_isGrounded)
                {
                    m_isWalking = false;
                    m_isRunning = false;
                    m_isCrouching = false;

                    if (localRigidbody2D.velocity.y >= 0)
                    {
                        m_isJumping = true;
                        m_isFalling = false;
                    }
                    else
                    {
                        m_isJumping = false;
                        m_isFalling = true;
                    }
                }
                else
                {
                    m_isJumping = false;
                    m_isFalling = false;
                    m_isCrouching = crouching;

                    if (Mathf.Abs(localRigidbody2D.velocity.y) <= 0.1f)
                    {
                        m_isWalking = walking;
                        m_isRunning = running;
                    }
                    else
                    {
                        m_isWalking = false;
                        m_isRunning = false;
                    }
                }

                if (Mathf.Abs(direction) == 0)
                {
                    m_isRunning = false;
                    m_isWalking = false;
                }

                // Send all variables to animator
                setAnimatorVariables();
            }



            private void setAnimatorVariables()
            {
                if (m_movement.animators.Length == 0)
                {
                    return;
                }

                foreach (Animator animator in m_movement.animators)
                {
                    animator.SetBool("isGrounded", m_isGrounded);
                    animator.SetBool("isFalling", m_isFalling);
                    if (m_activation.enableJump)
                    {
                        animator.SetBool("isJumping", m_isJumping);
                    }
                    if (m_activation.enableCrouch)
                    {
                        animator.SetBool("isCrouching", m_isCrouching);
                    }
                    if (m_activation.enableWalk)
                    {
                        animator.SetBool("isWalking", m_isWalking);
                    }
                    animator.SetBool("isRunning", m_isRunning);
                    animator.SetBool("isWallSurfing", m_isWallSurfing);
                    animator.SetBool("isFacingRight", m_isFacingRight);
                }
            }
        }

        // END of Character namespace
    }

    // END of PixelCrown namespace
}