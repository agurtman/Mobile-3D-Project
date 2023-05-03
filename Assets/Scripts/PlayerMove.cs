using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float speed = 5f;
    [SerializeField] Transform rayPos;
    [SerializeField] GameObject car;
    [SerializeField] Transform point;
    [SerializeField] float radius;
    [SerializeField] Camera carCamera;
    [SerializeField] AudioSource carAudio;
    [SerializeField] AudioListener carListener;
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject carUI;
    CarController carController;
    Rigidbody rb;
    Vector3 direction;
    Animator anim;
    bool boost;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        carController = car.GetComponent<CarController>();
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        direction = transform.TransformDirection(horizontal, 0, vertical);
        anim.SetFloat("move", Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical)));

        if (boost)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }

        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + speed * direction * Time.deltaTime);
    }

    public void Punch()
    {
        anim.SetTrigger("punch");
        if (timer > 1f)
        {
            timer = 0;
            RaycastHit playerHit;
            Ray ray = new Ray(rayPos.position, rayPos.forward);

            if (Physics.Raycast(ray, out playerHit, 1.5f))
            {
                if (playerHit.transform.CompareTag("Enemy"))
                    playerHit.transform.GetComponent<EnemyAI>().ChangeHealth(-25);
            }

            if (playerHit.collider.CompareTag("enemy ragdoll"))
            {
                playerHit.collider.transform.root.GetComponent<EnemyRagdoll>().Dead(true);
                playerHit.collider.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 100, ForceMode.Impulse);
            }
        }
    }
        

    public void onPointerDownBoost()
    {
        boost = true;
    }

    public void onPointerUpBoost()
    {
        boost = false;
    }

    public void InCar()
    {
        if (Vector3.Distance(transform.position, point.position) < radius)
        {
            carCamera.enabled = true;
            gameObject.SetActive(false);
            gameObject.transform.SetParent(car.transform);
            carController.enabled = true;
            carListener.enabled = true;
            carAudio.Play();
            playerUI.SetActive(false);
            carUI.SetActive(true);
        }
    }

    public void SayHello()
    {
        anim.SetTrigger("hi");
    }

    public void HelloGuys(string say)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15);
        foreach (var people in colliders)
        {
            if (people.tag == "people")
            {
                people.GetComponent<Animator>().SetTrigger("hi");
                print(say);
            }
        }
    }
}
