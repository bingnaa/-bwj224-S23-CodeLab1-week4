using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDbehaviorOG : MonoBehaviour
{
    public static WASDbehaviorOG Instance;
    
    Rigidbody2D rb2d;

    public float forceAmount = 60;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb2d.AddForce(Vector2.down * forceAmount);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(Vector2.right * forceAmount);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb2d.AddForce(Vector2.up * forceAmount);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(Vector2.left * forceAmount);
        }

        rb2d.velocity *= 0.999f;

    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("food"))
        {
            Destroy(col.gameObject);
            GameManager.Instance.slimeB++;
        }
        if (col.gameObject.CompareTag("heal"))
        {
            Destroy(col.gameObject);
            GameManager.Instance.health += 10;
        }
        if (col.gameObject.CompareTag("Finish"))
        {
            Destroy(col.gameObject);
            GameManager.Instance.health -= (20 * GameManager.Instance.Mode);
        }
        if (col.gameObject.CompareTag("diffSwitch"))
        {
            GameManager.Instance.Mode++;
        }
    }
}
