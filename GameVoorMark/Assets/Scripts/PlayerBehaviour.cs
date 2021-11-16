using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector]
    public int score;

    public float rotateSpeed;
    public float thrust;

    private bool isAttacking;

    public GameObject sprite;
    public GameObject particles;

    private Rigidbody2D rb;

    private void Start()
    {
        isAttacking = false;

        particles.SetActive(false);

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.E) && !isAttacking)
        {
            sprite.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

            particles.SetActive(true);

            thrust++;
        }

        if (Input.GetKeyUp(KeyCode.E) && !isAttacking && thrust > 50)
        {
            rb.AddForce(Vector2.right * h * thrust * 4);
            rb.AddForce(Vector2.up * v * thrust * 4);

            isAttacking = true;
        }

        if (gameObject.transform.position.x > 15 || gameObject.transform.position.x < -15)
        {
            SceneManager.LoadScene(0);
        }
        
        if (gameObject.transform.position.y > 15 || gameObject.transform.position.y < -15)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && isAttacking)
        {
            isAttacking = false;

            rb.velocity = Vector2.zero;
            transform.Rotate(0, 0, 0);

            particles.SetActive(false);

            Destroy(collision.gameObject);

            thrust = 0;

            score++;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
