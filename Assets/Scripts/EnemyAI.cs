using UnityEngine;

public class EnemyAI : NavigationAI
{
    [SerializeField] [Range(0, 360)] private float viewAngle = 90f;
    [SerializeField] private float viewDistance = 15f;
    [SerializeField] private Transform target;
    private float timer = 0;
    [SerializeField] private int health = 100;
    bool isDead;

    protected override void Update()
    {
        float distanceToPlayer = Vector3.Distance(target.position, agent.transform.position);

        if (!isDead)
        {
            if (IsInView())
            {
                if (distanceToPlayer >= 1.5f)
                    MoveToTarget();
                else
                {
                    timer += Time.deltaTime;
                    agent.isStopped = true;
                    anim.SetBool("idle", true);

                    if (timer > 1)
                    {
                        timer = 0;
                        anim.SetTrigger("hit");
                        target.GetComponent<PlayerController>().ChangeHealth(-10);
                    }
                }
            }
            else
            {
                agent.isStopped = false;
                base.Update();
            }

            DrawView();
        }
    }

    private bool IsInView()
    {
        float currentAngle = Vector3.Angle(transform.forward, target.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, viewDistance))
        {
            if (currentAngle < viewAngle / 2f && Vector3.Distance(transform.position, target.position) <= viewDistance && hit.transform == target.transform)
                return true;
        }
        return false;
    }

    private void MoveToTarget()
    {
        agent.isStopped = false;
        agent.speed = 3.5f;
        agent.SetDestination(target.position);
        anim.SetBool("idle", false);
    }

    public void ChangeHealth(int count)
    {
        health += count;
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        //anim.SetTrigger("dead");
        agent.speed = 0;
        isDead = true;
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        Destroy(this.gameObject, 20);
    }

    private void DrawView()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, viewAngle / 2f, 0)) * (transform.forward * viewDistance);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, viewAngle / 2f, 0)) * (transform.forward * viewDistance);
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }
}
