using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Motor : MonoBehaviour
{
   public Transform lookAt;

   //public float boundX = 0.15f;
   //public float boundY = 0.15f;
   public float offsetX = 0f;
   public float offsetY = 500f;
   private void LateUpdate() {
    //    Vector3 delta = Vector3.zero;

    //     //Check if we're inside the bound on the X axis
    //    float deltaX = lookAt.position.x - transform.position.x;

    //    if(deltaX > boundX || deltaX < -boundX)
    //    {
    //        if( transform.position.x < lookAt.position.x )
    //        {
    //             deltaX = deltaX - boundX;
    //        }
    //        else{
    //            deltaX = deltaX + boundX;
    //        }
    //    }

    //      //Check if we're inside the bound on the Y aYis
    //    float deltaY = lookAt.position.y - transform.position.y;

    //    if(deltaY > boundY || deltaY < -boundY)
    
    //    {
    //        if( transform.position.y < lookAt.position.y )
    //        {
    //             deltaY = deltaY - boundY;
    //        }
    //        else{
    //            deltaY = deltaY + boundY;
    //        }
    //    }

    //    transform.position +=  new Vector3(delta.x, delta.y, 0);
    //    Debug.Log("DeltaX: " + deltaX);
    //    Debug.Log("DeltaY: " + deltaY);
    //    Debug.Log("boundX: " + boundX);
    //    Debug.Log("boundY: " + boundY);
     transform.position = new Vector3(  lookAt.position.x+ offsetX,  lookAt.position.y + offsetY,  0);
   }

}
