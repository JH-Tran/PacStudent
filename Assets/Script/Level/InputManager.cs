using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private string currentInput = "";
    [SerializeField] private string lastInput = "";
    [SerializeField] private GameObject player;
    [SerializeField] private PacStudentController playerMoveScript;

    private RaycastHit2D raycastLeftHit, raycastRightHit, raycastUpHit, raycastDownHit;
    private bool hitWall;
    private float timeTaken = .5f;
    private bool hitWallAudio = false;

    void Start()
    {
        player = gameObject;
    }

    void Update()
    {
        //Input for when player moves
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = "W";
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = "A";
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = "S";
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = "D";
        }
        // Direction change of player
        if (lastInput != null && !playerMoveScript.isLerping && currentInput != lastInput)
        {
            raycastHitWall(lastInput);
            if (!hitWall)
            {
                currentInput = lastInput;
                hitWallAudio = false;
            }
            else
            {
                if (hitWallAudio == false && lastInput.Equals(currentInput))
                {
                    playerMoveScript.hitWallAudio();
                    hitWallAudio = true;
                }
            }
        }
        // Player continues to moves if there is no lerp, collision,
        if (currentInput != null && !playerMoveScript.isLerping)
        {
            raycastHitWall(currentInput);
            if (!hitWall)
            {
                movePlayer();
                hitWallAudio = false;
            }
            else
            {
                if (hitWallAudio == false && lastInput.Equals(currentInput))
                {
                    playerMoveScript.hitWallAudio();
                    hitWallAudio = true;
                }
            }
        }
    }

    private void movePlayer()
    {
        if (currentInput.Equals("W"))
        {
            playerMoveScript.AddTween(player.transform, player.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1), timeTaken);
        }
        else if (currentInput.Equals("A"))
        {
            playerMoveScript.AddTween(player.transform, player.transform.position, new Vector3(player.transform.position.x - 1, player.transform.position.y), timeTaken);
        }
        else if (currentInput.Equals("S"))
        {
            playerMoveScript.AddTween(player.transform, player.transform.position, new Vector3(player.transform.position.x, player.transform.position.y - 1), timeTaken);
        }
        else if (currentInput.Equals("D"))
        {
            playerMoveScript.AddTween(player.transform, player.transform.position, new Vector3(player.transform.position.x + 1, player.transform.position.y), timeTaken);
        }
    }
    private void raycastHitWall(string input)
    {
        if (input.Equals("W"))
        {
            raycastUpHit = Physics2D.Raycast(transform.position, Vector2.up);
            hitWall = playerMoveScript.isRaycastHit(raycastUpHit);
            Debug.DrawRay(transform.position, Vector2.up);
        }
        else if (input.Equals("A"))
        {
            raycastLeftHit = Physics2D.Raycast(transform.position, -Vector2.right);
            hitWall = playerMoveScript.isRaycastHit(raycastLeftHit);
            Debug.DrawRay(transform.position, -Vector2.right);
        }
        else if (input.Equals("S"))
        {
            raycastDownHit = Physics2D.Raycast(transform.position, -Vector2.up);
            hitWall = playerMoveScript.isRaycastHit(raycastDownHit);
            Debug.DrawRay(transform.position, -Vector2.up);
        }
        else if (input.Equals("D"))
        {
            raycastRightHit = Physics2D.Raycast(transform.position, Vector2.right);
            hitWall = playerMoveScript.isRaycastHit(raycastRightHit);
            Debug.DrawRay(transform.position, Vector2.right);
        }

    }

}
