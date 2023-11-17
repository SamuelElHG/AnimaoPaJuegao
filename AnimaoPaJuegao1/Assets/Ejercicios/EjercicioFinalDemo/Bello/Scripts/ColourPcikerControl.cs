using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourPcikerControl : MonoBehaviour
{
    [SerializeField]
    private float currentHue, currentSat, currentVal;
    [SerializeField]
    private RawImage hueImage, satValImage, outputImage;
    [SerializeField]
    private Slider hueSlider;
    [SerializeField]
    private Texture2D hueTexture, svTexture, outputTexture;
    [SerializeField]
    MeshRenderer changeThisColor;

    private void Start()
    {
        CreatehueImage();
        CreateSVImage();
        CreateOutputImage();
        UpdateOutputImage();
    }
    private void CreatehueImage()
    {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 0.05f));
        }
        hueTexture.Apply();
        currentHue = 0;
        hueImage.texture = hueTexture;
    }
    private void CreateSVImage()
    {
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SatValTexture";
        for(int y = 0; y < svTexture.height; y++)
        {
            for(int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x,y, Color.HSVToRGB(
                    currentHue,
                    (float)x/svTexture.width,
                    (float)y/svTexture.height));
            }
        }
        svTexture.Apply();
        currentSat = 0;
        currentVal = 0;

        satValImage.texture = svTexture;

    }
    private void CreateOutputImage()
    {
        outputTexture = new Texture2D(1, 16);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "OutputTexture";

        Color currentColour = Color.HSVToRGB(currentHue,currentSat,currentVal);
        for(int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0,i, currentColour);
        }
        outputTexture.Apply();
        outputImage.texture = outputTexture;

    }
    private void UpdateOutputImage()
    {
        Color currentColour = Color.HSVToRGB(currentHue,currentSat,currentVal);
        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColour);
        }
        outputTexture.Apply();
        changeThisColor.material.SetColor("_BaseColor", currentColour);
    }
    public void SetSV(float S, float V)
    {
        currentSat = S;
        currentVal = V;
        UpdateOutputImage();
    }
    public void UpdateSVImage() 
    {
        currentHue = hueSlider.value;
        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(
                   currentHue,
                   (float)x / svTexture.width,
                   (float)y / svTexture.height));
            }
        }
        svTexture.Apply();
        UpdateOutputImage();
    }
}
