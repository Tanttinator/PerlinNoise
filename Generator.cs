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
         *  int octaves: How many layers of noise are generated
         */
        public static float[,] GenerateHeightmap(int width, int height, int seed, int octaves)
        {
            if(octaves <= 0)
            {
                Debug.LogError("PerlinNoise.Generator.GenerateHeightmap: Must have more than 0 octaves!");
                return null;
            }
            //Initialize the heightmap
            float[,] heightmap = new float[width, height];

            //Initialize RNG
            System.Random prng = new System.Random(seed);

            //Random offsets for each octave
            Vector2[] octaveOffsets = new Vector2[octaves];

            for(int i = 0; i < octaves; i++)
            {
                //Offset the coordinates by a random value
                float offsetX = prng.Next(-100000, 100000);
                float offsetY = prng.Next(-100000, 100000);
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }

            //Loop through our heightmap and generate noise values for each element
            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    //The strength of the layer -- the larger amplitude, the more it will affect the height value
                    float amplitude = 1f;
                    //The scale of the layer -- the larger frequency, the grainier the noise
                    float frequency = 1f;
                    //The height at these coordinates
                    float heightValue = 0f;

                    //Generate noise layers with differing scales and strengths to add more/less detail
                    for(int i = 0; i < octaves; i++)
                    {
                        //Sample coordinates
                        float sampleX = (x + octaveOffsets[i].x) / 100f * frequency;
                        float sampleY = (y + octaveOffsets[i].y) / 100f * frequency;

                        //Get noise sample and fit it between -1 and 1
                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                        //Add noise layer to current value
                        heightValue += perlinValue * amplitude;

                        //Change our amplitude and frequency
                        amplitude *= 2f;
                        frequency *= 0.5f;
                    }

                    heightmap[x, y] = heightValue;
                }
            }

            return heightmap;
        }
    }
}
