using UnityEngine;
//using Windows.Kinect;
using System.Collections;
using System;

public class PlayerGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    [Tooltip("UI-Text to display gesture-listener messages and gesture information.")]
    public UnityEngine.UI.Text gestureInfo;

    // singleton instance of the class
    private static PlayerGestureListener instance = null;

    // private bool to track if progress message has been displayed
    private bool progressDisplayed;
    private float progressGestureTime;

    // whether the needed gesture has been detected or not
    private bool swipeLeft = false;
    private bool swipeRight = false;
    private bool swipeUp = false;
    private bool rightHandRaised = false;
    private bool leftHandRaised = false;
    private bool isSquat = false;
    private bool isPush = false;
    private bool isRunning = false;
    /// <summary>
    /// Gets the singleton PlayerGestureListener instance.
    /// </summary>
    /// <value>The PlayerGestureListener instance.</value>
    public static PlayerGestureListener Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
	/// Determines whether swipe left is detected.
	/// </summary>
	/// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
	public bool IsSwipeLeft()
    {
        if (swipeLeft)
        {
            swipeLeft = false;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether swipe right is detected.
    /// </summary>
    /// <returns><c>true</c> if swipe right is detected; otherwise, <c>false</c>.</returns>
    public bool IsSwipeRight()
    {
        if (swipeRight)
        {
            swipeRight = false;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether swipe up is detected.
    /// </summary>
    /// <returns><c>true</c> if swipe up is detected; otherwise, <c>false</c>.</returns>
    public bool IsSwipeUp()
    {
        if (swipeUp)
        {
            swipeUp = false;
            return true;
        }

        return false;
    }

    public bool IsPush()
    {
        if (isPush)
        {
            isPush = false;
            return true;
        }

        return false;
    }

    public bool IsRightHandRaised()
    {
        if (rightHandRaised)
        {
            rightHandRaised = false;
            return true;
        }

        return false;
    }

    public bool IsLeftHandRaised()
    {
        if (leftHandRaised)
        {
            leftHandRaised = false;
            return true;
        }

        return false;
    }

    public bool IsSquat()
    {
        if (isSquat)
        {
            isSquat = false;
            return true;
        }

        return false;
    }

    public bool IsRunning()
    {
        if (isRunning)
        {
            isRunning = false;
            return true;
        }

        return false;
    }

    public void UserDetected(long userId, int userIndex)
    {
        // the gestures are allowed for the primary user only
        KinectManager manager = KinectManager.Instance;
        if (!manager || (userIndex != playerIndex))
            return;

        // Detect these specific gestures
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
        //		manager.DetectGesture(userId, KinectGestures.Gestures.LeanForward);
        //		manager.DetectGesture(userId, KinectGestures.Gestures.LeanBack);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);

        manager.DetectGesture(userId, KinectGestures.Gestures.Push);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
        manager.DetectGesture(userId, KinectGestures.Gestures.Run);

        if (gestureInfo != null)
        {
            gestureInfo.text = "Swipe Left, Right or Up.";
        }
    }

    public void UserLost(long userId, int userIndex)
    {
        if (userIndex != playerIndex)
            return;

        if (gestureInfo != null)
        {
            gestureInfo.text = string.Empty;
        }

        
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex != playerIndex)
            return;

        if ((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - {1:F0}%", gesture, screenPos.z * 100f);
                gestureInfo.text = sGestureText;

                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;
            }
        }
        else if ((gesture == KinectGestures.Gestures.Wheel || gesture == KinectGestures.Gestures.LeanLeft || gesture == KinectGestures.Gestures.LeanRight ||
            gesture == KinectGestures.Gestures.LeanForward || gesture == KinectGestures.Gestures.LeanBack) && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - {1:F0} degrees", gesture, screenPos.z);
                gestureInfo.text = sGestureText;

                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;
            }
        }
        else if (gesture == KinectGestures.Gestures.Run && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - progress: {1:F0}%", gesture, progress * 100);
                gestureInfo.text = sGestureText;

                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;

                isRunning = true;
            }
        }
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex != playerIndex)
            return false;

        /*if (progressDisplayed)
            return true;*/

        if (gestureInfo != null)
        {
            string sGestureText = gesture + " detected";
            gestureInfo.text = sGestureText;
        }
        if (gesture == KinectGestures.Gestures.SwipeLeft)
            swipeLeft = true;
        else if (gesture == KinectGestures.Gestures.SwipeRight)
            swipeRight = true;
        else if (gesture == KinectGestures.Gestures.SwipeUp)
            swipeUp = true;
        else if (gesture == KinectGestures.Gestures.RaiseLeftHand)
            leftHandRaised = true;
        else if (gesture == KinectGestures.Gestures.RaiseRightHand)
            rightHandRaised = true;
        else if (gesture == KinectGestures.Gestures.Squat)
            isSquat = true;
        else if (gesture == KinectGestures.Gestures.Push)
            isPush = true;

        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {
        if (userIndex != playerIndex)
            return false;

        if (progressDisplayed)
        {
            progressDisplayed = false;

            if (gestureInfo != null)
            {
                gestureInfo.text = String.Empty;
            }
        }

        return true;
    }

    void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 2f))
        {
            progressDisplayed = false;

            if (gestureInfo != null)
            {
                gestureInfo.text = String.Empty;
            }

            Debug.Log("Forced progress to end.");
        }
    }

}
