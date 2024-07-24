var ApplixirPlugin = {
    $impl: {},
    adStatusCallbackX: function (status) {
        //console.log('Ad Status:', status);
        var iresult = 0;
        switch (status) {
            case "ad-watched":
                iresult = 1;
                break;
            case "network-error":
                iresult = 2;
                break;
            case "ad-blocker":
                iresult = 3;
                break;
            case "ad-interrupted":
                iresult = 4;
                break;
            case "ads-unavailable":
                iresult = 5;
                break;
            case "fb-watched":
                iresult = 6;
                break;
            case "cors-error":
                iresult = 7;
                break;
            case "no-zoneId":
                iresult = 8;
                break
            case "ad-started":
                iresult = 9;
                break;
            case "fb-started":
                iresult = 10;
                break;
            case "sys-closing":
                iresult = 11;
                break;
            case "ad-initready":
                iresult = 12;
                break;
            default:
                iresult = 0;
                break;
        }
        Runtime.dynCall('vi', window.applixirCallback,
            [iresult]);
        //console.log('Ad Status done:', status);
    },
    ShowVideo__deps: [
        '$impl',
        'adStatusCallbackX'
    ],

    // Adjust this call & parameters to match the options you are using and the
    // approach that works best for passing your values to them (remove unused parms)
    ShowVideo: function (dev, game, zone, cv1, cv2, dmode, prebd, pbs, pbk, fbck, ver, callback) {
        var local_options = {
            zoneId: zone, // the zone ID from the "Games" page
            devId: dev, // your developer ID from the "Account" page
            gameId: game, // the ID for this game from the "Games" page
            custom1: cv1, // this is used for custom game values such as player ID
            custom2: cv2, // this is used for custom game values such as reward info
            dMode: dmode, // determines if you want an MD5 hash at your https end-point (see docs)
            prebid: prebd, // enables prebid mode
            pb_sec: pbs, // prebid targeting section/category
            pb_key: pbk, // prebid targeting key/subject
            adStatusCb: _adStatusCallbackX, // optional
            fallback: fbck, // if '1' then fallback will be shown if no ads are available
            verbosity: ver // 0..5
        };
        //console.log(local_options);
        window.applixirCallback = callback;
        invokeApplixirVideoUnit(local_options);
    }
};
autoAddDeps(ApplixirPlugin, '$impl');
mergeInto(LibraryManager.library, ApplixirPlugin);
