using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerlinNoise
{
    //Base class for perlin noise generation
    public class Generator
    {
        /**Generates a 2D array of noise values
         * args:
         *  int width: X size of the array
         *  int height: Y size of the array
         */
        public static float[,] GenerateHeightmap(int width, int height)
        {
            //Initialize the heightmap
            float[,] heightmap = new float[width, height];

            //Loop through our heightmap and generate noise values for each element
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    heightmap[x, y] = Sample(x, y);
                }
            }

            return heightmap;
        }

        public static float Sample(int x, int y)
        {
            return Mathf.PerlinNoise(x / 100f, y / 100f);
        }
    }
}
