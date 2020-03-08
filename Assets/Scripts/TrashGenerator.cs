using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    [SerializeField] GameObject trashPrefab;
    public bool canGenerateTrash;
    ScoreManager scoreManager;
    public enum Side{right, left};
    public Side myside;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update() 
    {
        int actualScore = scoreManager.Score;

        if(canGenerateTrash && actualScore > 8)
        {
            int randomGenerate = Random.Range(0, 3);
            canGenerateTrash = false;
            if(randomGenerate == 0)
            {
                CancelInvoke("CreateTrash");
                InvokeRepeating("CreateTrash", 0.5f, 2f);
                int generatorSide = Random.Range(0, 2);
                if(generatorSide == 0)
                {
                    myside = Side.left;
                    this.transform.position = new Vector3(-4.5f, Camera.main.transform.position.y, 0.0f);
                } 
                else
                {
                    myside = Side.right;
                    this.transform.position = new Vector3(4.5f, Camera.main.transform.position.y, 0.0f);
                }
            }
        }
    }

    // Update is called once per frame
    void CreateTrash()
    {
        GameObject actualTrash = Instantiate(trashPrefab, this.transform.position, Quaternion.identity);
    }
}
