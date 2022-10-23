using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] playerPosition;
    [SerializeField] private PlayerMovement playerMoveScript;

    void Start()
    {
        player = gameObject;
    }

    void Update()
    {
        if (player.transform.position == playerPosition[0].position)
        {
            playerMoveScript.AddTween(player.transform, playerPosition[0].position, playerPosition[1].position, 1.5f);
        }
        else if (player.transform.position == playerPosition[1].position)
        {
            playerMoveScript.AddTween(player.transform, playerPosition[1].position, playerPosition[2].position, 1.5f);
        }
        else if (player.transform.position == playerPosition[2].position)
        {
            playerMoveScript.AddTween(player.transform, playerPosition[2].position, playerPosition[3].position, 1.5f);
        }
        else if (player.transform.position == playerPosition[3].position)
        {
            playerMoveScript.AddTween(player.transform, playerPosition[3].position, playerPosition[0].position, 1.5f);
        }
    }
}
