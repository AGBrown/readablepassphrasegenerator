﻿// Copyright 2013 Murray Grant
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace MurrayGrant.ReadablePassphrase.Random
{
    /// <summary>
    /// An external random source from any function returning a byte array.
    /// </summary>
    public class ExternalRandomSource : RandomSourceBase
    {
        private readonly Func<int, byte[]> _RandomProvider;
        public ExternalRandomSource(Func<int, byte[]> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            this._RandomProvider = source;
        }

        public override byte[] GetRandomBytes(int numberOfBytes)
        {
            return _RandomProvider(numberOfBytes);
        }
    }
}