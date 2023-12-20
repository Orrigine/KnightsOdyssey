using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShakeScreen : MonoBehaviour
{
    public static CinemachineShakeScreen Instance { get; private set; }
    
    [SerializeField] CinemachineVirtualCamera m_virtualCamera;
    private float m_shakeTimer;
    private float m_shakeTimerTotal;
    private float m_startingIntensity;

    [SerializeField] private AnimationCurve _intensityCurve;

    private void Reset()
    {
        m_virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
   
        StartCoroutine(ShakeRoutine());
        IEnumerator ShakeRoutine()
        {
            var startTime = Time.time;
            var elapsedTime = Time.time - startTime;
            elapsedTime = elapsedTime * time;

            while (elapsedTime < time)
            {
                elapsedTime = Time.time - startTime;
                
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensityCurve.Evaluate(elapsedTime)*intensity;
                
                yield return null;
            }

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }
    }

}
