using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShield : MonoBehaviour
{
    [SerializeField] private bool tower1IsActive;
    [SerializeField] private bool tower2IsActive;
    [SerializeField] private bool tower3IsActive;
    [SerializeField] private bool tower4IsActive;
    [SerializeField] private TowerPower towerPower1;
    [SerializeField] private TowerPower towerPower2;
    [SerializeField] private TowerPower towerPower3;
    [SerializeField] private TowerPower towerPower4;


    // Update is called once per frame
    void Update()
    {
        tower1IsActive = !towerPower1.isActive;
        tower2IsActive = !towerPower2.isActive;
        tower3IsActive = !towerPower3.isActive;
        tower4IsActive = !towerPower4.isActive;

        if (tower1IsActive && tower2IsActive && tower3IsActive && tower4IsActive && transform.localScale.x > 0)
        {
            //gameObject.transform.localScale -= new Vector3(.5f, 0, .5f);
            gameObject.transform.localPosition -= new Vector3(0, .5f, 0);
        }
    }
}
