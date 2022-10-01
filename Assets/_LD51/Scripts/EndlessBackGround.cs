using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class EndlessBackGround : MonoBehaviour
    {
        [SerializeField] Transform[] backgroundTransform;
        [SerializeField] Transform cameraTransform;
        BoxCollider2D col;
        public bool isVisible;
        private void Start()
        {
            cameraTransform = Camera.main.transform;
            col = GetComponent<BoxCollider2D>();
            //MoveBackground();
        }
        private void MoveBackground(int direction = 1)
        {
            if (!this.isVisible)
            {

                foreach (Transform t in backgroundTransform)
                {

                    if (t == this.transform) continue;
                    
                    
                    
                        
                        float newX = t.transform.position.x + (col.size.x * direction);
                        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                        break;
                    

                }
            }
        }


        private void LateUpdate()
        {

            if (this.transform.position.x < cameraTransform.position.x - 40 && isVisible)
            {
                isVisible = false;
                MoveBackground();

            } else if (this.transform.position.x > cameraTransform.position.x + 40 && isVisible) 
            {
                
                isVisible = false;
                MoveBackground(-1);
                
            }
            else
            {
                isVisible = true;
            }
            
            
        }

    }
}
