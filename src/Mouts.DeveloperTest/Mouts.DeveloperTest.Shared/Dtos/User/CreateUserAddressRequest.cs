using System;
using System.Collections.Generic;
using System.Text;

namespace Mouts.DeveloperTest.Shared.Dtos.User
{
    public sealed record CreateUserAddressRequest(
    string City,
    string Street,
    int Number,
    string Zipcode,
    CreateUserGeoLocationRequest Geolocation);
}
