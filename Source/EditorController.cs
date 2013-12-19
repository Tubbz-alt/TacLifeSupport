﻿/**
 * Thunder Aerospace Corporation's Life Support for Kerbal Space Program.
 * Written by Taranis Elsu.
 * 
 * (C) Copyright 2013, Taranis Elsu
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tac
{
    class EditorController : MonoBehaviour, Savable
    {
        private Icon<EditorController> icon;
        private BuildAidWindow window;

        void Awake()
        {
            this.Log("Awake");
            icon = new Icon<EditorController>(new Rect(Screen.width * 0.25f, 0, 32, 32), "icon.png", "LS",
                "Click to show the Life Support Build Aid", OnIconClicked, "EditorIcon");
            window = new BuildAidWindow(TacLifeSupport.Instance.globalSettings);
        }

        void Start()
        {
            this.Log("Start");
            if (TacLifeSupport.Instance.gameSettings.Enabled)
            {
                icon.SetVisible(true);
            }
            else
            {
                icon.SetVisible(false);
                window.SetVisible(false);
            }
        }

        public void Load(ConfigNode globalNode)
        {
            icon.Load(globalNode);
            window.Load(globalNode);
        }

        public void Save(ConfigNode globalNode)
        {
            icon.Save(globalNode);
            window.Save(globalNode);
        }

        private void OnIconClicked()
        {
            window.ToggleVisible();
        }
    }
}