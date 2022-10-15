using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[RequireComponent(typeof(GUIText))]
public class TestExif : MonoBehaviour
{

    public string imagePath = "";
    public RawImage cachedTexture = null;
    private Texture2D texture = null;

    void Awake()
    {
        //this.cachedTexture = GetComponent<RawImage>();
    }

    void Start()
    {
        StartCoroutine(LoadTexture());
    }

    IEnumerator LoadTexture()
    {
        yield return StartCoroutine(GetImage(this.imagePath));
        //if (this.cachedTexture)
        //{
        //    this.cachedTexture.texture = this.texture;
        //}
    }


    /// <summary>
    /// ExifLib - http://www.codeproject.com/Articles/47486/Understanding-and-Reading-Exif-Data
    /// </summary>
    IEnumerator GetImage(string url)
    {
        WWW www = new WWW(url);
        Debug.Log("Fetching image " + url);
        yield return www;
        if (!System.String.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            this.texture = null;
        }
        else
        {
            Debug.Log("Finished Getting Image -> SIZE: " + www.bytes.Length.ToString());
            ExifLib.JpegInfo jpi = ExifLib.ExifReader.ReadJpeg(www.bytes, "Foo");
            Debug.Log("EXIF: " + jpi.Orientation.ToString());
            Debug.Log("EXIF: " + jpi.Model);
            cachedTexture.texture = www.texture;
            cachedTexture.SetNativeSize();
        }
    }
}
