using System;
using System.Collections.Generic;

namespace AngularMvcPhoneDb.Core.DomainDto
{
    public class PhoneManufacturerDto
    {
        public PhoneManufacturerDto(string manufacturerName, IEnumerable<PhoneSummaryDto> phoneDto)
        {
            ManufacturerName = manufacturerName;
            PhoneDto = phoneDto;
        }

        public String ManufacturerName { get; private set; }
        public IEnumerable<PhoneSummaryDto> PhoneDto { get; private set; }
    }
}
