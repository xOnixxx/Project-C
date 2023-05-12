using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SmoothMove(Func<float, float, float, float> moveFun, Vector3 target, int numOfIt)
    {
        StartCoroutine(SmoothMoveC(moveFun, transform.localPosition, target, numOfIt));
    }
    
    public IEnumerator SmoothMoveC(Func<float, float, float, float> moveFun, Vector3 origin, Vector3 target, int numOfIt)
    {
        float ox = origin.x;
        float oy = origin.y;
        float oz = origin.z;

        float tx = target.x;
        float ty = target.y;
        float tz = target.z;

        float dx = tx - ox;
        float dy = ty - oy; 
        float dz = tz - oz;

        float px = ox / numOfIt;//(dx / numOfIt);
        float py = oy / numOfIt;//(dy / numOfIt);
        float pz = (dz / numOfIt);

        for (float i = 0; i <= numOfIt; i++)
        {
            /*
            ox += moveFun(px);
            oy += moveFun(py);
            oz += moveFun(pz);
            */
            //Debug.Log(transform.localPosition);
            //Debug.Log(moveFun(i/numOfIt, ox, dx) + " "  +moveFun(i/numOfIt, oy, dy) + " " + moveFun(i/numOfIt, oz, dz) + " " + (i/numOfIt));
            transform.localPosition = new Vector3(moveFun(i / numOfIt, ox, dx) , moveFun(i / numOfIt, oy, dy) , moveFun(i / numOfIt, oz, dz));

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SmoothSize(Func<float, float, float, float> moveFun, Vector3 target, int numOfIt)
    {
        StartCoroutine(SmoothSizeC(moveFun, transform.localPosition, target, numOfIt));
    }

    public IEnumerator SmoothSizeC(Func<float, float, float, float> moveFun, Vector3 origin, Vector3 target, int numOfIt)
    {
        float ox = origin.x;
        float oy = origin.y;
        float oz = origin.z;

        float tx = target.x;
        float ty = target.y;
        float tz = target.z;

        float dx = tx - ox;
        float dy = ty - oy;
        float dz = tz - oz;

        float px = ox / numOfIt;//(dx / numOfIt);
        float py = oy / numOfIt;//(dy / numOfIt);
        float pz = (dz / numOfIt);

        for (float i = 0; i <= numOfIt; i++)
        {
            /*
            ox += moveFun(px);
            oy += moveFun(py);
            oz += moveFun(pz);
            */
            //Debug.Log(transform.localPosition);
            //Debug.Log(moveFun(i/numOfIt, ox, dx) + " "  +moveFun(i/numOfIt, oy, dy) + " " + moveFun(i/numOfIt, oz, dz) + " " + (i/numOfIt));
            transform.localScale = new Vector3(moveFun(i / numOfIt, ox, dx), moveFun(i / numOfIt, oy, dy), moveFun(i / numOfIt, oz, dz));

            yield return new WaitForSeconds(0.01f);
        }
    }
}
