using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] List<AxleInfo> axleInfos;
    [SerializeField] float maxMotorTorque;
    [SerializeField] float maxSteeringAngle;
    [SerializeField] Joystick joystick;
    [SerializeField] GameObject player;
    [SerializeField] GameObject spotLights;
    [SerializeField] GameObject pointLights;
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject carUI;
    [SerializeField] AudioSource breakAudio;
    bool isBreak;
    bool isLightsOn;
    float pitch;
    float speed;

    public void FixedUpdate()
    {
        pitch = Mathf.Lerp(0.6f, 1.6f, joystick.Vertical);
        GetComponent<AudioSource>().pitch = Mathf.Lerp(GetComponent<AudioSource>().pitch, pitch, 0.01f);

        float motor = maxMotorTorque * joystick.Vertical;
        float steering = maxSteeringAngle * joystick.Horizontal;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = -steering;
                axleInfo.rightWheel.steerAngle = -steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            if (!isBreak)
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
                pointLights.SetActive(false);
                speed += motor;
            }
            else
            {
                axleInfo.leftWheel.brakeTorque = 2000;
                axleInfo.rightWheel.brakeTorque = 2000;
                pointLights.SetActive(true);
                speed -= speed;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void StopOn()
    {
        isBreak = true;
        if (speed != 0)
            breakAudio.Play();
    }

    public void StopOff()
    {
        isBreak = false;
        breakAudio.Stop();
    }

    public void ExitCar()
    {
        player.SetActive(true);
        player.transform.parent = null;
        GetComponentInChildren<Camera>().enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
        GetComponent<AudioSource>().Stop();
        playerUI.SetActive(true);
        carUI.SetActive(false);
        this.enabled = false;
    }

    public void LightOn()
    {
        if (!isLightsOn)
        {
            spotLights.SetActive(true);
            isLightsOn = true;
        }
        else
        {
            spotLights.SetActive(false);
            isLightsOn = false;
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // присоединено ли колесо к мотору
    public bool steering; // поворачивает ли колесо
}