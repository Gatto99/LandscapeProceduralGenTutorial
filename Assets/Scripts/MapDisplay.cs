using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;

    public void DrawTexture(Texture2D texture){

        //non uso Texture.material che è istanziato a RUNTIME
        //ma voglio che venga renderizzato nell'EDITOR
        textureRenderer.sharedMaterial.mainTexture = texture;
        //set plane size to the noiseMap
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    // public void DrawNoiseMap(float[,] noiseMap, Color lbColor, Color ubColor){
    //     int width = noiseMap.GetLength(0);
    //     int height = noiseMap.GetLength(1);

    //     Texture2D texture = new Texture2D(width, height);

    //     // Metto in un array monodimensionale i colori 
    //     // da associare ai pixel
    //     Color[] colourMap = new Color[width * height];
    //     for(int y=0; y<height; y++){
    //         for(int x=0; x<width; x++){
    //             // y * width = indice di riga
    //             // x = indice di colonna
    //             colourMap[y * width + x] = Color.Lerp(lbColor, ubColor, noiseMap[x,y]);
    //             // float enne = noiseMap[x,y];
    //             // Color color;
    //             // if(enne < 0.3)
    //             //     color = Color.blue;
    //             // else 
    //             //     color = Color.green;
    //             // colourMap[y * width + x] = color;
    //         }
    //     }
    //     texture.SetPixels(colourMap);
    //     texture.Apply();

    //     //non uso Texture.material che è istanziato a RUNTIME
    //     //ma voglio che venga renderizzato nell'EDITOR
    //     textureRenderer.sharedMaterial.mainTexture = texture;
    //     //set plane size to the noiseMap
    //     textureRenderer.transform.localScale = new Vector3(width, 1, height);
    // }
}
