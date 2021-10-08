using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    //player
    public GameObject Player;

    //player position for the camera
    public float maxY;
    public float maxX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //camera movement
        Vector3 newCameraPos = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        if(newCameraPos.x < 0)
        {
            newCameraPos.x = 0;
        }

        if(newCameraPos.x > maxX)
        {
            newCameraPos.x = maxX;
        }

        if (newCameraPos.y < 0)
        {
            newCameraPos.y = 0;
        }

        if (newCameraPos.y > maxY)
        {
            newCameraPos.y = maxY;
        }

        transform.position = Vector3.Lerp(transform.position, newCameraPos, 0.05f);
    }
}
