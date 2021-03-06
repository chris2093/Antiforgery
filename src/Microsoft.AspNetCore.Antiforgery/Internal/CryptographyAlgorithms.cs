﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;

namespace Microsoft.AspNetCore.Antiforgery.Internal
{
    public static class CryptographyAlgorithms
    {
#if NETSTANDARD1_3
        public static SHA256 CreateSHA256()
        {
            var sha256 = SHA256.Create();

            return sha256;
        }
#else
        public static SHA256 CreateSHA256()
        {
            SHA256 sha256;

            try
            {
                sha256 = SHA256.Create();
            }
            // SHA256.Create is documented to throw this exception on FIPS compliant machines.
            // See: https://msdn.microsoft.com/en-us/library/z08hz7ad%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            catch (System.Reflection.TargetInvocationException)
            {
                // Fallback to a FIPS compliant SHA256 algorithm.
                sha256 = new SHA256CryptoServiceProvider();
            }

            return sha256;
        }
#endif
    }
}
