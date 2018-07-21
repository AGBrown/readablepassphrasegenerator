﻿// Copyright 2011 Murray Grant
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
using MurrayGrant.ReadablePassphrase.Words;
using MurrayGrant.ReadablePassphrase.Dictionaries;

namespace MurrayGrant.ReadablePassphrase.WordTemplate
{
    public class IndefinitePronounTemplate : Template
    {
        private readonly bool _IsPlural;
        private readonly bool _IsPersonal;
        public override bool IncludeInAlreadyUsedList { get { return false; } }
        public bool IsPlural { get { return _IsPlural; } }
        public IndefinitePronounTemplate(bool isPlural, bool isPersonal)
        {
            this._IsPlural = isPlural;
            this._IsPersonal = isPersonal;
        }

        public override WordAndString ChooseWord(WordDictionary words, Random.RandomSourceBase randomness, IEnumerable<Word> alreadyChosen)
        {
            var word = words.ChooseWord<IndefinitePronoun>(randomness, alreadyChosen, x => x.IsPersonal == _IsPersonal);
            if (!this._IsPlural)
                return new WordAndString(word, word.Singular);
            else
                return new WordAndString(word, word.Plural);
        }
    }
}