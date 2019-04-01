using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; // 0 = triple shot , 1 = speed boost, 2 = shields
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5.00f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            //enable triple shot
            if(player != null)
            {
                if(powerupID == 0)
                {
                    player.TripleShotPowerUpOn();
                }
                else if(powerupID == 1)
                {
                    player.SpeedBoostOn();
                }
                else 
                {

                }
            }

            //turn on tripple shot bool to true
            Destroy(this.gameObject);

            //handle to the component
        }



    }
}
