﻿using System.Collections;
using System.Collections.Generic;

namespace Langbry
{
    public class LanguageTable
    {
        public Dictionary<string, string> LanguageDictionary { get; private set; } = new Dictionary<string, string>();

        public LanguageTable(IDictionary dic)
        {
            foreach (DictionaryEntry d in dic)
            {
                LanguageDictionary.Add(d.Key.ToString(), d.Value.ToString());
            }
        }
    }
}