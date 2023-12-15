using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    [SerializeField] private ShipCamera shipCam;
    private Movement shipMove;
    private WreckingBall wreckingBall; // Reference to the WreckingBall script

    // Start is called before the first frame update
    void Start()
    {
        if (shipCam == null) shipCam = this.gameObject.GetComponent<ShipCamera>();
        shipMove = this.GetComponent<Movement>();
        wreckingBall = GetComponentInChildren<WreckingBall>(); // Assuming WreckingBall is a child object
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) shipCam.ZoomOut();
        if (Input.GetKeyUp(KeyCode.Mouse1)) shipCam.DefaultZoom();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Check if the WreckingBall is ready to launch
            if (wreckingBall != null && wreckingBall.readyToLaunch)
            {
                wreckingBall.Launch();
            }
        }
    }

    void FixedUpdate()
    {
        shipMove.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }
}
