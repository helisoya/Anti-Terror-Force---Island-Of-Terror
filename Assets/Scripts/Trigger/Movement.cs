using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform[] positions;
    public bool loop = false;

    public bool rotateTowardTarget = true;

    public float speed = 1;

    private int current = 0;


    void Update(){

        if(transform.position!=positions[current].position){
            transform.position = Vector3.MoveTowards(transform.position,positions[current].position,speed*Time.deltaTime);
            if(rotateTowardTarget){
                transform.LookAt(positions[current]);
            }
        }else{
            if(current==positions.Length-1){
                if(loop){
                    current = 0;
                }else{
                    transform.rotation = positions[current].rotation;
                    this.enabled = false;
                }
            }else{
                current++;
            }
        }
    }
}
