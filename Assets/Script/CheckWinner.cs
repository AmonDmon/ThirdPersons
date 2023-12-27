using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    
    
    public Camera defaultCamera;
    public Camera winnerCamera;
    public bool isWinner = false;

    public Transform target;
    public float smoothSpeed = 1.0f;

    public static CheckWinner instance;

    public Transform playerRotation;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultCamera.enabled = true;
        winnerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWinner)
        {
            defaultCamera.enabled = false;
            winnerCamera.enabled = true;
        }
    }

    private void LateUpdate()
    {
        if(target != null && isWinner)
        {
            Vector3 desiredPosirion = new Vector3(target.position.x+4.0f, target.position.y+2.2f, target.position.z + 2.6f);

            Vector3 smoothedPosition = Vector3.Lerp(winnerCamera.transform.position, desiredPosirion
                                        , smoothSpeed * Time.deltaTime);
            winnerCamera.transform.position = smoothedPosition;

            playerRotation.LookAt(new Vector3(playerRotation.position.x+1.0f, playerRotation.position.y, playerRotation.position.z));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && PlayerControler.instance.groundedPlayer) 
        {
            isWinner = true;
        }
    }
}
