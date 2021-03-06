﻿namespace OffCategoryReader
{
    using OffLangParser;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = new FileStream(@"d:\\categories.txt", FileMode.Open, FileAccess.Read, FileShare.None, 1, true))
            {
                var f = new LinkedLangFileParser(
                    new LangFileParser(
                     new StopWordsParser(),
                     new SynonymsParser(),
                     new TranslationSetParser(
                         new TranslationParser(),
                         new LinkedDataParser(new List<PrefixOnlyParser<LinkedData>>
                         {
                                new WikidataParser(),
                                new WikidataCategoryParser(),
                                new WikipediaCategoryParser(),
                                new PnnsGroupParser(1),
                                new PnnsGroupParser(2),
                                new CountryParser(),
                                new RegionParser(),
                                new InstanceOfParser(),
                                new GrapeVarietyParser(),
                                new LabelParser()
                         })))).Parse(stream, Encoding.UTF8);
                f.WriteToAsync(Console.Out).Wait();
                using (var o = new StreamWriter(@"d:\\categories2.txt"))
                {
                    f.WriteToAsync(o).Wait();
                }
            }
        }
    }
}
