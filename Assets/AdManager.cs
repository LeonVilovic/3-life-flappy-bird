using GoogleMobileAds.Api;
using System.IO;
using UnityEngine;

[System.Serializable]
public class AdConfig
{
    public string adUnitId;
}
public class AdManager : MonoBehaviour
{
    private static AdManager instance = null;
    private AdManager() { }
    public static AdManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AdManager();
            return instance;
        }
    }

    private AdConfig adConfig;

    private InterstitialAd interstitial;
    private const string adUnitId = "ca-app-pub-3940256099942544/1033173712"; // Test ID
    //private const string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID

    public void LoadInterstitialAd()
    {
        // Load JSON from StreamingAssets
        string path = Path.Combine(Application.streamingAssetsPath, "adConfig.json");
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            adConfig = JsonUtility.FromJson<AdConfig>(jsonString);
            Debug.Log("Loaded AdConfig JSON: " + jsonString);
        }
        else
        {
            Debug.LogError("adConfig.json not found at path: " + path);
            return;
        }

        var adRequest = new AdRequest();

        InterstitialAd.Load(adConfig.adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError($"Interstitial failed to load: {error.GetMessage()}");
                    return;
                }

                if (ad == null)
                {
                    Debug.LogError("Interstitial failed to load: ad is null");
                    return;
                }

                // Assign the loaded ad to your member variable
                interstitial = ad;

                // Register event handlers for this ad
                interstitial.OnAdFullScreenContentOpened += () => Debug.Log("Ad opened");
                interstitial.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Ad closed, loading a new one...");
                    interstitial.Destroy();
                    interstitial = null;
                    LoadInterstitialAd(); // preload next ad
                };

                Debug.Log("Interstitial ad loaded successfully!");
            });
    }

    public void ShowInterstitialAd()
    {
        //if (interstitial != null)
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
            Debug.Log("ShowInterstitialAd() - interstitial.Show(); triggerd");
        }
        else
        {
            Debug.Log("Interstitial Ad not ready to show yet.");
        }
    }

    private void OnDestroy()
    {
        interstitial?.Destroy();
    }
}
