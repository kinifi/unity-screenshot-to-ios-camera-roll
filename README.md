# unity-screenshot-to-ios-camera-roll
save a screenshot of your unity game and send it to the iOS camera roll


```csharp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SaveToAlbumNative.Init();
		SaveToAlbumNative.ReceivedOnCompleteEvent.AddListener(SavedImage);

		StartCoroutine(UploadPNG());
	}

	private void SavedImage() {
		Debug.Log("Unity On Complete Saved Image");
	}

	IEnumerator UploadPNG()
    {
        
		Debug.Log("waiting for 3 seconds");
		yield return new WaitForSeconds(3.0f);

		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
		SaveToAlbumNative.SaveImage(bytes);
        
		Object.Destroy(tex);

    }

	void OnDestroy() {
		//nil our values in objc to save memory leaks
		SaveToAlbumNative.deInit();
	}
	
}

```
