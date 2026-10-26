using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList = new List<GameObject>();

    Camera mainCam;
    bool missHandledThisFrame;
    readonly List<GameObject> appleList = new List<GameObject>();

    void Awake()
    {
        mainCam = Camera.main;
        if (basketList == null)
        {
            basketList = new List<GameObject>();
        }
    }

    void Start()
    {
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    void Update()
    {
        MoveBasketsToMouse();
    }

    void LateUpdate()
    {
        missHandledThisFrame = false;
    }

    void MoveBasketsToMouse()
    {
        if (mainCam == null || basketList.Count == 0)
        {
            return;
        }

        Vector3 mousePos2D = Input.mousePosition;
        // If this line causes a NullReferenceException, select the Main Camera
        // in the Hierarchy and set its Transform Position Z to a negative number.
        mousePos2D.z = -mainCam.transform.position.z;
        float mouseX = mainCam.ScreenToWorldPoint(mousePos2D).x;

        for (int i = 0; i < basketList.Count; i++)
        {
            GameObject basketGO = basketList[i];
            if (basketGO == null)
            {
                continue;
            }
            Vector3 pos = basketGO.transform.position;
            pos.x = mouseX;
            basketGO.transform.position = pos;
        }
    }

    public void RegisterApple(GameObject apple)
    {
        appleList.Add(apple);
    }

    public void UnregisterApple(GameObject apple)
    {
        appleList.Remove(apple);
    }

    public void AppleMissed()
    {
        if (missHandledThisFrame || basketList.Count == 0)
        {
            return;
        }
        missHandledThisFrame = true;

        for (int i = appleList.Count - 1; i >= 0; i--)
        {
            Destroy(appleList[i]);
        }
        appleList.Clear();

        int basketIndex = basketList.Count - 1;
        GameObject tBasketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("_Scene_0");
        }
    }
}
