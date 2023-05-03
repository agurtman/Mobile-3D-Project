using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCheck : MonoBehaviour
{
    [SerializeField] private int checkpoints;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            checkpoints++;
            startPosition = transform.position;
        }
    }

    public int GetCheck()
    {
        return checkpoints;
    }

    public void Respawn()
    {
        transform.position = startPosition;
    }
}
