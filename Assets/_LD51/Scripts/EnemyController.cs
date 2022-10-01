using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class EnemyController : MonoBehaviour
    {
        Transform player;
        [SerializeField] float searchInterval = 2f;
        [SerializeField] float normalSpeed = 2f;
        [SerializeField] float agressiveSpeed = 10f;
        float currentSpeed = 2f;
        float acceleration = 0.3f;
        [SerializeField] float detectionRange = 10f;
        Vector3 moveMentDirection;
        bool hasDetectedPlayer = false;
        float timer = 0;
        float changeDirectionTimer = 0;
        float changeDirectionInterval = 10f;
        


        public void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        
        
        public void Update()
        {
            if (timer > searchInterval)
            {
                timer = 0;
                DetectPlayer();
            }
            if (hasDetectedPlayer)
            {
                Vector3 playerDirection = (player.position - transform.position).normalized;
                Vector3 moveMentDirection = Vector3.RotateTowards(transform.position, playerDirection, 4, 4f);
                
                
                

            } else
            {
                if (changeDirectionTimer > changeDirectionInterval)
                {
                    changeDirectionTimer = 0;
                    ChangeDirection();

                }
            }
            timer += Time.deltaTime;
            changeDirectionTimer += Time.deltaTime;

            transform.position += moveMentDirection * currentSpeed * Time.deltaTime;
            //transform.rotation = Quaternion.LookRotation(moveMentDirection);
            if (moveMentDirection.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x,0,0);
            } else if (moveMentDirection.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 0, 0); 
            }
        }

        void ChangeDirection()
        {
            Vector3 dir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            moveMentDirection = dir;
            if (transform.position.y < -21f && moveMentDirection.y < 0)
            {
                moveMentDirection.y *= -1;
            } else if (transform.position.y > 5 && moveMentDirection.y > 0)
            {
                moveMentDirection.y *= -1;
            } 
        }
        
        void DetectPlayer()
        {
            if ((player.position - transform.position).sqrMagnitude <= detectionRange * detectionRange)
            {
                hasDetectedPlayer = true;
                if (currentSpeed < agressiveSpeed - 1f)
                {
                    StartCoroutine("ChangeSpeed");
                }
            } else
            {
                hasDetectedPlayer = false;
                if (currentSpeed > normalSpeed + 1f)
                {
                    StartCoroutine("ChangeSpeed");
                }
            }
        }

        IEnumerable ChangeSpeed()
        {
            if (hasDetectedPlayer && currentSpeed < agressiveSpeed - 0.1f)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, agressiveSpeed, acceleration);
                yield return null;
            } else if (!hasDetectedPlayer && currentSpeed > normalSpeed +0.1f)
            {
                currentSpeed = Mathf.Lerp(agressiveSpeed, normalSpeed, acceleration);
                yield return null;
            }
            yield return null;
        }

    }
}
