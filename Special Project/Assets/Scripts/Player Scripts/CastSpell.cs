using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CastSpell : MonoBehaviour
{

    public Spell[] spell;
    private int index;
    public GameObject spellEffects;

    public Camera playerCam;
    public Transform attackPoint;

    //Gameplay Variables
    public float manaTotal, currentMana, rechargeTime, manaRegen;
    private float spellsCasted;
    private bool readyToCast, allowInvoke, shooting, reloading, readyToShoot;

    //UI Elements
    public TextMeshProUGUI manaDisplay;
    public Slider slider;



    // Start is called before the first frame update
    void Awake()
    {
        currentMana = manaTotal;
        readyToShoot = true;
        allowInvoke = true;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        castSpell();
        changeSpell();

        if (manaDisplay.text != null)
        {
            manaDisplay.text = (currentMana + " / " + manaTotal);
            slider.value = (currentMana / manaTotal);
        }

        if (currentMana > manaTotal)
        {
            currentMana = manaTotal;
        }
    }

    private void castSpell()
    {
        // Check to see if player can hold and fire
        //shooting = Input.GetKeyDown(KeyCode.Mouse0);
        shooting = Input.GetKey(KeyCode.Mouse0);


        // Recharging
        if (Input.GetKeyDown(KeyCode.R) && currentMana < manaTotal && !reloading)
        {
            StartCoroutine(Recharge());
            //Recharge();
        }

        if (readyToShoot && shooting && !reloading && currentMana <= 0)
        {
            StartCoroutine(Recharge());
            //Recharge();
        }

        // Shooting
        if (readyToShoot && shooting && !reloading && currentMana > 0 && currentMana > spell[index].manaCost)
        {
            spellsCasted = 0;

            Casting();
        }
    }

    private void changeSpell()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            index++;
            if (index >= spell.Length)
            {
                index = 0;
            }
            allowInvoke = true;
            readyToCast = true;
            Debug.Log("Selected " + spell[index].name);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            index--;
            if (index < 0)
            {
                index = spell.Length - 1;
            }
            allowInvoke = true;
            readyToCast = true;
            Debug.Log("Selected " + spell[index].name);
        }
    }

    private void Casting()
    {
        //Debug.Log("Casting " + spell[index].name);
        readyToShoot = false;

        // Find target point of projectile
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        // Calculating direction from attack to target
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // Add spread later on

        // Calculate new direction with spread

        // Instantion projectile
        GameObject currentSpell = Instantiate(spell[index].spellMesh, attackPoint.position, Quaternion.identity);
        //Instantiate(spellEffects, attackPoint.position, Quaternion.identity);

        // Rotate spell to shoot direction
        currentSpell.transform.forward = directionWithoutSpread.normalized;

        // Add forces to spell
        currentSpell.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * spell[index].forwardForce, ForceMode.Impulse);

        // Create the muzzle flash
        //if (muzzleFlash != null)
        //    Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        
        currentMana -= spell[index].manaCost;
        spellsCasted += spell[index].manaCost;
        DestroyObject(currentSpell);

        // Invoke resetShot function
        if (allowInvoke)
        {
            Invoke("ResetShot", spell[index].timeBetweenCasting);
            allowInvoke = false;
        }

        if (spellsCasted < spell[index].manaCost && currentMana > 0)
            Invoke("Shoot", spell[index].timeBetweenCasts);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(rechargeTime);

        while (currentMana < manaTotal)
        {
            if (shooting)
                break;
            Debug.Log("Charging");
            currentMana += manaRegen;
            if (currentMana > manaTotal)
            {
                currentMana = manaTotal;
            }
            yield return new WaitForSeconds(1);
            
        }
        reloading = false;

    }

    private void ReloadFinished()
    {
        while (currentMana < manaTotal)
        {
            Debug.Log("Charging");
            currentMana += manaRegen;
            
        }
        if (currentMana > manaTotal)
        {
            currentMana = manaTotal;
        }
        //currentMana = manaTotal;
        reloading = false;
    }

    private void DestroyObject(GameObject projectile)
    {
        var destroyTime = 5;
        Destroy(projectile, destroyTime);
    }
}
