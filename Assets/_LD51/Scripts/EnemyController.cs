using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class EnemyController : MonoBehaviour, ISpawnable
    {
        public Spawner Spawner { get; set; }
        Transform player;
        [SerializeField] float searchInterval = 2f;
        [SerializeField] float normalSpeed = 2f;
        [SerializeField] float agressiveSpeed = 10f;
        [SerializeField] float currentSpeed = 2f;
        float acceleration = 0.3f;
        [SerializeField] float detectionRange = 10f;
        [SerializeField] Vector3 moveMentDirection;
        [SerializeField] bool hasDetectedPlayer = false;
        float timer = 0;
        float changeDirectionTimer = 0;
        float changeDirectionInterval = 10f;
        GameManager gameManager;


        public void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            moveMentDirection = new Vector3(1, 0, 0);
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null) player = playerObject.transform;

            currentSpeed = normalSpeed;
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
                moveMentDirection = playerDirection.normalized;
                
                
                

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

            
            //transform.rotation = Quaternion.LookRotation(moveMentDirection);
            if (moveMentDirection.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x,1,1);
            } else if (moveMentDirection.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1); 
            }
            ChangeDirectionAtBounds();
            transform.position += moveMentDirection * currentSpeed * Time.deltaTime;
        }

        void ChangeDirection()
        {
            Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            
            moveMentDirection = dir;
            //ChangeDirectionAtBounds();
        }

        private void ChangeDirectionAtBounds()
        {
            if (transform.position.y < -10f && moveMentDirection.y < 0)
            {
                moveMentDirection.y *= -1;
            }
            else if (transform.position.y > 0 && moveMentDirection.y > 0)
            {
                moveMentDirection.y *= -1;
            }
        }

        void DetectPlayer()
        {
            if (player == null)
            {
                GameObject playerObject = GameObject.FindWithTag("Player");
                if (playerObject == null) return;
                player = playerObject.transform;

                
            }
            if (!player.gameObject.activeInHierarchy || player.position.y > 3)
            {
                hasDetectedPlayer = false;
                return;
            }
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
                    //StartCoroutine("ChangeSpeed");
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
