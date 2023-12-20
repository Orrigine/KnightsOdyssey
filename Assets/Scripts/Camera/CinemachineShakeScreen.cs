using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShakeScreen : MonoBehaviour
{
    public static CinemachineShakeScreen Instance { get; private set; }
    
    private CinemachineVirtualCamera m_virtualCamera;
    private float m_shakeTimer;
    private float m_shakeTimerTotal;
    private float m_startingIntensity;
    
    private void Awake()
    {
        Instance = this;
        m_virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        m_startingIntensity = intensity;
        m_shakeTimerTotal = time;
        m_shakeTimer = time;
    }

    private void Update()
    {
        if (m_shakeTimer > 0f)
        {
            m_shakeTimer -= Time.deltaTime;
            if (m_shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                Mathf.Lerp(m_startingIntensity, 0f, 1 - (m_shakeTimer / m_shakeTimerTotal));
            }
        }
    }
}
