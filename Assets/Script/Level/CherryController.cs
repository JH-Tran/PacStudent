using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject cherry;
    [SerializeField] GameObject originObject;
    private Vector3 originPosition;
    public Vector3 randomPosition;

    public float horizontalMin;
    public float horizontalMax;
    public float verticalMin;
    public float verticalMax;
    public int sideNum;

    private float spriteSizeError = 1;
    private float spawnTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        verticalMax = cam.orthographicSize + spriteSizeError;
        verticalMin = -cam.orthographicSize - spriteSizeError;
        horizontalMax = cam.aspect * cam.orthographicSize + spriteSizeError;
        horizontalMin = -cam.aspect * cam.orthographicSize - spriteSizeError;
        if(originObject != null)
        {
            originPosition = originObject.transform.position;
        }
/*        Debug.Log("hMax: " + horizontalMax);
        Debug.Log("hMin: " + horizontalMin);
        Debug.Log("vMax: " + verticalMax);
        Debug.Log("vMin: " + verticalMin);*/
        InvokeRepeating("spawnCherry", spawnTime, spawnTime);
    }

    private void spawnCherry()
    {
        // 0 = top, 1 = left, 2 = bottom, 3 = right
        sideNum = Random.Range(0,4);
        randomPosition = originPosition + new Vector3(horizontalMax, verticalMax, 0f);
        switch (sideNum)
        {
            case 0:
                Debug.Log("Top");
                randomPosition = originPosition + new Vector3(Random.Range(horizontalMin, horizontalMax), verticalMax, 0f);
                break;
            case 1:
                Debug.Log("Left");
                randomPosition = originPosition + new Vector3(horizontalMax, Random.Range(verticalMin, horizontalMax), 0f);
                break;
            case 2:
                Debug.Log("Bottom");
                randomPosition = originPosition + new Vector3(Random.Range(horizontalMin, horizontalMax), verticalMin, 0f);
                break;
            case 3:
                Debug.Log("Right");
                randomPosition = originPosition + new Vector3(horizontalMin, Random.Range(verticalMin, horizontalMax), 0f);
                break;
        }
        Instantiate(cherry, randomPosition, Quaternion.identity);
    }
}
