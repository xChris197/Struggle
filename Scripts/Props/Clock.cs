using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform hourHand;

    private const float secondsPerInGameDay = 86400f;
    private const float hoursPerInGameDay = 24f;

    private float day = 1f;

    private void FixedUpdate()
    {
        day += Time.deltaTime / secondsPerInGameDay;
        float dayNormalised = day % 1f;
        float rotationDegreesPerDay = 360f;

        hourHand.transform.Rotate(Vector3.up * -dayNormalised * rotationDegreesPerDay);
        minuteHand.transform.Rotate(Vector3.up * -dayNormalised * rotationDegreesPerDay * hoursPerInGameDay);
    }
}
