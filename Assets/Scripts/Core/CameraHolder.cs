using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 initialRotation;

    // Start is called before the first frame update
    void Awake()
    {
        initialRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position;

        transform.eulerAngles = new Vector3(player.transform.eulerAngles.x + initialRotation.x, 
            player.transform.eulerAngles.y + initialRotation.y, 0);
    }
}
