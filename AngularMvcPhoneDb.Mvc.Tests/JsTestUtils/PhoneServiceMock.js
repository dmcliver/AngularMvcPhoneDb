var PhoneServiceMock = (function () {

    var _phone, _data, _id;

    function _addPhone(phone, data, id) {

        _phone = phone;
        _data = data;
        _id = id;
    }

    function _verifyAddPhone(phone, data, id) {
        return _phone === phone && _data === data && _id === id;
    }

    return {

        addPhone: _addPhone,
        verifyAddPhone: _verifyAddPhone
    };

})();