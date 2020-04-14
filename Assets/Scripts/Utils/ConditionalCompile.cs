

using System.Collections;

using UnityEngine;

public static class ConditionalCompile
{
    #region Methods

    public static int InputTouchCount()
    {
        int touchCount = Input.touchCount;
#if (UNITY_EDITOR || PC_MAC)
        if (touchCount == 0)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))// || Input.anyKeyDown || Input.anyKey)
            {
                touchCount = 1;
            }
        }

#endif

        return touchCount;
    }

    public static TouchGUI GetTouch(int aID)
    {
        TouchGUI touchGUI = new TouchGUI();

        if (Input.touchCount > aID)
        {
            Touch touch = Input.GetTouch(aID);
            touchGUI.position = touch.position;
            touchGUI.phase = touch.phase;
            touchGUI.fingerId = touch.fingerId;
            touchGUI.deltaPosition = touch.deltaPosition;
        }


#if (PC_MAC || UNITY_EDITOR)
        else
        {
            touchGUI.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            touchGUI.fingerId = 0;
            touchGUI.phase = TouchPhase.Ended;
            touchGUI.deltaPosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (Input.GetMouseButtonDown(aID)) //|| Input.anyKeyDown)
            {
                touchGUI.phase = TouchPhase.Began;
            }
            else if (Input.GetMouseButton(aID)) //|| Input.anyKey)
            {
                touchGUI.phase = TouchPhase.Moved;
            }
        }
#endif
        return touchGUI;
    }

    #endregion Methods
}