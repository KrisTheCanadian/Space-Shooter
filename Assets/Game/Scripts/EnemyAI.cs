using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private float _speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckBounds();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void CheckBounds()
    {

        if (transform.position.y < -5f)
        {
            Respawn();
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

    private void Respawn()
    {
        float randomX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomX, 7.42f, 0);

    }

    private void Explode()
    {
        GameObject clone = Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(clone, 3f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Explode();
        }

        if (other.tag == "laser")
        {
            GameObject laser = other.GetComponent<GameObject>();

            if (laser != null)
            {
                Destroy(laser);

            }


            Explode();
            
            
        }
    }
}
