using System.Collections;

using UnityEngine;

public struct TouchGUI
{
    #region Fields Arranged Nicely :)

    public Vector2 deltaPosition;
    public Vector2 position;

    public TouchPhase phase;

    public int fingerId;

    public bool PositionIsSet;

    #endregion Fields Arranged Nicely :)

    #region Methods

    //public Vector2 position
    //{
    //    get
    //    {
    //        return iPosition;
    //    }
    //    set
    //    {
    //        iPosition = value;
    //        iPositionIsSet  =true;
    //    }
    //}
    public override string ToString()
    {
        string s = "TOUCH = ";
        s += "position " + position + " ";
        s += "phase " + this.phase + " ";
        s += "phase " + this.fingerId + " ";
        return s;
    }

    #endregion Methods
}