using System;
using UnityEngine;


namespace PixelCrown
{
    namespace Character
    {
        // Control structure
        [Serializable]
        public class Control
        {
            [Tooltip("Enable character to be controlled by the player input")]
            public bool enableControl = true;

            [Tooltip("Jump button identifier")]
            public string controlJump = "Jump";

            [Tooltip("Crouch button identifier")]
            public string controlCrouch = "Crouch";

            [Tooltip("Walk toggle button identifier")]
            public string controlWalkToggle = "WalkToggle";

            [Tooltip("Horizontal axis identifier")]
            public string controlAxisHorizontal = "Horizontal";
        }


        public class PlayerController2D : CharacterController2D
        {

            [SerializeField]
            public PixelCrown.Character.Control control;


            // True if the jump button is pressed
            private bool pressedJump = false;

            // True if the crouch button is pressed
            private bool pressedCrouch = false;

            // True toggle to run or walk
            // Toggle to walk when enableAlwaysRun is true, toggle to run when enableAlwaysRun is false
            private bool pressedWalkToggle = false;



            private float longJumpEndTime = 0.0f;
            private bool releaseJump = true;
            private float releaseJumpTime = 0.0f;
            private int doubleJumpCount = 0;
            private float jumpStartTime = 0.0f;
            private float dustEndTime = 0.0f;
            private bool canDoubleJump = false;
            private bool canWallJump = false;
            
            // horizontal direction
            private float horizontalMove = 0f;


            private void FixedUpdate()
            {
                CheckControls();

                // Move the character
                Move(horizontalMove, pressedCrouch, pressedJump, pressedWalkToggle);
                lastPosition = transform.position;
            }

            private void CheckControls()
            {
                if (!control.enableControl)
                {
                    return;
                }

                pressedJump = false;
                pressedWalkToggle = false;

                if (control.controlAxisHorizontal != "")
                {
                    horizontalMove = Input.GetAxisRaw(control.controlAxisHorizontal);
                }

                if (m_activation.enableWalk && control.controlWalkToggle != "" && Input.GetButton(control.controlWalkToggle))
                {
                    pressedWalkToggle = true;
                }

                if (m_activation.enableJump && control.controlJump != "" && Input.GetButton(control.controlJump))
                {
                    pressedJump = true;
                }

                if (m_activation.enableCrouch && control.controlCrouch != "")
                {
                    if (Input.GetButton(control.controlCrouch))
                    {
                        pressedCrouch = true;
                    }
                    else
                    {
                        pressedCrouch = false;
                    }
                }
                else
                {
                    pressedCrouch = false;
                }

            }




            private void Flip()
            {
                // Switch character facing
                m_isFacingRight = !m_isFacingRight;
                transform.Rotate(0f, 180f, 0f);
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