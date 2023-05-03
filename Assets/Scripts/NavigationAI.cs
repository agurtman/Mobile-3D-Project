using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationAI : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();
    protected NavMeshAgent agent;
    protected Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetDestination();
    }

    protected virtual void Update()
    {
        if (agent.remainingDistance < 0.2f)
        {
            StartCoroutine(Idle());
        }
    }

    public void SetDestination()
    {
        Vector3 newTarget = points[Random.Range(0, points.Count)].position;
        agent.SetDestination(newTarget);
    }

    IEnumerator Idle()
    {
        agent.speed = 0;
        SetDestination();
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(5);
        agent.speed = 3.5f;
        anim.SetBool("idle", false);
    }

    IEnumerator Hello()
    {
        agent.speed = 0;
        SetDestination();
        anim.SetBool("hello", true);
        yield return new WaitForSeconds(3);
        agent.speed = 3.5f;
        anim.SetBool("hello", false);
    }

    public void SayHello()
    {
        StartCoroutine(Hello());
    }
}