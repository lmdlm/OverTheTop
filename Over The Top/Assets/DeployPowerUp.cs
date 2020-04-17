using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployPowerUp : MonoBehaviour
{

    public GameObject heartPrefab;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHeart());
    }
    
    IEnumerator SpawnHeart()
    {
        while (true)
        {
            yield return new WaitForSeconds(40.0f);
            CreateHeart();
        }
    }

    private void CreateHeart()
    {
        GameObject heart = Instantiate(heartPrefab) as GameObject;
        heart.transform.position = new Vector2(0.0f, 5.0f);
    }
}
