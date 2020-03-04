using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    float velocity;
    TrashGenerator trashGenerator;
    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        var euler = transform.eulerAngles;
        euler.z = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = euler;
        velocity = Random.Range(5f, 8f);
        trashGenerator = FindObjectOfType<TrashGenerator>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trashGenerator.myside == TrashGenerator.Side.right)
        {
            transform.Translate(Vector3.left * Time.deltaTime * velocity, Space.World);
            if(transform.position.x < -5f)
            {
                Destroy(gameObject);
            }
        }
        else if(trashGenerator.myside == TrashGenerator.Side.left)
        {
            transform.Translate(Vector3.right * Time.deltaTime * velocity, Space.World);
            if(transform.position.x > 5f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ship")
        {
            scoreManager.EndGame();
        }
    }
}
