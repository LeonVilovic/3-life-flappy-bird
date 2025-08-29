using UnityEngine;

public class MainMenuAdsInit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AdManager.Instance.InitializeMobileAds();
        AdManager.Instance.EnableRaiseAdEventsOnUnityMainThread();
        //AdManager.Instance.ShowAdInspector();
        AdManager.Instance.BannerAdLoadAndShow();
        AdManager.Instance.InterstitialAdLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
