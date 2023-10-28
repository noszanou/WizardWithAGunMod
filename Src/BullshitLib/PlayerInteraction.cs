using GGECS;
using System;
using Unity.Collections;

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
                        if (world.entityManager.TryGetComponent<PlayerComp>(entity, out PlayerComp comp))
                            GameUniverse.persistentWorld.world.entityManager.RemoveComponent<GodModeComp>(comp.persistentEntity);
                        Console.WriteLine("GodMode OFF for Entity " + entityName);
                    }
                    else
                    {
                        world.entityManager.AddComponent<GodModeComp>(entity);
                        if (world.entityManager.TryGetComponent<PlayerComp>(entity, out PlayerComp comp))
                            GameUniverse.persistentWorld.world.entityManager.AddComponent<GodModeComp>(comp.persistentEntity);
                        Console.WriteLine("GodMode ON for Entity " + entityName);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }

        private static bool OpDamageEnabled { get; set; }
        public static void HandleDmg()
        {
            OpDamageEnabled = !OpDamageEnabled;
            GameUniverse.persistentWorld.world.GetMainThreadData<TestSettingsData>().SetDamageMultiplier(OpDamageEnabled ? 50000 : 1);
        }

        public static void HandleCrafting()
        {
            // Free craft/build etc
            GameUniverse.persistentWorld.world.GetMainThreadData<TestSettingsData>().FreeCraftingEnabled();
        }
    }
}
