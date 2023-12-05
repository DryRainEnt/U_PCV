using System;
using PCV_Fundamentals;
using UnityEngine;
using UnityEngine.UI;

namespace PCV_Interfaces
{
    public class Cursor : Singleton<Cursor>
    {
        public RectTransform Transform;
        public Image Image;
        
        public Vector2 Position => Transform.position;

        private void Awake()
        {
            Image = GetComponent<Image>();
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Image.enabled = !Image.enabled;
            }
            
            // Cursor follows mouse
            Transform.position = Input.mousePosition;
            
            if (Input.GetMouseButtonDown(0))
            {
                var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var subject = hit.collider;
                    if (subject != null)
                    {
                        Debug.Log($"Clicked on {subject.name}");
                    }
                }
            }
        }
    }
}
