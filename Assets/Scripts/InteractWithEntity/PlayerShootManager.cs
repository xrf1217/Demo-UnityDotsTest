using System;
using System.Collections;
using System.Collections.Generic;
using Ecs.Systems;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    private void Start()
    {
        PlayerShootSystem playerShootSyster= World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootSystem>();
        playerShootSyster.OnShoot += OnShoot;
    }

    private void OnShoot(object sender, EventArgs e)
    {
        Entity playerEntity = (Entity)sender;
        LocalTransform localTransform =
            World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
        Debug.Log(localTransform.Position);
    }

  
}
