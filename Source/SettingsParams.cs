﻿/**
 * Thunder Aerospace Corporation's Life Support for Kerbal Space Program.
 * Originally Written by Taranis Elsu.
 * This version written and maintained by JPLRepo (Jamie Leighton)
 * 
 * (C) Copyright 2013, Taranis Elsu
 * (C) Copyright 2016, Jamie Leighton
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 * 
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 * 
 * Non-commercial - You may not use this work for commercial purposes.
 * 
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tac
{
    public class TAC_SettingsParms : GameParameters.CustomParameterNode

    {
        public override string Title { get { return "TAC LS Options"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override bool HasPresets { get { return false; } }
        public override string Section { get { return "TAC Life Support"; } }
        public override int SectionOrder { get { return 1; } }

        [GameParameters.CustomParameterUI("Enabled", autoPersistance = true, toolTip = "If on, TAC/LS is enabled in this save,\nIf off, it's not enabled in this save.")]
        public bool enabled = true;

        [GameParameters.CustomParameterUI("Use Stock App Launcher Icon", toolTip = "If on, the Stock Application launcher will be used,\nif off will use Blizzy Toolbar if installed.")]
        public bool UseAppLToolbar = true;

        [GameParameters.CustomParameterUI("Editor Filter", autoPersistance = true, toolTip = "Turn the TAC/LS Editor filter Category on and off.")]
        public bool EditorFilter = true;

        [GameParameters.CustomParameterUI("When resources run out, kerbals..", toolTip = "When LS resources run out,\nyou can select that kerbals die or hibernate.")]
        public string hibernate = "Die";

        [GameParameters.CustomIntParameterUI("Respawn delay (seconds)", minValue = 300, maxValue = 100000, stepSize = 300, autoPersistance = true, toolTip = "The time in seconds a kerbal is comatose\n if fatal EC / Heat option is off")]
        public int respawnDelay = 300;

        [GameParameters.CustomIntParameterUI("Vessel List Update delay (Minutes)", minValue = 1, maxValue = 30, stepSize = 1, autoPersistance = true, toolTip = "The Minutes between List Sorting for the Vessel List.\nThe list is sorted from least resources to most and this is costly performance-wise.")]
        public int vesselUpdateList = 5;

        public override IList ValidValues(MemberInfo member)
        {
            if (member.Name == "hibernate")
            {
                List<string> myList = new List<string>();
                myList.Add("Die");
                myList.Add("Hibernate");
                IList myIlist = myList;
                return myIlist;
            }
            else
            {
                return null;
            }
        }

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            if (HighLogic.fetch != null)
            {
                if (HighLogic.LoadedScene == GameScenes.MAINMENU || HighLogic.LoadedScene == GameScenes.SETTINGS || HighLogic.LoadedScene == GameScenes.SPACECENTER)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            if (member.Name == "enabled")
                return true;
            if (member.Name == "respawnDelay")
            {
                if (enabled && hibernate != "Die")
                    return true;
                return false;
            }
            if (enabled)
                return true;
            return false;
        }
    }

    public class TAC_SettingsParms_Sec2 : GameParameters.CustomParameterNode

    {
        public override string Title
        {
            get { return "TAC LS Consumption/Production"; }
        }

        public override GameParameters.GameMode GameMode
        {
            get { return GameParameters.GameMode.ANY; }
        }

        public override bool HasPresets
        {
            get { return false; }
        }

        public override string Section
        {
            get { return "TAC Life Support"; }
        }

        public override int SectionOrder
        {
            get { return 2; }
        }
        
        [GameParameters.CustomFloatParameterUI("Food Consumption Rate",
            toolTip = "Amt of food consumed per Kerbal (units per Day).", minValue = 0.01f, maxValue = 2f, displayFormat = "F6", stepCount = 400)]
        public float FoodConsumptionRate = 0.365625f;

        [GameParameters.CustomFloatParameterUI("Water Consumption Rate",
            toolTip = "Amt of water consumed per Kerbal (units per Day).", minValue = 0.01f, maxValue = 2f, displayFormat = "F6", stepCount = 400)]
        public float WaterConsumptionRate = 0.2416625f;

        [GameParameters.CustomFloatParameterUI("Oxygen Consumption Rate",
            toolTip = "Amt of oxygen consumed per Kerbal (units per Day).", minValue = 10f, maxValue = 200f, displayFormat = "F6", stepCount = 400)]
        public float OxygenConsumptionRate = 37.01241f;

        [GameParameters.CustomFloatParameterUI("Base Electricity Rate",
            toolTip = "Base Electricity Consumption Rate (units per Day).", minValue = 300f, maxValue = 1500f, displayFormat = "F1", stepCount = 1000)]
        public float BaseElectricityConsumptionRate = 459f;

        [GameParameters.CustomFloatParameterUI("Kerbal Electricity Rate",
            toolTip = "Per Kerbal Electricity Consumption Rate (units per Day).", minValue = 100f, maxValue = 600f, displayFormat = "F1", stepCount = 1000)]
        public float ElectricityConsumptionRate = 306f;

        [GameParameters.CustomFloatParameterUI("EVA Electricity Rate",
            toolTip = "EVA Electricity Consumption Rate (units per Day).", minValue = 5f, maxValue = 300f, displayFormat = "F1", stepCount = 500)]
        public float EvaElectricityConsumptionRate = 91.8f;

        [GameParameters.CustomFloatParameterUI("CarbonDioxide Production Rate",
            toolTip = "Per Kerbal CarbonDioxide Production Rate (units per Day).", minValue = 1f, maxValue = 300f, displayFormat = "F6", stepCount = 300)]
        public float CO2ProductionRate = 31.97978f;

        [GameParameters.CustomFloatParameterUI("Waste Production Rate",
            toolTip = "Per Kerbal Waste Production Rate (units per Day).", minValue = 0.01f, maxValue = 2f, displayFormat = "F6", stepCount = 400)]
        public float WasteProductionRate = 0.03325f;

        [GameParameters.CustomFloatParameterUI("Waste Water Production Rate",
            toolTip = "Per Kerbal Waste Water Production Rate (units per Day).", minValue = 0.1f, maxValue = 2f, displayFormat = "F6", stepCount = 400)]
        public float WasteWaterProductionRate = 0.30775f;

        
        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            if (HighLogic.fetch != null)
            {
                if (HighLogic.LoadedScene == GameScenes.MAINMENU || HighLogic.LoadedScene == GameScenes.SETTINGS ||
                    HighLogic.LoadedScene == GameScenes.SPACECENTER)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            if (parameters.CustomParams<TAC_SettingsParms>().enabled)
                return true;
            return false;
        }

        public override void OnLoad(ConfigNode node)
        {
            if (HighLogic.LoadedScene == GameScenes.MAINMENU)
            {
                if (TacStartOnce.globalSettings == null)
                {
                    TacStartOnce.Instance.Awake();
                }
                var hoursDay = GameSettings.KERBIN_TIME ? 6 : 24;
                FoodConsumptionRate = (float) (TacStartOnce.globalSettings.FoodConsumptionRate*60*60*hoursDay);
                WaterConsumptionRate = (float) (TacStartOnce.globalSettings.WaterConsumptionRate*60*60*hoursDay);
                OxygenConsumptionRate = (float) (TacStartOnce.globalSettings.OxygenConsumptionRate*60*60*hoursDay);
                BaseElectricityConsumptionRate =
                    (float) (TacStartOnce.globalSettings.BaseElectricityConsumptionRate*60*60*hoursDay);
                ElectricityConsumptionRate =
                    (float) (TacStartOnce.globalSettings.ElectricityConsumptionRate*60*60*hoursDay);
                EvaElectricityConsumptionRate =
                    (float) (TacStartOnce.globalSettings.EvaElectricityConsumptionRate*60*60*hoursDay);
                CO2ProductionRate = (float) (TacStartOnce.globalSettings.CO2ProductionRate*60*60*hoursDay);
                WasteProductionRate = (float) (TacStartOnce.globalSettings.WasteProductionRate*60*60*hoursDay);
                WasteWaterProductionRate = (float) (TacStartOnce.globalSettings.WasteWaterProductionRate*60*60*hoursDay);
            }
        }

        public override void OnSave(ConfigNode node)
        {
            var hoursDay = GameSettings.KERBIN_TIME ? 6 : 24;
            TacStartOnce.globalSettings.FoodConsumptionRate = (FoodConsumptionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.WaterConsumptionRate = (WaterConsumptionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.OxygenConsumptionRate = (OxygenConsumptionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.BaseElectricityConsumptionRate = (BaseElectricityConsumptionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.ElectricityConsumptionRate = (ElectricityConsumptionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.EvaElectricityConsumptionRate = (EvaElectricityConsumptionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.CO2ProductionRate = (CO2ProductionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.WasteProductionRate = (WasteProductionRate/60/60/hoursDay);
            TacStartOnce.globalSettings.WasteWaterProductionRate = (WasteWaterProductionRate/60/60/hoursDay);
        }
    }

    public class TAC_SettingsParms_Sec3 : GameParameters.CustomParameterNode

    {
        public override string Title { get { return "TAC LS Limits"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override bool HasPresets { get { return false; } }
        public override string Section { get { return "TAC Life Support"; } }
        public override int SectionOrder { get { return 3; } }
        
        [GameParameters.CustomIntParameterUI("Max Delta Time", toolTip = "This is the maximum time multiplier used between resource calculations.", minValue = 10000, maxValue = 200000)]
        public int MaxDeltaTime = 86400;

        [GameParameters.CustomIntParameterUI("Max Delta Time (Electricity)", toolTip = "This is the maximum time multiplier used between Electricity calculations.", minValue = 1, maxValue = 5)]
        public int ElectricityMaxDeltaTime = 1;

        [GameParameters.CustomIntParameterUI("Max time without Food", toolTip = "The maximum amount of time a Kerbal can go without food (in hours).", minValue = 72, maxValue = 900)]
        public int MaxTimeWithoutFood = 360;

        [GameParameters.CustomIntParameterUI("Max time without Water", toolTip = "The maximum amount of time a Kerbal can go without water (in hours).", minValue = 24, maxValue = 200)]
        public int MaxTimeWithoutWater = 36;

        [GameParameters.CustomIntParameterUI("Max time without Oxygen", toolTip = "The maximum amount of time a Kerbal can go without oxygen (in minutes).", minValue = 1, maxValue = 20)]
        public int MaxTimeWithoutOxygen = 2;

        [GameParameters.CustomIntParameterUI("Max time without Electricity", toolTip = "The maximum amount of time a Kerbal can go without electricity (in hours).", minValue = 2, maxValue = 200)]
        public int MaxTimeWithoutElectricity = 2;

        [GameParameters.CustomIntParameterUI("Default amount for EVA suits", toolTip = "The amount of each resource EVA suits will take with them (in units).", minValue = 0, maxValue = 90000)]
        public int EvaDefaultResourceAmount = 21600;
        
        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            if (HighLogic.fetch != null)
            {
                if (HighLogic.LoadedScene == GameScenes.MAINMENU || HighLogic.LoadedScene == GameScenes.SETTINGS || HighLogic.LoadedScene == GameScenes.SPACECENTER)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            if (parameters.CustomParams<TAC_SettingsParms>().enabled)
                return true;
            return false;
        }

        public override void OnLoad(ConfigNode node)
        {
            if (HighLogic.LoadedScene == GameScenes.MAINMENU)
            {
                if (TacStartOnce.globalSettings == null)
                {
                    TacStartOnce.Instance.Awake();
                }
                //set values, converted from per second to per Day for UI.
                MaxDeltaTime = TacStartOnce.globalSettings.MaxDeltaTime;
                ElectricityMaxDeltaTime = TacStartOnce.globalSettings.ElectricityMaxDeltaTime;
                MaxTimeWithoutFood = (int) TacStartOnce.globalSettings.MaxTimeWithoutFood/60/60;
                MaxTimeWithoutOxygen = (int) TacStartOnce.globalSettings.MaxTimeWithoutOxygen/60;
                MaxTimeWithoutWater = (int) TacStartOnce.globalSettings.MaxTimeWithoutWater/60/60;
                MaxTimeWithoutElectricity = (int) TacStartOnce.globalSettings.MaxTimeWithoutElectricity/60/60;
                EvaDefaultResourceAmount = (int) TacStartOnce.globalSettings.EvaDefaultResourceAmount;
            }
        }

        public override void OnSave(ConfigNode node)
        {
            //Convert Hourly amounts from UI back to per second.
            TacStartOnce.globalSettings.MaxDeltaTime = MaxDeltaTime;
            TacStartOnce.globalSettings.ElectricityMaxDeltaTime = ElectricityMaxDeltaTime;
            TacStartOnce.globalSettings.MaxTimeWithoutFood = MaxTimeWithoutFood*60*60;
            TacStartOnce.globalSettings.MaxTimeWithoutOxygen = MaxTimeWithoutOxygen*60;
            TacStartOnce.globalSettings.MaxTimeWithoutWater = MaxTimeWithoutWater*60*60;
            TacStartOnce.globalSettings.MaxTimeWithoutElectricity = MaxTimeWithoutElectricity*60*60;
            TacStartOnce.globalSettings.EvaDefaultResourceAmount = EvaDefaultResourceAmount;
        }
    }
}