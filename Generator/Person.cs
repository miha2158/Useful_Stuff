using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Generator
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Person
    {
        public string First { get; set; } = String.Empty;
        public string Last { get; set; } = String.Empty;

        public Person() { }
        public Person(string First, string Last)
        {
            this.First = First;
            this.Last = Last;
        }

        public static readonly List<string> MaleFirstNames = new List<string>(File.ReadAllLines(@"malefirstnames.txt"));
        public static readonly List<string> MaleLastNames = new List<string>(File.ReadAllLines(@"malelastnames.txt"));
        public static readonly List<string> FemaleFirstNames = new List<string>(File.ReadAllLines(@"femalefirstnames.txt"));
        public static readonly List<string> FemaleLastNames = new List<string>(File.ReadAllLines(@"femalelastnames.txt"));

        public static Person FillBlanks() => FillBlanks((Gender) NewValue.Int(2));
        public static Person FillBlanks(Gender PersonGender)
        {
            var p = new Person();
            switch (PersonGender)
            {
                case Gender.Male:
                {
                    p.First = MaleFirstNames[NewValue.Int(MaleFirstNames.Count)];
                    p.Last = MaleLastNames[NewValue.Int(MaleLastNames.Count)];
                    break;
                }

                case Gender.Female:
                {
                    p.First = FemaleFirstNames[NewValue.Int(FemaleFirstNames.Count)];
                    p.Last = FemaleLastNames[NewValue.Int(FemaleLastNames.Count)];
                    break;
                }
            }

            return p;
        }

        public override string ToString() => $"{First} {Last}";
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
        {
            var person = obj as Person;
            return person != null &&
                   First == person.First &&
                   Last == person.Last;
        }
    }
}