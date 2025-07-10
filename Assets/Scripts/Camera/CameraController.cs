using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    public float smoothtime = 0.25f;
    public float yExtra;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        offset = new Vector3(0, 0, -10);
    }
    private void Update()
    {
        Vector3 targetPos = player.position + offset;
        transform.position = targetPos;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothtime);
    }
}
