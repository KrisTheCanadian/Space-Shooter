using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerExplosion;
    public bool canTripleShot = false;
    public int lives = 3;



    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    

    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    
    [SerializeField]
    private float _speed = 5.0f;



    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        
    }

    private void Update()
    {

        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {

            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);

            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 1.03f, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;

        }


    }

    public void Damage()
    {
        
        if(lives > 0)
        {
            lives--;
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject clone = Instantiate(_playerExplosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(clone, 3f);
    }



    private void Movement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);



        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.55f)
        {
            transform.position = new Vector3(transform.position.x, -4.55f, 0);
        }

        if (transform.position.x > 9.45f)
        {
            transform.position = new Vector3(-9.44f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.45f)
        {
            transform.position = new Vector3(9.44f, transform.position.y, 0);
        }
    }
    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;
    }

    public void SpeedBoostOn()
    {
        _speed = _speed + 5f;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        _speed = _speed - 5f;
    }
}
