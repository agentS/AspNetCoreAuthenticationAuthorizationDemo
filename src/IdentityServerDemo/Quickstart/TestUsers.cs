// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer4.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "818727", Username = "monty.burns@burnspowerplant.com", Password = "alice", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Charles Montgomery Burns"),
                    new Claim(JwtClaimTypes.GivenName, "Charles Montgomery"),
                    new Claim(JwtClaimTypes.FamilyName, "Burns"),
                    new Claim(JwtClaimTypes.Email, "monty.burns@burnspowerplant.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': '1000 Mammon Avenue', 'locality': 'Springfield', 'postal_code': 00000, 'country': 'USA' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            }
        };
    }
}