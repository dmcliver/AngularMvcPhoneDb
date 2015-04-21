using System;
using System.Collections.Generic;
using System.Web.Http;
using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Core.DomainDto;
using AngularMvcPhoneDb.Core.Repositories;
using AngularMvcPhoneDb.Mvc.Models;

namespace AngularMvcPhoneDb.Mvc.Controllers
{
    public class DataController : ApiController
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISmartPhoneBuilder _smartPhoneBuilder;
        private readonly IPhoneRepository _phoneRepository;

        public DataController(IManufacturerRepository manufacturerRepository, ISmartPhoneBuilder smartPhoneBuilder, IPhoneRepository phoneRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _smartPhoneBuilder = smartPhoneBuilder;
            _phoneRepository = phoneRepository;
        }

        public DataController() : this(new ManufacturerRepository(), new SmartPhoneBuilder(), new PhoneRepository())
        {
        }

        // GET api/data
        public IEnumerable<PhoneManufacturerDto> Get()
        {
            return _manufacturerRepository.LoadAllWithPhones();
        }

        public SmartPhone Get(Guid id)
        {
            return _phoneRepository.LoadById(id);
        }

        // POST api/data
        public void Post([FromBody]AddPhoneModel model)
        {
                Manufacturer manufacturer = !_manufacturerRepository.Any(model.manu) ? 
                                            new Manufacturer {Name = model.manu} : 
                                            _manufacturerRepository.LoadById(model.manu);

                var phone = _phoneRepository.Create();
                _smartPhoneBuilder.BuildFromModel(phone, model, manufacturer);
                _phoneRepository.Add(phone);
        }
    }
}
