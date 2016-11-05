namespace Wordnet
{
    static class Nodes
    {
        public const string Literal = "LITERAL";
        public const string Synset = "SYNSET";
        public const string Sense = "SENSE";
        public const string Word = "WORD";
        public const string ID = "ID";
        public const string Synonym = "SYNONYM";
        public const string ILR = "ILR";
        public const string ILRType = "TYPE";
        public const string Domain = "DOMAIN";
        public const string Usage = "USAGE";
        public const string Definition = "Def";
        public const string PartOfSpeech = "POS";
        public const string Data = "DATA";

    }

    public static class Types
    {
        private enum PartsOfSpeech
        {
            Noun, Pronoun, Verb, Adverb, Adjective, Conjunction, Preposition, Interjection
        }

        private enum ILRTypes
        {
            //zawężenie, czyli kwiat dla rośliny to Hiponim
            hiponimia,
            //Uogólnienie, czyli człowiek dla kobieta to Hiperonim
            hypernym,
            //Hiper_PWN-plWN - hiperonim w Princeton wordnet
            Hiper_PWN,
            //Hipo_PWN-plWN - hiponim w Princeton wordnet
            Hipo_PWN
        }

        public const string Source_PLWNID = "plwnid";
        public const string Source_PNID = "pnid";
    }

    public static class Messages
    {
        public const string DirectoryNotChosen = "Please choose the directory!";
        public const string ErrorFound = "There is an error ";
        public const string InvalidWord = "Please enter valid word!";
        public const string NoNumberFound = @"No such number\word found - please provide valid number.";
    }

    public static class Paths
    {
        public const string DefaultOutputPath = "C:\\Temp\\mergedWordnet.xml";
    }
}
