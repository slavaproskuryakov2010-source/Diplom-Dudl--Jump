using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    public float forceJump;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (collision.relativeVelocity.y < 0) 
            {
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * forceJump;
            }
        }
    }
   

}
