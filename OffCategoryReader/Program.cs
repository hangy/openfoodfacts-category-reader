namespace OffCategoryReader
{
    using OffLangParser;
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
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
                            new PnnsGroupParser(2)
                     })))).Parse(@"d:\\categories.txt");
            f.WriteToAsync(Console.Out).Wait();
            using (var o = new StreamWriter(@"d:\\categories2.txt"))
            {
                f.WriteToAsync(o).Wait();
            }
        }
    }
}
