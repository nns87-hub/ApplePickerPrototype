using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    ApplePicker applePicker;

    void Start()
    {
        applePicker = Camera.main.GetComponent<ApplePicker>();
        if (applePicker != null)
        {
            applePicker.RegisterApple(gameObject);
        }
    }

    void OnDestroy()
    {
        if (applePicker != null)
        {
            applePicker.UnregisterApple(gameObject);
        }
    }

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(gameObject);

            if (applePicker != null)
            {
                applePicker.AppleMissed();
            }
        }
    }
}
