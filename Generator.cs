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
         *  int seed: Seed for RNG
         */
        public static float[,] GenerateHeightmap(int width, int height, int seed)
        {
            //Initialize the heightmap
            float[,] heightmap = new float[width, height];

            //Initialize RNG
            System.Random prng = new System.Random(seed);

            //Offset the coordinates by a random value
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);

            //Loop through our heightmap and generate noise values for each element
            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    heightmap[x, y] = Mathf.PerlinNoise(((float)x + offsetX) / 100f, ((float)y + offsetY) / 100f);
                }
            }

            return heightmap;
        }
    }
}
