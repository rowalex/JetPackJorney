using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem trail;
    [SerializeField] Text score;
    [SerializeField] Button restart;
    [SerializeField] float upForce;
    [SerializeField] float gravity;
    private int scoreText = 0;

    bool isBursting;

    Rigidbody2D rb;

    public bool isAlive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trail.Stop();
        restart.onClick.AddListener(Restart);
    }

    private void Update()
    {
        if (isAlive)
        {
            if (Input.GetKey(KeyCode.Mouse0))
                isBursting = true;
            else
                isBursting = false;

            if (Input.GetKeyDown(KeyCode.Mouse0))
                trail.Play();
            else if (Input.GetKeyUp(KeyCode.Mouse0))
                trail.Stop();

            if (isBursting)
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = gravity;
            }
            score.text = scoreText.ToString();
        }
        else
        {
            trail.Stop() ;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            isAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            scoreText++;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Enemy")
            isAlive = false;
    }



    private void FixedUpdate()
    {
        if (isBursting) rb.AddForce(Vector2.up * upForce * Time.deltaTime);
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
