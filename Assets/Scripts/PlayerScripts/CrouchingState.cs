
using UnityEngine;

public class CrouchingState : PlayerBaseState
{

    public CrouchingState(PlayerStateManager player) : base(player){}

    float crouchSlider = 0f;

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        Crouch();
    }

    // Crouch ability
    bool pullup = false;
    void Crouch()
    {
        if (Player.isCrouching)
        {
            Player.stand_collision.enabled = false;
            Player.crouch_collision.enabled = true;

            // If the player wants to crouch while in midair or crouch-jump:
            if (Player.isPlayerGrounded == false && pullup == false)
            {   
                float pullforce = Mathf.Sqrt(2 * (1f+Player.jumpHeight) * Player.aerialGravity);

                float currentYvel = Player.rigid.velocity.y;
                float trajectory = Mathf.Pow(pullforce - currentYvel, 2) / (2*Player.aerialGravity);

                Player.rigid.AddForce(trajectory * Vector3.up, ForceMode.Impulse);

                pullup = true;
            }

            // Set Camera to crouch height
            SetCameraHeight(Player.cameraCrouchPos);
        }
        else
        {
            Player.stand_collision.enabled = true;
            Player.crouch_collision.enabled = false;

            // Set Camera to stand height
            SetCameraHeight(Player.cameraStandPos);
        }

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
