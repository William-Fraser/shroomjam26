using UnityEngine;

public class LookAtCam : MonoBehaviour
{
   
    public Canvas canvas;
    void Update()
    {
        canvas.gameObject.transform.LookAt(Camera.main.transform);
    }
}
