namespace Taaldc.Marketing.API.DTO;

public record InquiryDto
{
    public InquiryDto(string message, int propertyId, string property, string attendedBy, string remarks, int inquiryTypeId, string inquiryType, int statusId, string status, string salutation, string firstName, string lastName, string emailAddress, string contactNo, string country, string province, string townCity)
    {
        Message = message;
        PropertyId = propertyId;
        Property = property;
        AttendedBy = attendedBy;
        Remarks = remarks;
        InquiryTypeId = inquiryTypeId;
        InquiryType = inquiryType;
        StatusId = statusId;
        Status = status;
        Salutation = salutation;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        ContactNo = contactNo;
        Country = country;
        Province = province;
        TownCity = townCity;
    }

    public string Message { get;  }
    public int PropertyId { get;  }
    public string Property { get;  }
    public string AttendedBy { get; }
    public string Remarks { get;  }
    
    public int InquiryTypeId { get; }
    public string InquiryType { get;  }
    
    private int StatusId { get; }
    public string Status { get;  }

    
    public string Salutation { get; }
    public string FirstName { get;  }
    public string LastName { get;  }
    public string EmailAddress { get;  }
    public string ContactNo { get; }
    public string Country { get;  }
    public string Province { get;  }
    public string TownCity { get; }
}