using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager;
    public int targetScore;
    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;
    private float minForce = 12.0f;
    private float maxForce = 16.0f;
    private float torqueRange = 10.0f;
    private float xRange = 4.0f;
    private float yPos = -2.0f;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        
        transform.position = SpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown() 
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            gameManager.UpdateScore(targetScore);
        }
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
        if(!CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    float RandomTorque()
    {
        return Random.Range(-torqueRange,torqueRange);
    }

    Vector3 SpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), yPos);
    }
}
