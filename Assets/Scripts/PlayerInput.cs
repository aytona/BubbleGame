using UnityEngine;
using System;
using System.Collections;

#region Simple Touch
public struct SimpleTouch
{
    public Vector2 StartTouchLocation;
    public Vector2 CurrentTouchLocation;
    public DateTime StartTime;
    public TouchPhase Phase;
}
#endregion Simple Touch

public class PlayerInput : MonoBehaviour {

    #region Public Variables
    public float SwipeTime;
    public float SwipeDistance;
    #endregion Public Variables

    #region Private Variables
    private RaycastHit2D hit;
    private SimpleTouch ActiveTouch;
    private Touch DeviceTouch;
    #endregion Private Variables

    #region MonoBehaviour
    void Start()
    {
        ActiveTouch.Phase = TouchPhase.Canceled;
    }

    void Update()
    {
        #region Editor Controls
        if (Application.isEditor)
        {
            if (Input.GetMouseButton(0))
            {
                if (ActiveTouch.Phase == TouchPhase.Canceled)
                {
                    ActiveTouch.CurrentTouchLocation = Input.mousePosition;
                    ActiveTouch.StartTouchLocation = Input.mousePosition;
                    ActiveTouch.StartTime = DateTime.Now;
                    ActiveTouch.Phase = TouchPhase.Began;
                }
                else
                {
                    ActiveTouch.CurrentTouchLocation = Input.mousePosition;
                }
            }
            else
            {
                if (ActiveTouch.Phase == TouchPhase.Began)
                {
                    CalculateTouchInput(ActiveTouch);
                    ActiveTouch.Phase = TouchPhase.Canceled;
                }
            }
        }
        #endregion Editor Controls
        #region Touch Controls
        else
        {
            if (Input.touchCount > 0)
            {
                DeviceTouch = Input.GetTouch(0);
                if (ActiveTouch.Phase == TouchPhase.Canceled)
                {
                    ActiveTouch.Phase = DeviceTouch.phase;
                    ActiveTouch.StartTime = DateTime.Now;
                    ActiveTouch.StartTouchLocation = DeviceTouch.position;
                    ActiveTouch.CurrentTouchLocation = DeviceTouch.position;
                }
                else
                {
                    ActiveTouch.CurrentTouchLocation = DeviceTouch.position;
                    ActiveTouch.Phase = DeviceTouch.phase;
                }
            }
            else
            {
                if (ActiveTouch.Phase != TouchPhase.Canceled)
                {
                    CalculateTouchInput(ActiveTouch);
                    ActiveTouch.Phase = TouchPhase.Canceled;
                }
            }
        }
        #endregion TouchControls
    }
    #endregion MonoBehaviour

    #region Private Functions
    private void CalculateTouchInput(SimpleTouch CurrentTouch)
    {
        Vector2 touchDirection = (CurrentTouch.CurrentTouchLocation - CurrentTouch.StartTouchLocation).normalized;
        float touchDistance = (CurrentTouch.StartTouchLocation - CurrentTouch.CurrentTouchLocation).magnitude;
        TimeSpan timeGap = System.DateTime.Now - CurrentTouch.StartTime;
        double touchTimeSpan = timeGap.TotalSeconds;

        // Set Game Character (Unknown for now)
    }
    #endregion Private Functions
}
