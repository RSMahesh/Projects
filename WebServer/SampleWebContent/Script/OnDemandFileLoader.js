(function (window) {
    //Public Methods
    loadFile = function (file) {
        switch (getFileType(file).toUpperCase()) {
            case "JS":
                loadJsFile(file);
                break;
            case "CSS":
                loadCssFile(file);
                break;
            default:
               alert("FileLoader Error : Ivalid File Type");
        }

    }

    unLoadFile = function (file) {
        switch (getFileType(file).toUpperCase()) {
            case "JS":
                unLoadJsFile(file);
                break;
            case "CSS":
                unLoadCssFile(file);
                break;
            default:
                alert("FileLoader Error : Ivalid File Type");
        }
    }

    //Private Methods
    var getFileType = function (filename) {
        return filename.split('.').pop();
    },

        loadJsFile = function (file) {
            if (isAlreadyFileLoaded("script", "src", file)) {
             //   alert('Already Loaded JS');
                return;
            }
            var fileref = document.createElement('script')
            fileref.setAttribute("type", "text/javascript")
            fileref.setAttribute("src", file)
            appendFileToHead(fileref);
        },


        loadCssFile = function (file) {
            if (isAlreadyFileLoaded("link", "href", file)) {
               // alert('Already Loaded CSS');
                return;
            }
            var fileref = document.createElement("link")
            fileref.setAttribute("rel", "stylesheet")
            fileref.setAttribute("type", "text/css")
            fileref.setAttribute("href", file)
            appendFileToHead(fileref);
        },


        unLoadJsFile = function (file) {
            removeFileFromHead("script", "src", file)
        },
        unLoadCssFile = function (file) {
            removeFileFromHead("link", "href", file)
        },

        appendFileToHead = function (fileref) {
            if (typeof fileref != "undefined")
                document.getElementsByTagName("head")[0].appendChild(fileref)
        },

        isAlreadyFileLoaded = function (targetelement, targetattr, file) {
            var allsuspects = document.getElementsByTagName(targetelement)
            for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
                if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(file) != -1)
                    return true;
            }
            return false;
        },

        removeFileFromHead = function (targetelement, targetattr, file) {
            var allsuspects = document.getElementsByTagName(targetelement)
            for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
                if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(file) != -1)
                    allsuspects[i].parentNode.removeChild(allsuspects[i]) //remove element by calling parentNode.removeChild()
            }
        }

    window.fileLoader = this;

})(window);