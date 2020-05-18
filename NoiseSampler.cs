using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PerlinNoise
{
    public class NoiseSampler
    {
        Settings settings;
        System.Random random;
        Vector2[] octaveOffsets;
        float maxPossibleHeight;

        public NoiseSampler(Settings settings, int seed)
        {
            this.settings = settings;
            random = new System.Random(seed);

            octaveOffsets = new Vector2[settings.octaves];

            float amplitude = 1f;
            maxPossibleHeight = 0f;

            for(int i = 0; i < settings.octaves; i++)
            {
                float offsetX = random.Next(-100000, 100000);
                float offsetY = random.Next(-100000, 100000);
                octaveOffsets[i] = new Vector2(offsetX, offsetY);

                maxPossibleHeight += amplitude;
                amplitude *= settings.persistence;
            }
        }

        public float Sample(int x, int y)
        {
            float amplitude = 1f;
            float frequency = 1f;
            float heightValue = 0f;

            for (int i = 0; i < settings.octaves; i++)
            {
                float sampleX = (x + octaveOffsets[i].x) / settings.scale * frequency;
                float sampleY = (y + octaveOffsets[i].y) / settings.scale * frequency;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                heightValue += perlinValue * amplitude;

                amplitude *= settings.persistence;
                frequency *= settings.lacunarity;
            }

            return (heightValue + 1) / (maxPossibleHeight / 0.9f);
        }
    }
}
