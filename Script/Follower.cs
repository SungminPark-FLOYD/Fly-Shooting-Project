using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private float maxShotDelay;
    [SerializeField]
    private float curShotDelay;
    [SerializeField]
    private Vector3 followPos;
    [SerializeField]
    private int followDelay;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Queue<Vector3> parentPos;

    public ObjectManager objectManager;

    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }
    private void Update()
    {
        Watch();
        TryAttack();
        Follow();
        Reload();
    }

    private void Watch()
    {
        if(!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position);

        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay)
            followPos = parent.position;
    }

    private void Follow()
    {
        transform.position = followPos;
    }
    private void TryAttack()
    {
        if (!Input.GetButton("Fire1")) return;

        if (curShotDelay < maxShotDelay) return;

        GameObject bullet = objectManager.MakeObj("BulletFollwer");
        bullet.transform.position = transform.position;

        curShotDelay = 0;
    }

   

    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
