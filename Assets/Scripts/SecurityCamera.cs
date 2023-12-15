using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private float lookInterval = 0.1f;
    [Range(30,110)]
    [SerializeField] private float fieldOfView = 75;
    private Transform emitter;
    private GameObject player;
    private bool canSeePlayer;

    void Start()
    {
        emitter = this.transform.GetChild(0);
        player = GameObject.Find("FirstPersonController");
        StartCoroutine(CheckForPlayer());
    }

    IEnumerator CheckForPlayer() 
    {
        while(true) 
        {
            yield return new WaitForSeconds(lookInterval);
            {
                Ray ray = new Ray(emitter.position, player.transform.position - emitter.position); // draw a ray from the emitter to enemy
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 100)) 
                {
                    if(hit.transform.gameObject.CompareTag("Player")) 
                    {
                        Vector3 targetDir = player.transform.position - emitter.position;
                        float angle = Vector3.Angle(targetDir, emitter.forward);

                        if (angle < fieldOfView) 
                        {
                            Debug.Log("Found player.");
                            // turn all of the turrets on.
                            StartCoroutine(CallTurrets());
                            Debug.DrawRay(emitter.position, player.transform.position - emitter.position, Color.green, 4);
                        } 
                        else 
                        {
                            canSeePlayer = false;
                            Debug.DrawRay(emitter.position, player.transform.position - emitter.position, Color.yellow, 4);
                        }
                        
                    } 
                    else 
                    {
                        Debug.DrawRay(emitter.position, player.transform.position - emitter.position, Color.red, 4);
                        canSeePlayer = false;
                    }

                }
            }
        }
    }
    IEnumerator CallTurrets() {
        Debug.Log("Calling Turret function.");
        if(canSeePlayer == false) {
            canSeePlayer = true;
            yield return new WaitForSeconds(1);
            //if we can still see the player one second later.
            if(canSeePlayer) {
                // find all of the turrets, and turn them on.
                GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
                foreach(GameObject turret in turrets) {
                    turret.GetComponent<TurretController>().Activate();
                }
            }
        }
        // else don't run this code.

    }
}