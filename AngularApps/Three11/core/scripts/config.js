(function(){
  define(
    function(){
        return Object.freeze({ "api": "http://localhost:53551", "rtmp": "rtmp://mediavalettestvod.cloudapp.net/vod", "host": "test-dark.mediavalet.net", "upload": { "maxBlockCount": 50000, "maxFileSize": 200000000000, "minChunkSize": 256000, "maxChunkSize": 4000000 }, "logos": { "medium": "https://az687986.vo.msecnd.net/logos/mv_medium.png", "large": "https://az687986.vo.msecnd.net/logos/mv_large.png" } });
    });
}).call();