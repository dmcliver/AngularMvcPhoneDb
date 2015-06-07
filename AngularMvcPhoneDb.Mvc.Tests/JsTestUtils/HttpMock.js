var HttpMock = (function () {

    var postCalled = false;
    var getCalled = false;
    var _postData;
    var _getData;

    function _withPostSuccessGiveData(data) {
        _postData = data;
    }

    function _withGetSuccessGiveData(data) {
        _getData = data;
    }

    var _post = function (url, data) {

        postCalled = true;

        return {

            success: function (succeedMethod) {
                succeedMethod(_postData);
            }
        };
    }
    var _verifyPostCalled = function () {
        return postCalled;
    }

    function _verifyGetCalled() {
        return getCalled;
    }

    function _get(url, opts) {

        getCalled = true;
        return {

            success: function (succeedMethod) {
                succeedMethod(_getData, {});
            }
        }
    }

    return {

        post: _post,
        get: _get,
        verifyPostCalled: _verifyPostCalled,
        verifyGetCalled: _verifyGetCalled,
        withPostSuccessGiveData: _withPostSuccessGiveData,
        withGetSuccessGiveData: _withGetSuccessGiveData
    };

})();