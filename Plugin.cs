﻿using System;
using BepInEx;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
namespace startingCash;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        // Plugin load logic goes here!
        // This script acts like a unity object.
        Logger.LogInfo($"Starting Cash Active");
        harmony.PatchAll(typeof(startingCash));
    }

    //This defines the startingCash class to run on the TimeOfDay object on the Awake function
    [HarmonyPatch(typeof(TimeOfDay), "Awake")]
    
    class startingCash
    {
        //This is a postfix - it runs AFTER the normal awake function for all TimeOfDay objects
        //Something to note is that there is only one TimeOfDay object - it's not like an enemy where there's many
        private static void Postfix(ref TimeOfDay __instance)
        {
          if(GameNetworkManager.Instance.isHostingGame){ // Only runs if the user running the mod is the host
            //Sets the starting amount of cash to 1000 - change the number here to change the number of starting credits 
            int startingAmount = 1000;
            //References the singular time of day object and navigates to the startingCredits value
            __instance.quotaVariables.startingCredits = startingAmount;
          }
        }  
    }
}