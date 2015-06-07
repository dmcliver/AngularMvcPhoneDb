/// <reference path="../Scripts/angular.js"/>
/// <reference path="../Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/jasmine.js"/>
/// <reference path="../JsTestUtils/TestHarness.js"/>
/// <reference path="../JsTestUtils/HttpMock.js"/>
/// <reference path="../JsTestUtils/PhoneServiceMock.js"/>
/// <reference path="../JsTestUtils/DocumentStub.js"/>
/// <reference path="../../AngularMvcPhoneDb.Mvc/Scripts/App/phoneController.js"/>

describe("Phone controller test", function () {

    var scope, controller;

    beforeEach(module("PhoneApp"));

    beforeEach(

        inject(function ($rootScope, $controller) {

            scope = $rootScope.$new();

            controller = $controller;
            controller(
                "phoneController", {
                    '$scope': scope,
                    '$document': DocumentStub,
                    '$http': HttpMock,
                    'phoneService': PhoneServiceMock
                }
            );
        })
    );

    it("Should retrieve data on doc ready", function () {

        var data = "ReadyToGo";

        HttpMock.withGetSuccessGiveData(data);

        var callback = DocumentStub.getCallback();
        callback();

        expect(scope.data).toBe(data + "");
    });

    it("Should give error if user tries to add phone with no data", function () {

        scope.addPhoneForm = { $pristine: true };
        scope.addPhone();

        expect(scope.genErr).toBe("Please enter some data");
    });

    it("Should give error if user tries to add phone with invalid data", function () {

        scope.addPhoneForm = { $pristine: false };
        scope.addPhoneForm.$invalid = true;
        scope.addPhone();

        expect(scope.genErr).toBeFalsy();
        expect(HttpMock.verifyPostCalled()).toBe(false);
    });

    it("Should post if user tries to add phone with valid data", function () {

        var phoneId = "afe56g-def890-1ef780-u6gch";

        scope.addPhoneForm = { $pristine: false };
        scope.addPhoneForm.$invalid = false;
        scope.phone = "myPhone";
        scope.data = "lotsOfPhones";

        HttpMock.withPostSuccessGiveData(phoneId);

        scope.addPhone();

        expect(scope.genErr).toBeFalsy();
        expect(HttpMock.verifyPostCalled()).toBe(true, "that $http::post method was called");
        expect(PhoneServiceMock.verifyAddPhone("myPhone", "lotsOfPhones", phoneId)).toBe(true, "that addPhone(myPhone, lotsOfPhones, " + phoneId + ") was called");
    });
});

