using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField] private GameObject flockPrefap;
    [Range(50, 200)] public int flockSize;
    [SerializeField] private Vector3 spawnBounds;

    public GameObject[] allUnits { get; set; }

    void Start()
    {
        GenerateUnits();
    }

    private void GenerateUnits()
    {
        allUnits = new GameObject[flockSize];
        for(int i = 0; i< flockSize;i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
        }
    }
}
