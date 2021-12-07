
using UnityEngine;

public class CrouchingState : PlayerBaseState
{

    public CrouchingState(PlayerStateManager player) : base(player){}

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        Crouch();
    }

    // Crouch ability
    bool pullup = false;

    RaycastHit hitInfo;
    void Crouch()
    {
        // Check if the player has an object above them
        Player.isUnderObject = Physics.SphereCast( Player.trans.position - (Vector3.up * 1f), .5f, Vector3.up, out hitInfo, 1f);

        // If the player is holding the crouch key:
        if (Player.isCrouching)
        {
            Player.stand_collision.enabled = false;
            Player.crouch_collision.enabled = true;

            // If the player wants to crouch while in midair or crouch-jump:
            if (Player.isPlayerGrounded == false && pullup == false && Player.rigid.velocity.y > 0f)
            {   
                float pullforce = Mathf.Sqrt(2 * (.5f+Player.jumpHeight) * Player.aerialGravity);

                float currentYvel = Player.rigid.velocity.y;
                float trajectory = Mathf.Pow(pullforce - currentYvel, 2) / (2*Player.aerialGravity);

                Player.rigid.AddForce(trajectory * Vector3.up, ForceMode.Impulse);

                pullup = true;
            }

            // Set Camera to crouch height
            SetCameraHeight(Player.cameraCrouchPos);

            // Non-kosher way of manipulating other states...
            // Set player movement speed to crouch speed
            Player.standState.playerMoveSpeed = Player.crouchSpeed;
        }
        else
        {
            if (Player.isUnderObject == false)
            {
                Player.stand_collision.enabled = true;
                Player.crouch_collision.enabled = false;

                // Set Camera to stand height
                SetCameraHeight(Player.cameraStandPos);

                // Set player speed back to walking speed.
                Player.standState.playerMoveSpeed = Player.walkSpeed;
            }
        }

        Debug.Log(Player.isUnderObject);

        // If the player has landed on solid ground...
        if (Player.isPlayerGrounded)
        {
            pullup = false;
        }
    }


    float refCamVel = 0f;
    void SetCameraHeight(float height)
    {
        Player.PlayerCamera.localPosition = Vector3.up * Mathf.SmoothDamp(
            Player.PlayerCamera.localPosition.y,
            height,
            ref refCamVel,
            5 * Time.deltaTime
        );
    }
}
