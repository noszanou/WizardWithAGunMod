using GGECS;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using UnityEngine.Rendering.VirtualTexturing;

namespace BullshitLib
{
    public static class PlayerInteraction
    {
        public static void HandleGodMode()
        {
            return; // marche po jregarde plus tard rompichame
            var world = GameUniverse.playerFocusedWorld.world;

            world.GetSystem<PlayerSystem>().GetAllPlayers(out NativeArray<ECSEntity>.ReadOnly player, out int l);
            for (int index = 0; index < player.Length; ++index)
            {
                try
                {
                    ECSEntity entity = player[index];

                    string entityName = world.entityManager.GetEntityName(entity);
                    if (world.entityManager.HasComponent<GodModeComp>(entity))
                    {
                        world.entityManager.RemoveComponent<GodModeComp>(entity);
                        PlayerComp comp;
                        if (world.entityManager.TryGetComponent<PlayerComp>(entity, out comp))
                            GameUniverse.persistentWorld.world.entityManager.RemoveComponent<GodModeComp>(comp.persistentEntity);
                        Console.WriteLine("GodMode OFF for Entity " + entityName);
                    }
                    else
                    {
                        world.entityManager.AddComponent<GodModeComp>(entity);
                        PlayerComp comp;
                        if (world.entityManager.TryGetComponent<PlayerComp>(entity, out comp))
                            GameUniverse.persistentWorld.world.entityManager.AddComponent<GodModeComp>(comp.persistentEntity);
                        Console.WriteLine("GodMode ON for Entity " + entityName);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }

        public static void HandleDmg()
        {
            GameUniverse.persistentWorld.world.GetMainThreadData<TestSettingsData>().SetDamageMultiplier(5000);
        }
    }
}
