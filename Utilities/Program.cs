using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{

    static class StringUtilities
    {
        public static int ToWords(string sentence)
        {
            string[] words = sentence.Split(' ');
            return words.Length;
        }

        public static string ToSentence(string sentence)
        {
            sentence = char.ToUpper(sentence[0]) + (sentence.ToLower()).Substring(1, sentence.Length - 1);
            return sentence;
        }

        public static string ToCamelCase(string sentence)
        {
            string[] words = sentence.Split(' ');
            string camelCase = "";

            for(int i = 0; i < words.Length; i++)
            {
                camelCase += ToSentence(words[i]);
            }

            return camelCase;
        }

        public static string JoinWith(this IEnumerable<string> words, string separator)
        {
            return string.Join(separator, words);
        }

        public static Tuple<string,string> TwoStringsToTuple(string sentence)
        {
            var separated = sentence.Split(' ');
            string firstname = separated[0];
            string lastname = separated[1];
            Tuple<string, string> person = Tuple.Create(firstname, lastname);
            return person;
        }

        public static string TupleToTwoStrings(Tuple<string,string> personTuple)
        {
            string firstname = personTuple.Item1;
            string lastname = personTuple.Item2;
            string person = firstname + " " + lastname;
            return person;
        }

        public static Tuple<int, int> GcdLcmTupleGetter(int num1, int num2)
        {
            int max = num1 > num2 ? num1 : num2;

            int gcd = 1;
            int lcm;
            for(int i = 1; i <= max; i++)
            {
                if (num1%i == 0 && num2%i == 0)
                {
                    gcd = i;
                }
            }

            lcm = (num1*num2)/ gcd;

            return Tuple.Create(gcd, lcm);

        }

        public static Tuple<bool, int> TryParseStringToTuple(this string sentence)
        {
            int temp;
            bool check = int.TryParse(sentence, out temp);
            return Tuple.Create(check, temp);
        }

        public static int? TryParseStringToTuple2(this string sentence)
        {
            int temp;
            bool check = int.TryParse(sentence, out temp);
            if(check)
            {
                return temp;
            }
            else
            {
                return null;
            }
        }
    }

    class GeneralizedName : IEnumerable<string>
    {
        private List<string> names = new List<string>();

        public GeneralizedName(string name, string surname)
        {
            names.Add(name);
            names.Add(surname);
        }

        public void AddMidName(string mid)
        {
            names.Insert(names.Count - 1, mid);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return names.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return names.GetEnumerator();
        }
    }

    class PrimeGenerator
    {
        private int last;

        public PrimeGenerator(int last)
        {
            this.last = last;
        }
        public List<int> GeneratePrimes()
        {
            List<int> numbers = new List<int>();
            for (int i = 2; i <= last; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    numbers.Add(i);
                }
            }
            return numbers;
        }
    }

    public class Maybe<T>
    {
        private T value;

        public Maybe(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException("null exception");
                }
                return value;
            }
        }

        public bool HasValue => this.value == null ? false : true;

        public override string ToString()
        {
            return HasValue ? Value.ToString() : "";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(StringUtilities.ToWords("Hello World")); //2
            Console.WriteLine(StringUtilities.ToWords("One two three four five")); //5
            Console.WriteLine(StringUtilities.ToSentence("the LorD OF thE Rings"));
            Console.WriteLine(StringUtilities.ToCamelCase("the LorD OF thE Rings"));
            var joined = new List<string> { "First", "Second", "Third" }.JoinWith(", ");
            Console.WriteLine(joined);
            Console.WriteLine(StringUtilities.TwoStringsToTuple("James Bond"));
            Console.WriteLine(StringUtilities.TupleToTwoStrings(new Tuple<string,string>("James", "Bond")));
            Console.WriteLine(StringUtilities.GcdLcmTupleGetter(24, 26));
            Console.WriteLine("hey".TryParseStringToTuple());
            Console.WriteLine("hey".TryParseStringToTuple2());
            //Tuple probably more convinient for some purposes, but I prefer nullable
            var romario = new GeneralizedName("Romario", "Filha");
            romario.AddMidName("Faria");
            romario.AddMidName("Da");
            romario.AddMidName("Sousa");

            foreach (var namePart in romario)
                Console.WriteLine(namePart);

            var generator = new PrimeGenerator(4000);
            foreach (var number in generator.GeneratePrimes())
                Console.WriteLine(number);

            var name = new Maybe<string>("Marcin");
            Console.WriteLine(name.HasValue); //Should display true
            Console.WriteLine(name); //Should display Marcin
            Console.WriteLine(name.Value); //Should display Marcin
            var name2 = new Maybe<string>(null);
            Console.WriteLine(name2.HasValue); //Should display false
            Console.WriteLine(name2); //Should display nothing
            Console.WriteLine(name2.Value); //Should throw an exception
            Console.ReadKey();
        }
    }
}
