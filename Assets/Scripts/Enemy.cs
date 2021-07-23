using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 4;

    private Player _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -5)
        {
            float randomx = Random.Range(-8f, 8f);
            transform.Translate(new Vector3(randomx, 7f, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        if (other.tag == "Player")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            //destroy us
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            //destroy laser
            Destroy(other.gameObject);

            //add 10 score
            if(_player != null)
            {
                _player.AddScore(10);
            }           
            //destroy us
            Destroy(this.gameObject);            
        }

    }
}
