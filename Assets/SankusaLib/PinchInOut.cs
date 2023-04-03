using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PinchInOut : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> onPinch = new UnityEvent<float>();
    public UnityEvent<float> OnPinch => onPinch;

    [SerializeField] private UnityEvent<Vector2> onPinchStart = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnPinchStart => onPinchStart;

    [SerializeField] private UnityEvent onPinchEnd = new UnityEvent();
    public UnityEvent OnPinchEnd => onPinchEnd;

    float oldDistance = 0;

    void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    void Update()
    {
        if(Touch.activeTouches.Count < 2) return;

        Touch touch0 = Touch.activeTouches[0];
        Touch touch1 = Touch.activeTouches[1];

        
        if(touch1.phase == TouchPhase.Began) {
            oldDistance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);
            onPinchStart.Invoke((touch0.screenPosition + touch1.screenPosition) / 2f);
        }

        if(touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) {
            onPinchEnd.Invoke();
        }
        //タップ２か所がどちらもドラッグしていない時は何もしない
        if(!(touch0.phase == TouchPhase.Moved) && !(touch1.phase == TouchPhase.Moved))
        {
            return;
        }

        float distance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);
        //前回のフレームとの差
        OnPinchInOut(distance - oldDistance);
        oldDistance = distance;
    }

    void OnPinchInOut(float delta)
    {
        onPinch.Invoke(delta);
    }
   
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        //公式には何も書いてなかったけど一応Disableしてます
        EnhancedTouchSupport.Disable();
    }

}