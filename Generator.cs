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
         *  float scale: How grainy the noise is (larger = smoother)
         *  float persistence: How much amplitude changes between octaves (smaller = smoother, recommended range: ]0f, 1f], default value = 0.5f)
         *  float lacunarity: How much frequency changes between octaves (smaller = smoother, recommended range: [1f, 16f], default value = 2f)
         *  Vector2 offset: Manual offset (default value = (0, 0))
         */
        public static float[,] GenerateHeightmap(int width, int height, int seed, int octaves, float scale, float persistence, float lacunarity, Vector2 offset)
        {
            if(octaves <= 0)
            {
                Debug.LogError("PerlinNoise.Generator.GenerateHeightmap: Must have more than 0 octaves!");
                return null;
            }

            //Make sure scale is greater than 0
            if (scale <= 0)
                scale = 0.00001f;

            //Initialize the heightmap
            float[,] heightmap = new float[width, height];

            //Initialize RNG
            System.Random prng = new System.Random(seed);

            //Random offsets for each octave
            Vector2[] octaveOffsets = new Vector2[octaves];

            for(int i = 0; i < octaves; i++)
            {
                //Offset the coordinates by a random value
                float offsetX = prng.Next(-100000, 100000) + offset.x;
                float offsetY = prng.Next(-100000, 100000) + offset.y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }

            //Zoom to the center when changing width and height
            float halfWidth = width / 2f;
            float halfHeight = height / 2f;

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
                        float sampleX = (x + halfWidth + octaveOffsets[i].x) / scale * frequency;
                        float sampleY = (y + halfHeight + octaveOffsets[i].y) / scale * frequency;

                        //Get noise sample and fit it between -1 and 1
                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                        //Add noise layer to current value
                        heightValue += perlinValue * amplitude;

                        //Change our amplitude and frequency
                        amplitude *= persistence;
                        frequency *= lacunarity;
                    }

                    heightmap[x, y] = heightValue;
                }
            }

            return heightmap;
        }

        /**Generates a 2D array of noise values vith given settings object
         * args:
         *  int width: X size of the array
         *  int height: Y size of the array
         *  int seed: Seed for RNG
         *  Settings settings: The noise settings
         *  Vector2 offset: Manual offset
         */
        public static float[,] GenerateHeightmap(int width, int height, int seed, Settings settings, Vector2 offset)
        {
            return GenerateHeightmap(width, height, seed, settings.octaves, settings.scale, settings.persistence, settings.lacunarity, offset);
        }
    }
}
