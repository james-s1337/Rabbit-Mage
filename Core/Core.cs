using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement movement { get; private set; }
    public CollisionSenses collisionSenses { get; private set; }

    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionSenses = GetComponentInChildren<CollisionSenses>();

        if (movement == null || collisionSenses == null)
        {
            Debug.LogError("Missing Core Component");
        }
    }

    public void LogicUpdate()
    {
        movement.LogicUpdate();
    }
}
