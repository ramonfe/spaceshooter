﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float _speedVariable = 3.0f;
    [SerializeField]
    private int _powerupID;

    [SerializeField]
    private AudioClip _audioClip;

     

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speedVariable * Time.deltaTime);

        if (transform.position.y <= -4.5f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {            
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActivate();
                        break;
                    case 1:
                        player.SpeedUpActivated();
                        break;
                    case 2:
                        player.ShieldActivated();
                        break;
                    default:
                        Debug.Log("Shield collected");
                        break;
                }  
            }

            Destroy(this.gameObject);

        }
    }
}
