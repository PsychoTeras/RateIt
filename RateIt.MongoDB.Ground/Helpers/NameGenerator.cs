using System;
using System.Text;

namespace RateIt.MongoDB.Ground.Helpers
{
    enum CharType : byte
    {
        Unknown,
        Initial,
        Final
    }

    //Thread-unsafe class
    internal sealed class NameGenerator
    {

#region Constants

        public const byte MIN_NAME_LENGTH = 3;
        public const byte MAX_NAME_LENGTH = 12;
        public const byte MAX_WORDS_COUNT = 2;

#endregion

#region Private members

        //Initial chars list
        private readonly char[] _initials = new[] 
        { 
            'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 
            'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' 
        };
        private readonly int _initialsCount;

        //Final chars list
        private readonly char[] _finals = new[] 
        { 
            'a', 'e', 'i', 'o', 'u', 'y' 
        };
        private readonly int _finalsCount;

        //Private generator variables
        private int _rndSeed;
        private CharType _previousCharType;
        private CharType _currentCharType;
        private int _currentCharStep;

#endregion

#region Class methods

        public NameGenerator()
        {
            _initialsCount = _initials.Length;
            _finalsCount = _finals.Length;
            _rndSeed = Environment.TickCount;
        }

        private Random GetInitialRandomInstance()
        {
            return new Random(++_rndSeed);
        }

        private void ResetGenerator()
        {
            _currentCharStep = 0;
            _previousCharType = _currentCharType = CharType.Unknown;
        }

        public string Generate()
        {
            return Generate(MIN_NAME_LENGTH, MAX_NAME_LENGTH);
        }

        public string Generate(byte minLength)
        {
            return Generate(minLength, MAX_NAME_LENGTH);
        }

        public string Generate(byte minLength, byte maxLength)
        {
            int wordsCount = GetInitialRandomInstance().Next(1, MAX_WORDS_COUNT + 1);
            return Generate(minLength, maxLength, (byte)wordsCount);
        }

        public string Generate(byte minLength, byte maxLength, byte wordsCount)
        {
            if (minLength == 0)
            {
                throw new Exception("Min name length must be more than zero");
            }

            if (maxLength < minLength)
            {
                throw new Exception("Max name length must be more or equal to min name length");
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < wordsCount; i++)
            {
                GenerateWord(minLength, maxLength, i == 0, result);
                if (i < wordsCount - 1)
                {
                    result.Append(' ');
                }
            }

            return result.ToString();
        }

        private void GenerateWord(byte minLength, byte maxLength, bool firstWord,
            StringBuilder result)
        {
            ResetGenerator();
            Random rndInstance = GetInitialRandomInstance();
            int productNameLength = rndInstance.Next(minLength, maxLength);
            for (int i = 0; i < productNameLength; i++)
            {
                char c = GetNextChar(rndInstance);
                switch (i + (firstWord ? 0 : 1))
                {
                    case 0:
                        result.Append(char.ToUpper(c));
                        break;
                    default:
                        result.Append(c);
                        break;
                }
            }
        }

        private char GetNextChar(Random rndInstance)
        {
            if (_currentCharStep == 0)
            {
                SetNewCharSequence(rndInstance);
            }

            _currentCharStep--;

            switch (_currentCharType)
            {
                case CharType.Initial:
                    return _initials[rndInstance.Next(0, _initialsCount)];
                case CharType.Final:
                    return _finals[rndInstance.Next(0, _finalsCount)];
                default:
                    return '?';
            }
        }

        private void SetNewCharSequence(Random rndInstance)
        {
            switch (_previousCharType)
            {
                case CharType.Final:
                    _currentCharType = CharType.Initial;
                    break;
                case CharType.Initial:
                    _currentCharType = CharType.Final;
                    break;
                default:
                    _currentCharType = (CharType)rndInstance.Next(1, 3);
                    break;
            }

            _previousCharType = _currentCharType;
            _currentCharStep = rndInstance.Next(1, 2);
        }

#endregion

    }
}
