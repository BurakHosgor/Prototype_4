using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody playerRb;

    private GameObject focalPoint;
    public bool hasPowerUp;
    private float powerUpStrenght = 15.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward*speed*forwardInput);
        powerupIndicator.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody =collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer*powerUpStrenght,ForceMode.Impulse);
            Debug.Log("Collided with"+collision.gameObject.name);
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp=false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
