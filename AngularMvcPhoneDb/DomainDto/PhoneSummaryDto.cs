using System;

namespace AngularMvcPhoneDb.Core.DomainDto
{
    public class PhoneSummaryDto
    {
        public PhoneSummaryDto(Guid id, string modelName)
        {
            Id = id;
            ModelName = modelName;
        }

        public Guid Id { get; private set; }
        public String ModelName { get; private set; }
    }
}