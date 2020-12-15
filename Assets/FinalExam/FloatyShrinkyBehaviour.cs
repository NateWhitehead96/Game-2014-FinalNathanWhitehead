/*FloatyShrinkyBehaviour
 * Nathan Whitehead
 * 101242269
 * 12/15/20
 * Script that controls a platform that floats and shrinks
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyShrinkyBehaviour : MonoBehaviour
{
    //public Rigidbody2D rigidbody;
    public bool isActivated;
    public bool shrinking;
    public int direction;
    public int maxHeight;
    public int minHeight;

    public AudioSource shrink;
    public AudioSource expand;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    private void BobUpDown() // this function on every update will move the platform up or down based on what you set it's heights to
    {
        //transform.position = new Vector3(transform.position.x, 5 + Mathf.PingPong(Time.time, 1), 0.0f);
        transform.position = new Vector3(transform.position.x, transform.position.y + direction * Time.deltaTime, transform.position.z);

        if (transform.position.y < minHeight)
        {
            direction = 1;
        }
        if (transform.position.y > maxHeight)
        {
            direction = -1;
        }
    }

    private void Shrink() // this function makes the platform scale shrink when colliding with a player and grow when not.
    {
        if(isActivated == true)
        {
            expand.Stop();
            if(shrink.isPlaying == false)
            {
                shrink.Play();
            }
            shrinking = true;
            transform.localScale = new Vector3(transform.localScale.x - 1 * Time.deltaTime, transform.localScale.y - 1 * Time.deltaTime, 1.0f);
        }
        else if (isActivated == false && shrinking == true)
        {
            shrink.Stop();
            if(expand.isPlaying == false)
            {
                expand.Play();
            }
            if (transform.localScale.x < 2)
            {
                transform.localScale = new Vector3(transform.localScale.x + 1 * Time.deltaTime,
                    transform.localScale.y + 1 * Time.deltaTime, 1.0f);
            }
            else
            {
                shrinking = false;
            }
        }
    }

    // Update is called once per frame
    void Update() // we just call both of our functions here to ensure they run all the time
    {
        BobUpDown();
        Shrink();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // player is on the platform
            isActivated = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player is off the platform
            isActivated = false;
        }
    }
}
