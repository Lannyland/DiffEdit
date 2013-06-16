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
        // public static string strTerrainImage = @"http://lannyland.com/images/LannylandTechHover.png";
		public static string strTerrainImage = @"C:\Lanny\MAMI\IPPA\Maps\DiffMaps\TerrainImage.jpg";

        public static int intMapWith = 100;
        public static int intMapHeight = 100;

        #endregion

		
        #region Editor constants
		
		public static int diffLevel = 1;	// 1: Easy 2: Medium 3: Hard
		public static int navMode = 1;		// 1: Rotate 2: Pan
		public static int editMode = 1;		// 1: Paint 2: Select
		public static int brushSize = 1;	// Paint Brush Size from 1 to 10

        #endregion
    }
}
