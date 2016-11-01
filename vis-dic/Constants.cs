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
            Hiponimia,
            //Uogólnienie, czyli człowiek dla kobieta to Hiperonim
            Hypernym,
            //Hiper_PWN-plWN - hiperonim w Princeton wordnet
            Hiper_PWN,
            //Hipo_PWN-plWN - hiponim w Princeton wordnet
            Hipo_PWN
        }

    }

    public static class Messages
    {
        public const string DirectoryNotChosen = "Please choose the directory!";
        public const string ErrorFound = "There is an error ";
    }
}
