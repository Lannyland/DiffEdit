using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ProjectConstants
    {
        #region Global constants
        public static string strMapFileLoad = @"C:\Lanny\MAMI\IPPA\Maps\DiffMaps\Small_HikerPaulDiff.csv";
        public static string strMapFileSave = @"C:\Lanny\MAMI\IPPA\Maps\DiffMaps\NewDiffMap.csv";
        public static string strTerrainImage = @"C:\Lanny\MAMI\IPPA\Maps\DiffMaps\TerrainImage.jpg";

        public static int intMapWith = 100;
        public static int intMapHeight = 100;

        #endregion


        #region Editor constants

        // Terrain related constants
        public static Vector3 TerrainLoc = new Vector3(0,0,0);
        public static Vector3 TerrainSize = new Vector3(129, 129, 50);
        public static int TerrainHeightMapResolution = 129;
                
        // Main camera related constants
        public static Vector3 MainCameraLoc = new Vector3(50, 100, 50);
        public static Vector3 MainCameraRotation = new Vector3(90, 0, 0);

        // Spot light related constants
        public static Vector3 SpotLightLoc = new Vector3(50,700,50);
        public static Vector3 SpotLightRotation = new Vector3(0, 0, 0);
        public static int SpotLightRange = 1000;
        public static float SpotLightIntensity = 2.62f;

        #endregion
    }
}
