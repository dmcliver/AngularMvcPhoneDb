app.service('phoneService', function() {

    this.addPhone = function(phone, data, id) {

        var model = ko.utils.arrayFirst(data, function (d) {
            return d.ManufacturerName.toLowerCase() === phone.manu.toLowerCase();
        });

        if (model) 
            model.PhoneDto.push({ModelName: phone.model, Id: id});
        else 
            data.push({ ManufacturerName: phone.manu, PhoneDto: [{ ModelName: phone.model, Id: id }] });
    };
});
