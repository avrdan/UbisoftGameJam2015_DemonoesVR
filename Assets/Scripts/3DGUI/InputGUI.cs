using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public static class InputGUI
{
    #region Fields Arranged Nicely :)

    //caching input data so that it is refreshed only if it is called in a newer frame
    static List<TouchGUI> touchList = new List<TouchGUI>();

    static int lastFrameTouches = -1; //the last frame this info was updated; if Time.frameCount is equal to this value



    #endregion Fields Arranged Nicely :)

    #region Properties

    public static List<TouchGUI> touches
    {
        get
        {
            if (lastFrameTouches != Time.frameCount)
            {
                touchList.Clear();
                int touchCount = ConditionalCompile.InputTouchCount();
                for (int i = 0; i < touchCount; ++i)
                {

                    touchList.Add(ConditionalCompile.GetTouch(i));
                }
                lastFrameTouches = Time.frameCount;
            }

            return touchList;
        }
    }

    #endregion Properties
}