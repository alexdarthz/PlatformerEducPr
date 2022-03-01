using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Vector3 pos;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        pos = player.transform.position;
        pos.y += 2f;
        pos.z = -20f;
        this.transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
    }
}
