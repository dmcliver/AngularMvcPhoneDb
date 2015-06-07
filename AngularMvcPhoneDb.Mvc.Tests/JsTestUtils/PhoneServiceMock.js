var PhoneServiceMock = (function () {

    var phone, data, id;

    function _addPhone(_phone, _data, _id) {

        phone = _phone;
        data = _data;
        id = _id;
    }

    function _verifyAddPhone(_phone, _data, _id) {
        return _phone === phone && _data === data && _id === id;
    }

    return {

        addPhone: _addPhone,
        verifyAddPhone: _verifyAddPhone
    };

})();