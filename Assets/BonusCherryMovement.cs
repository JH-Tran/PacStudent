using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCherryMovement : MonoBehaviour
{
    private GameObject origin;
    private CherryController cherryController;

    private Vector3 cameraPosition;
    private Vector3 endPosition;
    private Vector3 startPosition;

    private float constant;
    private float duration = 20;
    private float gradient;
    private float timeTaken;
    private bool isMovementHorizontal;

    void Awake()
    {
        cherryController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CherryController>();
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position;
        origin = GameObject.Find("Centre");
        Debug.Log("Origin Position: " + origin.transform.position);
        startPosition = cherryController.randomPosition;

        gradient = (origin.transform.position.y - startPosition.y) / (origin.transform.position.x - startPosition.x);
        constant = origin.transform.position.y - (gradient * origin.transform.position.x);

        if (cherryController.sideNum == 0)
        {
            endPosition.y = cherryController.verticalMin + cameraPosition.y;
            endPosition.x = (endPosition.y - constant) / gradient;
        }
        else if (cherryController.sideNum == 1)
        {
            endPosition.x = cherryController.horizontalMin + cameraPosition.x;
            endPosition.y = (endPosition.x * gradient) + constant;
        }
        else if (cherryController.sideNum == 2)
        {
            endPosition.y = cherryController.verticalMax + cameraPosition.y;
            endPosition.x = (endPosition.y - constant) / gradient;
        }
        else if (cherryController.sideNum == 3)
        {
            endPosition.x = cherryController.horizontalMax + cameraPosition.x;
            endPosition.y = (endPosition.x * gradient) + constant;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, endPosition) > 0.01)
        {
            timeTaken += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, timeTaken / duration);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
