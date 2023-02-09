using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public int speed = 1;

    public float xScrolling = 0;

    public float yScrolling = 0;

    public new Renderer renderer;

    private Material mat;

    void Start(){
        mat = renderer.material;
    }

    void Update()
    {
        mat.mainTextureOffset += new Vector2(speed*Time.deltaTime*xScrolling,speed*Time.deltaTime*yScrolling);
        if(mat.mainTextureOffset.x <= -1){
            mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x+1,mat.mainTextureOffset.y);
        }else if(mat.mainTextureOffset.x >= 1){
            mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x-1,mat.mainTextureOffset.y);
        }
        if(mat.mainTextureOffset.y <= -1){
            mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x,mat.mainTextureOffset.y+1);
        }else if(mat.mainTextureOffset.y >= 1){
            mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x,mat.mainTextureOffset.y-1);
        }
    }
}
