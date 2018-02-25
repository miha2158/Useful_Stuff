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

        public static List<string> MaleFirstNames = new List<string>(0);
        public static List<string> MaleLastNames = new List<string>(0);
        public static List<string> FemaleFirstNames = new List<string>(0);
        public static List<string> FemaleLastNames = new List<string>(0);

        public static Person FillBlanks() => FillBlanks((Gender) NewValue.Int(2));
        public static Person FillBlanks(Gender PersonGender)
        {
            var p = new Person();
            switch (PersonGender)
            {
                case Gender.Male:
                {
                    if (MaleFirstNames.Count == 0)
                        MaleFirstNames = File.ReadAllLines(@"malefirstnames.txt").ToList();
                    p.First = MaleFirstNames[NewValue.Int(MaleFirstNames.Count)];

                    if (MaleLastNames.Count == 0)
                        MaleLastNames = File.ReadAllLines(@"malelastnames.txt").ToList();
                    p.Last = MaleLastNames[NewValue.Int(MaleLastNames.Count)];
                    break;
                }

                case Gender.Female:
                {
                    if (FemaleFirstNames.Count == 0)
                        FemaleFirstNames = File.ReadAllLines(@"femalefirstnames.txt").ToList();
                    p.First = FemaleFirstNames[NewValue.Int(FemaleFirstNames.Count)];

                    if (FemaleLastNames.Count == 0)
                        FemaleLastNames = File.ReadAllLines(@"femalelastnames.txt").ToList();
                    p.Last = FemaleLastNames[NewValue.Int(FemaleLastNames.Count)];
                    break;
                }
            }

            return p;
        }

        public override string ToString() => $"{First} {Last}";
        public override int GetHashCode() => ToString().GetHashCode();
    }
}