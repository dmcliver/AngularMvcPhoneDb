/// <reference path="../../AngularMvcPhoneDb.Mvc/Scripts/knockout-2.2.0.js"/>
/// <reference path="../Scripts/angular.js"/>
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/jasmine.js"/>
/// <reference path="../JsTestUtils/TestHarness.js"/>
/// <reference path="../JsTestUtils/HttpMock.js"/>
/// <reference path="../JsTestUtils/DocumentStub.js"/>
/// <reference path="../../AngularMvcPhoneDb.Mvc/Scripts/App/phoneService.js"/>

describe("Phone service test", function () {

    var service;

    beforeEach(module("PhoneApp"));

    beforeEach(

        inject(function (_phoneService_) {
            service = _phoneService_;
        })
    );

    it("Should add phone to data", function () {

        var model = "l1";

        var guid = "afe56g-def890-1ef780-u6gch";
        var data = [{ ManufacturerName: "lg", PhoneDto: [] }];
        var phone = { manu: "lG", model: model };

        service.addPhone(phone, data, guid);
        var len = data[0].PhoneDto.length;
        var phoneDto = data[0].PhoneDto[0];

        expect(len).toEqual(1);
        expect(phoneDto.ModelName).toEqual(model);
        expect(phoneDto.Id).toEqual(guid);
        expect(data.length).toEqual(1);
        expect(data[0].ManufacturerName).toBe("lg");
    });

    it("Should add new data for new smartphone company", function() {

        var manu = "LG";
        var model = "l1";

        var guid = "afe56g-def890-1ef780-u6gch";
        var data = [];
        var phone = { manu: manu, model: model };

        service.addPhone(phone, data, guid);
        var len = data[0].PhoneDto.length;
        var phoneDto = data[0].PhoneDto[0];

        expect(data.length).toEqual(1);
        expect(data[0].ManufacturerName).toEqual(manu);
        expect(len).toEqual(1);
        expect(phoneDto.ModelName).toEqual(model);
        expect(phoneDto.Id).toEqual(guid);
    });
});

