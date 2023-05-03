using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject impactPrefab;
    [SerializeField] Text ammoText;
    [SerializeField] GameObject punchButton;
    [SerializeField] GameObject shootUI;
    [SerializeField] GameObject gun;
    [SerializeField] Transform rayPos;
    [SerializeField] Text hpText;
    [SerializeField] GameObject carButton;
    [SerializeField] AudioSource shootSound;

    private int ammo;
    private int ammoMax = 30;
    private int ammoAll;
    private float shootTimer;
    private float range = 100;
    private int health;
    private int maxHealth = 100;
    bool shoot;
    bool isEquip;

    public void AddAmmo(int count)
    {
        ammoAll += count;
        ammoText.text = ammo + " / " + ammoAll;
    }

    public void Reload()
    {
        int ammoNeed = ammoMax - ammo;

        if (ammoAll >= ammoNeed)
        {
            ammoAll -= ammoNeed;
            ammo += ammoNeed;
        }
        else
        {
            ammo += ammoAll;
            ammoAll = 0;
        }
        ammoText.text = ammo + " / " + ammoAll;
    }

    public void OnPointerDown()
    {
        shoot = true;
    }

    public void OnPointerUp()
    {
        shoot = false;
    }

    public void EquipGun()
    {
        if (!isEquip)
        {
            isEquip = true;
            gun.SetActive(true);
            punchButton.SetActive(false);
            shootUI.SetActive(true);
        }
        else
        {
            isEquip = false;
            gun.SetActive(false);
            punchButton.SetActive(true);
            shootUI.SetActive(false);
        }
    }

    public void ChangeHealth(int count)
    {
        health += count;
        hpText.text = "HP: " + health.ToString();
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        Debug.Log("Game Over");
    }

    void Start()
    {
        AddAmmo(300);
        Reload();
        ChangeHealth(100);
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shoot && shootTimer >= 0.1f)
        {
            if (ammo <= 0)
            {
                return;
            }
            shootSound.Play();
            shootTimer = 0;
            ammo -= 1;
            ammoText.text = ammo + " / " + ammoAll;

            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.collider.name);
                GameObject impact = Instantiate(impactPrefab, hit.point, Quaternion.identity);
                Destroy(impact, 0.5f);

                if (hit.collider.CompareTag("Enemy"))
                    hit.collider.GetComponent<EnemyAI>().ChangeHealth(-10);

                if (hit.collider.CompareTag("enemy ragdoll"))
                {
                    hit.collider.transform.root.GetComponent<EnemyRagdoll>().Dead(true);
                    hit.collider.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 100, ForceMode.Impulse);
                }
            }
        }

        RaycastHit playerHit;
        Ray ray = new Ray(rayPos.position, rayPos.forward);
        Debug.DrawRay(rayPos.position, rayPos.forward, Color.red);

        if (Physics.Raycast(ray, out playerHit, 2f))
        {
            if (playerHit.transform.CompareTag("NPC"))
                playerHit.transform.GetComponent<NavigationAI>().SayHello();

            if(playerHit.transform.CompareTag("Health") && health < maxHealth)
            {
                ChangeHealth(maxHealth - health);
                Destroy(playerHit.collider.gameObject);
            }

            if (playerHit.transform.CompareTag("Ammo"))
            {
                AddAmmo(100);
                Destroy(playerHit.collider.gameObject);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Car"))
            carButton.SetActive(true);
        else
            carButton.SetActive(false);
    }
}
