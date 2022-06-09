using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieCameraControl : MonoBehaviour
{
    public PlayerControl player;
    public CompanionControl companion;
    void Start()
    {
        
    }
    void Update()
    {
        float cameraPositionX = (player.transform.position.x + companion.transform.position.x) / 2;
        float cameraPositionY = (player.transform.position.y + companion.transform.position.y) / 2 + 10;
        float cameraPositionZ = (player.transform.position.z + companion.transform.position.z) / 2 - 7.5f;
        transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ);
    }
}