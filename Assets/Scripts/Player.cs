using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    SpawnManager _spawnManager;

    [SerializeField]
    bool _isTrippleShotActivated = false;

    bool _isShieldActivated = false;

    [SerializeField]
    private GameObject _shieldsObj;

    [SerializeField]
    private int _score;

    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    private UIManager _uIManager;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is null");
        }

        if (_uIManager == null)
        {
            Debug.LogError("The UI Manager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(transform.position.y, -10, 0), 0);

        if (transform.position.x >= 16)
        {
            transform.position = new Vector3(-16, transform.position.y, 0);
        }

        else if (transform.position.x <= -16)
        {
            transform.position = new Vector3(16, transform.position.y, 0);
        }
    }
    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTrippleShotActivated)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

    }
    public void Damage()
    {
        if (_isShieldActivated)
        {
            _isShieldActivated = false;
            _shieldsObj.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        } 

        _uIManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActivate()
    {
        _isTrippleShotActivated = true;
        StartCoroutine(TripleShotPowerupRoutine());
    }
    public void SpeedUpActivated()
    {
        _speed = 10f;
        StartCoroutine(SpeedUpRoutine());
    }
    public void ShieldActivated()
    {
        _shieldsObj.SetActive(true);
        _isShieldActivated = true;
    }
    IEnumerator SpeedUpRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speed = 3.5f;
    }
    IEnumerator TripleShotPowerupRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTrippleShotActivated = false;
    }
    //methos to add 10 to score
    //comunicate with ui to update the score
    public void AddScore(int points)
    {
        _score += points;
        _uIManager.UpdateScore(_score);
    }
}
