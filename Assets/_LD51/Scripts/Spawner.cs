using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] int numberOfspawns = 100;
        [SerializeField] float spawnInterval;
        List<GameObject> spawnList = new List<GameObject>();
        float timer;
        [SerializeField] bool randomizeX = false;
        internal void Despawn(GameObject objectToDespawn)
        {
            spawnList.Remove(objectToDespawn);
            spawningPool.Enqueue(objectToDespawn);
            objectToDespawn.SetActive(false);
        }

        Transform cameraTransfor;

        Queue<GameObject> spawningPool = new Queue<GameObject>();
        [SerializeField] GameObject prefab;

        private void Start()
        {
            cameraTransfor = Camera.main.transform;
            spawningPool = new Queue<GameObject>();
            for (int i = 0; i < numberOfspawns; i++)
            {
                GameObject go = Instantiate(prefab, new Vector3(0, 250, 1), Quaternion.identity, null);
                go.SetActive(false);
                spawningPool.Enqueue(go);
                
            }

        }

        void Spawn()
        {
            if (spawnList.Count >= 10) CleanUp();
            if (spawningPool.Count < 1) return;
            GameObject go = spawningPool.Dequeue();
            
            if (go != null)
            {
                ISpawnable spawned = go.GetComponent<ISpawnable>(); 
                if (spawned != null)
                {
                    spawned.Spawner = this;
                }
                go.transform.position = transform.position;
                go.SetActive(true);
                spawnList.Add(go);
                if (randomizeX)
                {
                    go.transform.position = new Vector3(Random.Range(transform.position.x - 40f, transform.position.x + 40f), transform.position.y, transform.position.z);
                }
                

            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > spawnInterval)
            {
                Spawn();
                timer = 0;
                spawnInterval += Random.Range(-0.3f,0.3f);
                if (spawnInterval < 1 || spawnInterval > 30) spawnInterval = 10;
            }
        }

        void CleanUp()
        {
            for ( int i = spawnList.Count - 1; i >= 0;  i--)
            {
                if (Mathf.Pow((spawnList[i].transform.position.x - cameraTransfor.position.x),2)  > 100f)
                {
                    spawningPool.Enqueue(spawnList[i]);
                    spawnList[i].SetActive(false);
                    spawnList.RemoveAt(i);

                }
            }
        }

    }
}
