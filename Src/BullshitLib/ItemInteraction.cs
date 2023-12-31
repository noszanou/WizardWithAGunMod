﻿using GGECS;
using GGUtil;
using Inventory.Unity;
using Inventory;
using System.Collections.Generic;
using System.Text;
using System;
using Unity.Collections;

namespace BullshitLib
{
    public static class ItemInteraction
    {
        private static AssetHandle<StaticItem> FindStaticItem(string searchString, out string itemName)
        {
            searchString = searchString.ToLower();
            if (int.TryParse(searchString, out int result))
            {
                AssetHandle<StaticItem> handle = new AssetHandle<StaticItem>(result);
                StaticItem staticItem = Assets<StaticItem>.Get(handle);
                itemName = staticItem.name;
                return handle;
            }
            AssetItem<StaticItem>[] assets = Assets<StaticItem>.GetAssets();
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            foreach (AssetItem<StaticItem> assetItem in assets)
            {
                string locItemName = assetItem.asset.locItemName;
                stringList1.Add(locItemName == null ? string.Empty : locItemName.ToLower());
                stringList2.Add(assetItem.asset.name.ToLower());
            }
            for (int index = 0; index < assets.Length; ++index)
            {
                if (searchString == stringList2[index])
                {
                    itemName = stringList2[index];
                    return assets[index].handle;
                }
            }
            for (int index = 0; index < assets.Length; ++index)
            {
                if (searchString == stringList1[index])
                {
                    itemName = stringList2[index];
                    return assets[index].handle;
                }
            }
            for (int index = 0; index < assets.Length; ++index)
            {
                if (stringList2[index].Contains(searchString))
                {
                    itemName = stringList2[index];
                    return assets[index].handle;
                }
            }
            for (int index = 0; index < assets.Length; ++index)
            {
                if (stringList1[index].Contains(searchString))
                {
                    itemName = stringList2[index];
                    return assets[index].handle;
                }
            }
            itemName = string.Empty;
            return AssetHandle<StaticItem>.invalid;
        }

        public static void CreateItem(string itemNameToFind, int amount = 1, bool everyone = false)
        {
            if (!GameUniverse.isValid)
            {
                return;
            }

            InventorySystem system = GameUniverse.playerFocusedWorld.world.GetSystem<InventorySystem>();
            if (system == null)
            {
                Console.WriteLine("Cannot process give-item cmd because there is no InventorySystem");
                return;
            }

            if (!GameUniverse.TryGetPlayerAvatarWorldAndEntity(out ECSWorld _, out ECSEntity entity))
            {
                Console.WriteLine("Cannot process give-item cmd because there is no Player Entity");
                return;
            }

            ItemAssetHandle itemHandle = new ItemAssetHandle(FindStaticItem(itemNameToFind, out string itemName));

            if (!itemHandle.isValid)
            {
                Console.WriteLine($"Could not find item using {itemNameToFind} as a search parameter.");
                return;
            }
            if (everyone)
            {
                GameUniverse.playerFocusedWorld.world.GetSystem<PlayerSystem>().GetAllPlayers(out NativeArray<ECSEntity>.ReadOnly player, out int l);
                for (int index = 0; index < player.Length; ++index)
                {
                    entity = player[index];
                    system.SendAddItemToInventoryCommand(entity, itemHandle, amount);
                }
                Console.WriteLine($"Successfully sent command to give {amount} {itemName}(s) to Player");
                return;
            }
            system.SendAddItemToInventoryCommand(entity, itemHandle, amount);
            Console.WriteLine($"Successfully sent command to give {amount} {itemName}(s) to Player");

        }

        public static string GetItemsByName(string name = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            foreach (AssetItem<StaticItem> asset in Assets<StaticItem>.GetAssets())
            {
                string locItemName = asset.asset.locItemName;
                if (string.IsNullOrEmpty(name) || asset.asset.name.Contains(name) || !string.IsNullOrEmpty(locItemName) && locItemName.Contains(name))
                {
                    //stringBuilder.Append(string.Format("{0,-3} | {1,-55} | {2}", asset.handle.id, asset.asset.name, locItemName));
                    stringBuilder.Append(asset.asset.name);
                    stringBuilder.AppendLine();
                }
            }
            return stringBuilder.ToString();
        }
    }
}