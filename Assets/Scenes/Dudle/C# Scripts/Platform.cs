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
        if(collision.gameObject.name.Contains("DeadZone"))
        {
         float randX = Random.Range(-2, 2);
            float randY = Random.Range(transform.position.y + 16f,transform.position.y +16.5f);
            transform.position = new Vector3(randX, randY, 0);
        }
    }
   

}
