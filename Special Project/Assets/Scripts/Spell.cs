using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{ 

    public GameObject spellMesh;
    public string spellName;
    public float damage;
    public float manaCost;
    public float forwardForce;
    public float upwardForce;
    public float coolDown;
    public float timeBetweenCasts;
    public float timeBetweenCasting;

}
