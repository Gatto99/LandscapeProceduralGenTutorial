using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Noise
{
    private static float MIN_SCALE_VALUE = 0.0001f;

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset){
        float[,] noiseMap = new float[mapWidth, mapHeight];

        //dare randomicità tramite un seed
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i=0; i<octaves; i++) {
            //per non avere un valore troppo alto di PerlinNoise, definiamo un range [-10^5,10^5]
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <= 0)
            scale = MIN_SCALE_VALUE;

        // Func<float, float> sigmoid = (x => 16/(1+Mathf.Exp(-x)));

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        //Per scalare al centro e non in alto a dx
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for(int y = 0; y<mapHeight; y++){
            for(int x = 0; x<mapWidth; x++){

                float amplitude = 1;
                float frequency = 1; //tra [0,1]
                float noiseHeight = 0;

                //Aumentiamo il noiseHeight con il PelinValue di ogni octave
                for(int i = 0; i<octaves; i++){
                    // maggiore è la frequenza
                    // più i punti saranno distanti
                    // quindi height values cambiano più rapidamente
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;
                    // sampleX = sigmoid(sampleX);
                    // sampleY = sigmoid(sampleY);

                    //PerlinNoise ritorna un valore tra [0,1]
                    //in questo modo [0,1] -> [0,2] -> [-1,1]
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    //quindi il noiseHeight aumenta del perlinValue * l'amplitude dell'ottava corrente
                    noiseHeight += perlinValue * amplitude;

                    //le ottave successive avranno amplitude diminuita e frequenza aumentata
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeight){
                    maxNoiseHeight = noiseHeight;
                } else if(noiseHeight < minNoiseHeight){
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x,y] = noiseHeight;
            }
        }

        for(int y = 0; y<mapHeight; y++){
            for(int x = 0; x<mapWidth; x++){
                //InverseLerp ritorna un valore tra [0,1]
                noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);
            }
        }
        //Vogliamo far tornare i valori tra [0,1]
        return noiseMap;
    }
}
