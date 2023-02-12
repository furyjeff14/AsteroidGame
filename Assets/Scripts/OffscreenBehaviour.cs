using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenBehaviour : MonoBehaviour
{
    //Simply use a flat value for now, we can use percentages of screens as needed
    private float offsetScreenLimit = 15f;
    private bool isCheckPosition = true;

    public void OnBecameInvisible()
    {
        if (isCheckPosition)
        {
            return;
        }
        ProcessPosition();
    }

    private void OnDisable()
    {
        isCheckPosition = false;
    }

    private void ProcessPosition()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float posX = screenPos.x;
        float posY = screenPos.y;
        if (screenPos.x > Screen.width)
        {
            posX = -offsetScreenLimit;
        }
        if (screenPos.x < 0)
        {
            posX = Screen.width + offsetScreenLimit;
        }
        if (screenPos.y > Screen.height)
        {
            posY = -offsetScreenLimit;
        }
        if (screenPos.y < 0)
        {
            posY = Screen.height + offsetScreenLimit;
        }

        Vector3 tempScreenPos = new Vector3(posX, posY, screenPos.z);
        Vector3 newScreenPos = Camera.main.ScreenToWorldPoint(tempScreenPos);
        transform.parent.position = newScreenPos;
    }
}
