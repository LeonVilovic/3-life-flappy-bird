using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    private int screenshotIndex = 1;

    void Update()
    {
        // Press "S" or touch to take a screenshot
        if (Input.GetKeyDown(KeyCode.S) || Input.touchCount > 0)
        //if (Input.GetKeyDown(KeyCode.S))
        {
            string fileName = $"screenshot{screenshotIndex.ToString("D3")}.png"; // D3 = 3 digits with leading zeros
            //string path = Application.dataPath + "/" + fileName;
            string path = "C:\\Users\\vilov\\Desktop\\sc\\" + fileName;

            ScreenCapture.CaptureScreenshot(path);
            Debug.Log("Screenshot saved to: " + path);

            screenshotIndex++;
        }
    }
}
