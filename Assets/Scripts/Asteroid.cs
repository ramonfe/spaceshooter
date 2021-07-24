using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20f;
    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnmanager;

    private void Start()
    {
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float randomZ = Random.Range(-50, 0);
        //rotate obj on the Z axis
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    //checl for laser collision (trigger)
    //instatiate explosion at the position of the asterorid (US)
    //destroy explosion after 3 seconds
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            //destroy other
            Destroy(other.gameObject);
            _spawnmanager.StarSpawning();
            Destroy(this.gameObject, 0.25f);//us

        }
    }
}
