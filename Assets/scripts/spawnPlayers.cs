using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnPlayers : MonoBehaviour
{
    public GameObject plyerPrefab;
  
    public float floatminX;
    public float MaxX;
    public float minY;
    public float maxY;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomposition = new Vector3(Random.Range(floatminX,MaxX),transform.position.y ,Random.Range(minY,maxY));
        PhotonNetwork.Instantiate(plyerPrefab.name, randomposition, Quaternion.identity);
       
         }

    // Update is called once per frame
    void Update()
    {
        
    }
}
