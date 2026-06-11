using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
  public float  speed = 5f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [System.Obsolete]
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity =  new Vector2(horizontalInput * speed, rb.velocity.y);

        if (rb.velocity.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX=true;
        if (rb.velocity.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.name.Contains("DeadZone"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}