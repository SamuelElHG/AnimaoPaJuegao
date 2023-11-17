using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CUIColorPicker : MonoBehaviour
{
    public Color Color { get { return _color; } set { Setup( value ); } }
    public void SetOnValueChangeCallback( Action<Color> onValueChange )
    {
        _onValueChange = onValueChange;
    }
    private Color _color = Color.red;
    private Action<Color> _onValueChange;
    private Action _update;
    [SerializeField]
    private GameObject material;
    [SerializeField]
    private GameObject material1;
    [SerializeField]
    private ParticleSystem francoParticle1;
    [SerializeField]
    private ParticleSystem francoParticle2;
    [SerializeField]
    private ParticleSystem francoParticle3;
    [SerializeField]
    private ParticleSystem francoParticle4;
    [SerializeField]
    private ParticleSystem francoParticle5;
    [SerializeField]
    private ParticleSystem francoParticle6;
    [SerializeField]
    private ParticleSystem francoParticle7;
    [SerializeField]
    private ParticleSystem francoParticle8;
    private static void RGBToHSV( Color color, out float h, out float s, out float v )
    {
        var cmin = Mathf.Min( color.r, color.g, color.b );
        var cmax = Mathf.Max( color.r, color.g, color.b );
        var d = cmax - cmin;
        if ( d == 0 ) {
            h = 0;
        } else if ( cmax == color.r ) {
            h = Mathf.Repeat( ( color.g - color.b ) / d, 6 );
        } else if ( cmax == color.g ) {
            h = ( color.b - color.r ) / d + 2;
        } else {
            h = ( color.r - color.g ) / d + 4;
        }
        s = cmax == 0 ? 0 : d / cmax;
        v = cmax;
    }

    private static bool GetLocalMouse( GameObject go, out Vector2 result ) 
    {
        var rt = ( RectTransform )go.transform;
        var mp = rt.InverseTransformPoint( Input.mousePosition );
        result.x = Mathf.Clamp( mp.x, rt.rect.min.x, rt.rect.max.x );
        result.y = Mathf.Clamp( mp.y, rt.rect.min.y, rt.rect.max.y );
        return rt.rect.Contains( mp );
    }

    private static Vector2 GetWidgetSize( GameObject go ) 
    {
        var rt = ( RectTransform )go.transform;
        return rt.rect.size;
    }

    private GameObject GO( string name )
    {
        return transform.Find( name ).gameObject;
    }

    private void Setup( Color inputColor )
    {
        var satvalGO = GO( "SaturationValue" );
        var satvalKnob = GO( "SaturationValue/Knob" );
        var hueGO = GO( "Hue" );
        var hueKnob = GO( "Hue/Knob" );
        var result = GO( "Result" );
        var hueColors = new Color [] {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            Color.magenta,
        };
        var satvalColors = new Color [] {
            new Color( 0, 0, 0 ),
            new Color( 0, 0, 0 ),
            new Color( 1, 1, 1 ),
            hueColors[0],
        };
        var hueTex = new Texture2D( 1, 7 );
        for ( int i = 0; i < 7; i++ ) {
            hueTex.SetPixel( 0, i, hueColors[i % 6] );
        }
        hueTex.Apply();
        hueGO.GetComponent<Image>().sprite = Sprite.Create( hueTex, new Rect( 0, 0.5f, 1, 6 ), new Vector2( 0.5f, 0.5f ) );
        var hueSz = GetWidgetSize( hueGO );
        var satvalTex = new Texture2D(2,2);
        satvalGO.GetComponent<Image>().sprite = Sprite.Create( satvalTex, new Rect( 0.5f, 0.5f, 1, 1 ), new Vector2( 0.5f, 0.5f ) );
        Action resetSatValTexture = () => {
            for ( int j = 0; j < 2; j++ ) {
                for ( int i = 0; i < 2; i++ ) {
                    satvalTex.SetPixel( i, j, satvalColors[i + j * 2] );
                }
            }
            satvalTex.Apply();
        };
        var satvalSz = GetWidgetSize( satvalGO );
        float Hue, Saturation, Value;
        RGBToHSV( inputColor, out Hue, out Saturation, out Value );
        Action applyHue = () => {
            var i0 = Mathf.Clamp( ( int )Hue, 0, 5 );
            var i1 = ( i0 + 1 ) % 6;
            var resultColor = Color.Lerp( hueColors[i0], hueColors[i1], Hue - i0 );
            satvalColors[3] = resultColor;
            resetSatValTexture();
        };
        Action applySaturationValue = () => {
            var sv = new Vector2( Saturation, Value );
            var isv = new Vector2( 1 - sv.x, 1 - sv.y );
            var c0 = isv.x * isv.y * satvalColors[0];
            var c1 = sv.x * isv.y * satvalColors[1];
            var c2 = isv.x * sv.y * satvalColors[2];
            var c3 = sv.x * sv.y * satvalColors[3];
            var resultColor = c0 + c1 + c2 + c3;
            var resImg = result.GetComponent<Image>();
            resImg.color = resultColor;
            if ( _color != resultColor ) {
                if ( _onValueChange != null ) {
                    _onValueChange( resultColor );
                }
                _color = resultColor;
                CambiarColor();
            }
        };
        applyHue();
        applySaturationValue();
        satvalKnob.transform.localPosition = new Vector2( Saturation * satvalSz.x, Value * satvalSz.y );
        hueKnob.transform.localPosition = new Vector2( hueKnob.transform.localPosition.x, Hue / 6 * satvalSz.y );
        Action dragH = null;
        Action dragSV = null;
        Action idle = () => {
            if ( Input.GetMouseButtonDown( 0 ) ) {
                Vector2 mp;
                if ( GetLocalMouse( hueGO, out mp ) ) {
                    _update = dragH;
                } else if ( GetLocalMouse( satvalGO, out mp ) ) {
                    _update = dragSV;
                }
            }
        };
        dragH = () => {
            Vector2 mp;
            GetLocalMouse( hueGO, out mp );
            Hue = mp.y / hueSz.y * 6;
            applyHue();
            applySaturationValue();
            hueKnob.transform.localPosition = new Vector2( hueKnob.transform.localPosition.x, mp.y );
            if ( Input.GetMouseButtonUp( 0 ) ) {
                _update = idle;
                CambiarColor();
            }
        };
        dragSV = () => {
            Vector2 mp;
            GetLocalMouse( satvalGO, out mp );
            Saturation = mp.x / satvalSz.x;
            Value = mp.y / satvalSz.y;
            applySaturationValue();
            satvalKnob.transform.localPosition = mp;
            if ( Input.GetMouseButtonUp( 0 ) ) {
                _update = idle;
                CambiarColor();
            }
        };
        _update = idle;
        CambiarColor();
    }

    public void SetRandomColor()
    {
        var rng = new System.Random();
        var r = ( rng.Next() % 1000 ) / 1000.0f;
        var g = ( rng.Next() % 1000 ) / 1000.0f;
        var b = ( rng.Next() % 1000 ) / 1000.0f;
        Color = new Color( r, g, b );
        CambiarColor();
    }
   
    void Awake()
    {
        Color = Color.cyan;

    }

    void Update()
    {
        _update();
        
    }


    // Puedes llamar a este método con el nuevo color que deseas aplicar al objeto
    public void CambiarColor()
    {
        material.GetComponent<Renderer>().material.SetColor("_baseColor", Color);
        material1.GetComponent<Renderer>().material.SetColor("_Color", Color);

        //Franco first particle thing
        francoParticle1.GetComponent<ParticleSystem>();

        var colorOverLifeTime = francoParticle1.colorOverLifetime;
        colorOverLifeTime.enabled = true;

        var mainModule = francoParticle1.main;
        mainModule.startColor = Color;

        Gradient gradientFranco1 = new Gradient();
        gradientFranco1.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colorOverLifeTime.color = new ParticleSystem.MinMaxGradient(gradientFranco1);

        //Franco second particle thing
        francoParticle2.GetComponent<ParticleSystem>();

        var colorOverLifeTime2 = francoParticle2.colorOverLifetime;
        colorOverLifeTime2.enabled = true;

        var mainModule2 = francoParticle1.main;
        mainModule2.startColor = Color;

        Gradient gradientFranco2 = new Gradient();
        gradientFranco2.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.2f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(1.0f, 0.6f), new GradientAlphaKey(0.0f, 0.8f) }
        );
        colorOverLifeTime2.color = new ParticleSystem.MinMaxGradient(gradientFranco2);

        //Franco third particle thing
        francoParticle3.GetComponent<ParticleSystem>();

        var colorOverLifeTime3 = francoParticle3.colorOverLifetime;
        colorOverLifeTime3.enabled = true;

        var mainModule3 = francoParticle3.main;
        mainModule3.startColor = Color;

        Gradient gradientFranco3 = new Gradient();
        gradientFranco3.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.2f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(1.0f, 0.6f), new GradientAlphaKey(0.0f, 0.8f) }
        );
        colorOverLifeTime3.color = new ParticleSystem.MinMaxGradient(gradientFranco3);

        //Franco fourth particle thing
        francoParticle4.GetComponent<ParticleSystem>();

        var colorOverLifeTime4 = francoParticle4.colorOverLifetime;
        colorOverLifeTime4.enabled = true;

        var mainModule4 = francoParticle4.main;
        mainModule4.startColor = Color;

        Gradient gradientFranco4 = new Gradient();
        gradientFranco4.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 0.06f), new GradientAlphaKey(1.0f, 0.7f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colorOverLifeTime4.color = new ParticleSystem.MinMaxGradient(gradientFranco4);

        //Franco five particle thing
        francoParticle5.GetComponent<ParticleSystem>();

        var colorOverLifeTime5 = francoParticle5.colorOverLifetime;
        colorOverLifeTime5.enabled = true;

        var mainModule5 = francoParticle5.main;
        mainModule5.startColor = Color;

        Gradient gradientFranco5 = new Gradient();
        gradientFranco5.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 0.06f), new GradientAlphaKey(1.0f, 0.7f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colorOverLifeTime5.color = new ParticleSystem.MinMaxGradient(gradientFranco5);

        //Franco six particle thing
        francoParticle6.GetComponent<ParticleSystem>();

        var colorOverLifeTime6 = francoParticle6.colorOverLifetime;
        colorOverLifeTime6.enabled = true;

        var mainModule6 = francoParticle6.main;
        mainModule6.startColor = Color;

        Gradient gradientFranco6 = new Gradient();
        gradientFranco6.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colorOverLifeTime6.color = new ParticleSystem.MinMaxGradient(gradientFranco6);

        //Franco seven particle thing
        francoParticle7.GetComponent<ParticleSystem>();

        var colorOverLifeTime7 = francoParticle7.colorOverLifetime;
        colorOverLifeTime7.enabled = true;

        var mainModule7 = francoParticle7.main;
        mainModule7.startColor = Color;

        Gradient gradientFranco7 = new Gradient();
        gradientFranco7.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colorOverLifeTime7.color = new ParticleSystem.MinMaxGradient(gradientFranco7);

        //Franco eight particle thing
        francoParticle8.GetComponent<ParticleSystem>();

        var mainModule8 = francoParticle8.main;
        mainModule8.startColor = Color;
    }

}
