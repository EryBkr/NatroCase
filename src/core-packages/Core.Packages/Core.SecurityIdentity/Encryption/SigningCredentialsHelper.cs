﻿using Microsoft.IdentityModel.Tokens;

namespace Core.SecurityIdentity.Encryption;

public class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) =>
        new(securityKey, SecurityAlgorithms.HmacSha256Signature);
}
