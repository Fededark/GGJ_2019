using UnityEngine;

namespace TakUtility
{
    public class MouseFollow : MonoBehaviour {
        
        [SerializeField]
        [Tooltip("(Optional) Camera to use for the conversion. If not set Camera.main will be used")]
        private new Camera camera;
        
        void Awake()
        {
            if (camera == null)
                camera = Camera.main;
        }
        
        void Update() 
        {
            Vector3 mouse = camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(Mathf.Round(mouse.x), Mathf.Round(mouse.y), 0);        
        }
    }
}
