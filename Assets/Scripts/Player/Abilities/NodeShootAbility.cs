using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeShootAbility : PlayerAbilities
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float destoryBulletCD = 3.0f;
    public List<GameObject> listOfNodes = new List<GameObject>();
    private GameObject currentBulletOBJ;
    private float timer;
    private int bulletsLeft;
    [SerializeField]private GameObject[] uiImages;

    private bool bulletFiring;

    public override void SetAbilityToPlayer()
    {
        base.SetAbilityToPlayer(); // Run base setup first


        timer = 0;
        bulletsLeft = 3;
        uiImages.First().gameObject.transform.parent.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (bulletsLeft > 3)
        {
            bulletsLeft = 3;
        }
        if ((bulletFiring))
        {
            timer += Time.deltaTime;

            if (currentBulletOBJ == null) {
                timer = 0;
                bulletFiring = false;
            
            }


            if (timer > destoryBulletCD)
            {
                timer = 0;
                bulletFiring = false;
                if (currentBulletOBJ != null) { 
                    Destroy(currentBulletOBJ);
                    GetBulletBack();
                }
            }
        }

        if (abilityAction1.WasCompletedThisFrame() && !bulletFiring && bulletsLeft!=0)
        {
            FireBullet();
        }
        if (abilityAction3.WasCompletedThisFrame()) {
            TakeBack();



        }
        //Debug.Log(bulletsLeft);
    }

    private void FireBullet()
    {

        Vector2 aimDirection = player.aimAction.ReadValue<Vector2>().normalized;
        if (aimDirection == Vector2.zero) aimDirection = new Vector2(transform.parent.transform.localScale.x, 0);

        currentBulletOBJ = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = currentBulletOBJ.GetComponent<Rigidbody2D>();
        NodeShootBullet currentBullet = currentBulletOBJ.GetComponent<NodeShootBullet>();

        if (bulletRb != null && currentBullet !=null)
        {
            bulletFiring = true;

            bulletRb.gravityScale = 0f;
            bulletRb.linearVelocity = aimDirection * bulletSpeed;
            currentBullet.InitBullet(abilityAction2, this);
            bulletsLeft--;
            UpdateUI();



        }
    }

    public void ResetAll()
    {
        foreach (GameObject obj in listOfNodes) { 
            listOfNodes.Remove(obj);
            Destroy(obj);
        }
        bulletsLeft = 3;
        UpdateUI();
    }
    private void TakeBack()
    {
        if (listOfNodes.Count > 0)
        {
            Destroy(listOfNodes[0]);
            listOfNodes.RemoveAt(0);
            GetBulletBack();
            //Debug.Log(bulletsLeft);

        }
    }
    public void GetBulletBack()
    {
        bulletsLeft++;
        UpdateUI();
    }
    private void UpdateUI()
    {

        for (int i = 0; i < uiImages.Count(); i++)
        {
            uiImages[i].SetActive(i < bulletsLeft);
        }

    }


}
