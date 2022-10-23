using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBorder : MonoBehaviour
{
    public Transform[] borderCorners = new Transform[4];
    public Animator directionAnimator;
    private float speed = 2f;
    private bool isAtCorner = false;
    private int cornerNum = 1;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (!isAtCorner)
        {
            gameObjectMove(cornerNum);
        }
        else
        {
            if (cornerNum == 3)
                cornerNum = 0;
            else
                cornerNum++;
            directionAnimator.SetInteger("aniNum",cornerNum);
            isAtCorner = false;
        }
    }

    private void gameObjectMove(int cornerNum)
    {
        if (Vector3.Distance(gameObject.transform.position, borderCorners[cornerNum].transform.position) > 0.1f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                                                      borderCorners[cornerNum].transform.position,
                                                       speed);
        }
        else
        {
            gameObject.transform.position = borderCorners[cornerNum].transform.position;
            isAtCorner = true;
        }
    }
}
