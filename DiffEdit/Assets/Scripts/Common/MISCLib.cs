using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using rtwmatrix;
using System;
using System.Text;
using System.IO;

namespace Assets.Scripts.Common
{
    class MISCLib
    {
        // Read in a csv file and then store that into matrix
        public static RtwMatrix LoadMap(string FileInName)
        {
            // Read file one line at a time and store to list.
            FileStream file = new FileStream(FileInName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string strLine;
            List<string[]> lstRow = new List<string[]>();
            // Loop through lines
            while (sr.Peek() >= 0)
            {
                strLine = sr.ReadLine().Trim();
                if (strLine.Length > 1)
                {
                    string[] splitData = strLine.Split(',');
                    lstRow.Add(splitData);
                }
            }
            sr.Close();
            file.Close();
            // Create matrix
            RtwMatrix mGrid = new RtwMatrix(lstRow.Count, lstRow[0].Length);
            for (int i = 0; i < mGrid.Rows; i++)
            {
                for (int j = 0; j < mGrid.Columns; j++)
                {
                    mGrid[i, j] = (float)(Convert.ToDouble(lstRow[i][j]));
                }
            }
            return mGrid;
        }
	
		// Save mesh vertices to a csv file
        public static void SaveMap(string FileOutName, RtwMatrix mMap)
        {
            //  Delete file if it already exists
            if (File.Exists(FileOutName))
            {
                File.Delete(FileOutName);
            }

			// Write file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileOutName))
            {
                for (int i = 0; i < mMap.Rows; i++)
                {
                    string strLine = "";
                    for (int j = 0; j < mMap.Columns; j++)
                    {
                        strLine += mMap[i, j] + ",";
                    }
                    strLine = strLine.Substring(0, strLine.Length - 1);
                    file.WriteLine(strLine);
                }
            }
        }
		
        // Scale image values between 0 and 255
        public static void ScaleImageValues(ref RtwMatrix imgin)
        {
            float[] MinMax = imgin.MinMaxValue();
            float max = MinMax[1];
            if (max != 0)
            {
                for (int y = 0; y < imgin.Rows; y++)
                {
                    for (int x = 0; x < imgin.Columns; x++)
                    {
                        imgin[y, x] = imgin[y, x] / max * 255;
                    }
                }
            }
            else
            {
                for (int y = 0; y < imgin.Rows; y++)
                {
                    for (int x = 0; x < imgin.Columns; x++)
                    {
                        imgin[y, x] = imgin[y, x] / max * 255;
                    }
                }
            }
        }
	
		// Convert RtwMatrix to vertice array
		public static Vector3[] MatrixToArray(RtwMatrix mMap)
		{
			Vector3[] vertices = new Vector3[mMap.Columns * mMap.Rows];
			int index = 0;
			for (int i=0; i<mMap.Rows; i++)
			{
				for (int j=0; j<mMap.Columns; j++)
				{
					vertices[index] = new Vector3(Convert.ToSingle(j)/10-5, mMap[i, j], Convert.ToSingle(i)/10-5);
					index++;
				}
			}
			return vertices;
		}
		
		// Convert vertice array to RtwMatrix
		public static RtwMatrix ArrayToMatrix(Vector3[] vertices)
		{
			// Assuming a square mesh
			int width = Convert.ToInt16(Math.Sqrt(vertices.Length));
			RtwMatrix mMap = new RtwMatrix(width, width);
			int index = 0;
			for (int i=0; i<mMap.Rows; i++)
			{
				for (int j=0; j<mMap.Columns; j++)
				{
					mMap[i,j] = vertices[index].y;
					index++;
				}
			}
			return mMap;			
		}
		
		// Apply Difficulty Color Map
		public static Color[] ApplyDiffColorMap(Vector3[] vs)			
		{
			Color[] colors = new Color [vs.Length];
			int index = 0;
			for(int i=0; i<vs.Length; i++)
			{
				if(vs[index].y == 0f)
				{
					colors[index]=Color.blue;
				}
				else if(vs[index].y == 1f)
				{
					colors[index]=Color.green;
				}
				else if(vs[index].y == 2f)
				{
					colors[index]=Color.red;
				}
				else
				{
						colors[index]=Color.black;
				}
				index++;
			}
			return colors;
		}
		
		// Flip map to draw correctly in Diff Editor
		public static RtwMatrix FlipTopBottom(RtwMatrix map)
		{
			RtwMatrix result = new RtwMatrix(map.Rows, map.Columns);
			for (int i=0; i<map.Rows; i++)
			{
				for (int j=0; j<map.Columns; j++)
				{
					result[i,j] = map[map.Rows - 1 -i, j];
				}
			}
			return result;
		}
		
		// Find all points within Euclidian distance r from Point(x,y)
		public static Vector2[] PointInCircle(int x, int y, int r)
		{
			// Find box size and only evaluate things in box
			int offset = (int)(r/2);
			int minX = 0;
			int maxX = x;
			int minY = 0; 			
			int maxY = 0;
			int l = Assets.Scripts.ProjectConstants.intMapWith;
			
			minX = Mathf.Clamp(x-offset, 0, l);
			maxX = Mathf.Clamp(x+offset, 0, l);
			minY = Mathf.Clamp(x-offset, 0, l);
			maxY = Mathf.Clamp(x+offset, 0, l);
			

			for(int i=x-offset; i<x; i++)
			{
				for (int j=0; j<y; j++)
				{
					
				}
			}
			// So I need current size
			
			Vector2[] result = new Vector2[10];			
			return result;
		}
		
        // Calculating Euclidian Distance
        public static double EuclidianDistance(int x1, int y1, int x2, int y2)
        {
            double distance = Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));
            return distance;
        }

	}
}