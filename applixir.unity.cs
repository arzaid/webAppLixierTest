using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ApplixirWebGL
{
    public delegate void SimpleCallback(int val);
    [DllImport("__Internal")]
    public static extern void ShowVideo(int devId, int gameId, int
    zoneId, SimpleCallback onCompleted);
    [MonoPInvokeCallback(typeof(SimpleCallback))]
    private static void ApplixirEventHandler(int result)
    {
        Debug.Log("GOT VIDEO RESULT CALLBACK: " + result);
        PlayVideoResult pvr = PlayVideoResult.ADS_UNAVAILABLE;
        switch (result)
        {
            case 1:
                pvr = PlayVideoResult.AD_WATCHED;
                break;
            case 2:
                pvr = PlayVideoResult.NETWORK_ERROR;
                break;
            case 3:
                pvr = PlayVideoResult.AD_BLOCKER;
                break;
            case 4:
                pvr = PlayVideoResult.AD_INTERRUPTED;
                break;
            case 5:
                pvr = PlayVideoResult.ADS_UNAVAILABLE;
                break;
            case 6:
                pvr = PlayVideoResult.FB_WATCHED;
                break;
            case 7:
                pvr = PlayVideoResult.CORS_ERROR;
                break;
            case 8:
                pvr = PlayVideoResult.NO_ZONEID;
                break;
            case 9:
                pvr = PlayVideoResult.AD_STARTED;
                break;
            case 10:
                pvr = PlayVideoResult.FB_STARTED;
                break;
            case 11:
                pvr = PlayVideoResult.SYS_CLOSING;
                break;
            case 12:
                pvr = PlayVideoResult.AD_INITREADY;
                break;
            default:
                pvr = PlayVideoResult.ADS_UNAVAILABLE;
                break;
        }
        if (callback != null)
        {
            callback(pvr);
            callback = null;
        }
    }
    public enum PlayVideoResult
    {
        AD_WATCHED, // an ad was presented and completed successfully
        NETWORK_ERROR, // no connectivity available or other fatal network error
        AD_BLOCKER, // an ad blocker was detected
        AD_INTERRUPTED, // ad was ended prior to 5 seconds (abnormal end)
        ADS_UNAVAILABLE, // no ads were returned to the player from the ad servers
        FB_WATCHED,    // a fallback was presented successfully in response to ads-unavailable.
                       //  Will only occur if "fallback: 1" is set in the options.
        CORS_ERROR, // A CORS error was returned
        NO_ZONEID,  // There is no zoneId in the options
        AD_STARTED, // A video ad has been started
        FB_STARTED, // A fallback ad has been started
        SYS_CLOSING, // The system has completed all processes and is closing
        AD_INITREADY // The preload process has completed
    }
    private static Action<PlayVideoResult> callback = null;
    /// <summary>
    /// Calls out to the applixir service to show a video ad.
    ///
    /// Result is returned via the resultListerens event.
    ///
    /// </summary>
    ///
    public static void PlayVideo(Action<PlayVideoResult> callback)
    {
        ApplixirWebGL.callback = callback;
        ShowVideo(devId, gameId, zoneId, ApplixirEventHandler);
    }
    private static int devId;
    private static int gameId;
    private static int zoneId;
    // optional custom1 and custom2 are for use of game data like player ID and reward data
    // These must be setup and initialized as required by each game length limited by browser parms
    // private static string custom1; // character array or string value for game/player data
    // private static string custom2; // character array or string value for game/player data
    // private static bool dMode; // dMode determines if MD5 hash is useed server to server
    // private static bool prebid;  // enables the prebid system (contact us to enable prebid)
    // private static string pb_sec; // e.g. "facebook";
    // private static string pb_key; // e.g. "solitaire";
    // private static bool fallback;
    // private static string vSize; // "640x360" (default) or "640x480"
    // private static string vpos; // "top", "middle" or "bottom"
    // private static string vposM; // use if you want different pos for mobile - same as vpos
    public static void init(int devId, int gameId, int zoneId)
    {
        ApplixirWebGL.devId = devId;
        ApplixirWebGL.gameId = gameId;
        ApplixirWebGL.zoneId = zoneId;
        // ApplixirWebGL.custom1 = custom1; // values are encrpted AES-256-CBC client to server
        // ApplixirWebGL.custom2 = custom2: // with ports locked between client to server
        // ApplixirWebGL.dMode = dMode; // dMode determines if MD5 hash is useed server to server
        // ApplixirWebGL.prebid = prebid; // enables prebid mode
        // ApplixirWebGL.pb_sec = pb_sec; // prebid targeting section/category
        // ApplixirWebGL.pb_key = pb_key; // prebid targeting key/subject
        // ApplixirWebGL.fallback = fallback; // 0 (default) or 1
        // ApplixirWebGL.vSize = vSize; // "640x360" (default) or "640x480"
        // ApplixirWebGL.vpos = vpos;
        // ApplixirWebGL.vposM = vposM;
    }
    public static void onPlayVideoResultString(string result)
    {
        Debug.Log("GOT VIDEO RESULT CALLBACK: " + result);
        PlayVideoResult pvr = PlayVideoResult.ADS_UNAVAILABLE;
        if (!string.IsNullOrEmpty(result))
        {
            result = result.ToLower().Trim();
            switch (result)
            {
                case "ad-watched":
                    pvr = PlayVideoResult.AD_WATCHED;
                    break;
                case "network-error":
                    pvr = PlayVideoResult.NETWORK_ERROR;
                    break;
                case "ad-blocker":
                    pvr = PlayVideoResult.AD_BLOCKER;
                    break;
                case "ad-interrupted":
                    pvr = PlayVideoResult.AD_INTERRUPTED;
                    break;
                case "ads-unavailable":
                    pvr = PlayVideoResult.ADS_UNAVAILABLE;
                    break;
                case "fb-watched":
                    pvr = PlayVideoResult.FB_WATCHED;
                    break;
                case "cors-error":
                    pvr = PlayVideoResult.CORS_ERROR;
                    break;
                case "no-zoneId":
                    pvr = PlayVideoResult.NO_ZONEID;
                    break;
                case "ad-started":
                    pvr = PlayVideoResult.AD_STARTED;
                    break;
                case "fb-started":
                    pvr = PlayVideoResult.FB_STARTED;
                    break;
                case "sys-closing":
                    pvr = PlayVideoResult.SYS_CLOSING;
                    break;
                case "ad-initready":
                    pvr = PlayVideoResult.AD_INITREADY;
                    break;
                default:
                    pvr = PlayVideoResult.ADS_UNAVAILABLE;
                    break;
            }
        }
        if (callback != null)
        {
            callback(pvr);
            callback = null;
        }
    }
}
