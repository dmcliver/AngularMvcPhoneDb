var DocumentStub = (function () {

    var mthd = null;

    function _ready(mth) {
        mthd = mth;
    }
    function _getCallback() {
        return mthd;
    }

    return {

        ready: _ready,
        getCallback: _getCallback
    };

})();