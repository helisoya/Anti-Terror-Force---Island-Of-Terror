using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling3D : MonoBehaviour
{
    public float speed = 1;

    public Transform start;

    public Transform end;


    void Update(){
        if(transform.position != end.position){
            transform.position = Vector3.MoveTowards(transform.position,end.position,speed*Time.deltaTime);
        }else{
            transform.position = start.position;
        }
    }
}
