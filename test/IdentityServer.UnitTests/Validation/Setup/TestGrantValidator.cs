﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Validation;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServer4.UnitTests.Validation
{
    class TestGrantValidator : IExtensionGrantValidator
    {
        private readonly bool _isInvalid;
        private readonly string _errorDescription;

        public TestGrantValidator(bool isInvalid = false, string errorDescription = null)
        {
            _isInvalid = isInvalid;
            _errorDescription = errorDescription;
        }

        public Task<GrantValidationResult> ValidateAsync(ValidatedTokenRequest request)
        {
            if (_isInvalid)
            {
                return Task.FromResult(new GrantValidationResult(TokenErrors.InvalidGrant, _errorDescription));
            }

            return Task.FromResult(new GrantValidationResult("bob", "CustomGrant"));
        }

        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            if (_isInvalid)
            {
                context.Result = new GrantValidationResult(TokenErrors.InvalidGrant, _errorDescription);
            }
            else
            {
                context.Result = new GrantValidationResult("bob", "CustomGrant");
            }

            return Task.FromResult(0);
        }

        public string GrantType
        {
            get { return "custom_grant"; }
        }
    }
}