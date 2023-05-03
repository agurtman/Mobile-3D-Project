using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform player; // ссылка на игрока
    [SerializeField] private float mouseSense = 0.1f; // чувствительность мыши
    private float xAxisClamp; // диапазон оси X
    GameObject enemy;

    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.position.x >= Screen.width / 2)
            {
                float mouseX = touch.deltaPosition.x * mouseSense;
                float mouseY = touch.deltaPosition.y * mouseSense;

                xAxisClamp += mouseY;

                if (xAxisClamp > 10f)
                {
                    xAxisClamp = 10f;
                    mouseY = 10;
                    ClampXRotation(10f);
                }
                else if (xAxisClamp < -10f)
                {
                    xAxisClamp = -10f;
                    mouseY = -10;
                    ClampXRotation(10f);
                }

                transform.Rotate(Vector3.left * mouseY);
                player.Rotate(Vector3.up * mouseX);

                if (enemy != null)
                {
                    enemy.transform.Translate(-mouseX / 3, mouseY / 3, 0);
                }
            }
        }
    }

    private void ClampXRotation(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    public void FindEnemy(GameObject other)
    {
        enemy = other;
    }
}
