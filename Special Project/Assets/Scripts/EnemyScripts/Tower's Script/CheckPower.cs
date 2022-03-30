using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPower : MonoBehaviour
{
    [SerializeField] private TowerPower TowerPower1;
    [SerializeField] private bool tower1isPowered;
    [SerializeField] private TowerPower TowerPower2;
    [SerializeField] private bool tower2isPowered;
    [SerializeField] private TowerPower TowerPower3;
    [SerializeField] private bool tower3isPowered;
    [SerializeField] private TowerPower TowerPower4;
    [SerializeField] private bool tower4isPowered;
    public bool allTurretsPowered;

    // Start is called before the first frame update
    void Start()
    {/*
        tower1isPowered = true;
        tower2isPowered = true;
        tower3isPowered = true;
        tower4isPowered = true;*/
    }

    // Update is called once per frame
    void Update()
    {
        tower1isPowered = TowerPower1.isPowered;
        tower2isPowered = TowerPower2.isPowered;
        tower3isPowered = TowerPower3.isPowered;
        tower4isPowered = TowerPower4.isPowered;
        allTurretsPowered = tower1isPowered && tower2isPowered && tower3isPowered && tower4isPowered;
    }
}
