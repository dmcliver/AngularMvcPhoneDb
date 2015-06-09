/// <reference path="../../AngularMvcPhoneDb.Mvc/Scripts/angular.js"/>
/// <reference path="../../AngularMvcPhoneDb.Mvc/Scripts/angular-mocks.js"/>
/// <reference path="../Scripts/jasmine.js"/>
/// <reference path="../JsTestUtils/TestHarness.js"/>
/// <reference path="../JsTestUtils/HttpMock.js"/>
/// <reference path="../../AngularMvcPhoneDb.Mvc/Scripts/App/phoneController.js"/>

describe("Phone controller test", function () {

    var scope, controller, $http, phoneService, doc, loadDocument;

    beforeEach(module("PhoneApp"));

    beforeEach(function() {

        $http = jasmine.createSpyObj("$http", ["post", "get"]);
        phoneService = jasmine.createSpyObj("phoneService", ["addPhone"]);
        doc = jasmine.createSpyObj("$document", ["ready"]);

        var onReady = doc.ready.and || doc.ready.andCallFake;

        doc.ready.andCallFake(function (callbak) {
            loadDocument = callbak;
        });

        inject(function($rootScope, $controller) {

            scope = $rootScope.$new();

            controller = $controller("phoneController", {
                '$scope': scope,
                '$document': doc,
                '$http': $http,
                'phoneService': phoneService
            });
        });
    });

    it("Should set form data to initial state when user tries to add new phone", function() {

        scope.displayForm();

        expect(scope.phone.pixelWidth).toBe(0);
        expect(scope.phone.pixelHeight).toBe(0);
        expect(scope.phone.manu).toBe("");
        expect(scope.phone.model).toBe("");
        expect(scope.phoneFormDisplayable).toBe(true);
        expect(scope.phoneDisplayable).toBe(false);
    });

    it("Should retrieve data on doc ready", function () {

        var data = "ReadyToGo";

        stubGet(data);

        loadDocument();

        expect(scope.data).toBe(data);
        expect(scope.phone.pixelWidth).toBe(0);
        expect(scope.phone.pixelHeight).toBe(0);
        expect(scope.phone.manu).toBe("");
        expect(scope.phone.model).toBe("");
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

        expect(scope.genErr).toBe(false);
        expect($http.post).not.toHaveBeenCalled();
    });

    it("Should post if user tries to add phone with valid data", function () {

        var phoneId = "afe56g-def890-1ef780-u6gch";

        scope.addPhoneForm = { $pristine: false };
        scope.addPhoneForm.$invalid = false;
        scope.phone = "myPhone";
        scope.data = "lotsOfPhones";

        stubPost(phoneId);

        scope.addPhone();

        expect(scope.genErr).toBe(false);
        expect($http.post).toHaveBeenCalled();
        expect(phoneService.addPhone).toHaveBeenCalledWith("myPhone", "lotsOfPhones", phoneId);
    });

    function stubPost(phoneId) {

        var successObj = {

            success: function (callbak) {
                callbak(phoneId);
            }
        };

        if (!$http.post.and) {

            $http.post.andCallFake(function (url, data) {
                return successObj;
            });

            return;
        }

        $http.post.and.callFake(function (url, data) {
            return successObj;
        });
    }

    function stubGet(data) {

        var successObj = {

            success: function (callbak) {
                callbak(data);
            }
        };

        if (!$http.get.and) {

            $http.get.andCallFake(function (url, data) {
                return successObj;
            });

            return;
        }

        $http.get.and.callFake(function (url, data) {
            return successObj;
        });
    }
});

