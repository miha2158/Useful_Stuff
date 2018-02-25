using System;

namespace Generator
{
    public enum RelationshipStatus
    {
        Single,
        Taken,
        Married,
        NotMarried
    }
    
    public class ExtendedPerson: Person
    {
        public DateTime DateOfBirth { get; set; }
        public Gender PersonGender { get; set; }

        public RelationshipStatus RelationshipStatus { get; set; }
        public string School { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;

        public ExtendedPerson() { }
        public ExtendedPerson(Gender PersonGender, string First, string Last, DateTime DateOfBirth,
                              RelationshipStatus RelationshipStatus, string School = "", string University = ""): base(First, Last)
        {
            this.PersonGender = PersonGender;
            this.DateOfBirth = DateOfBirth;
            this.RelationshipStatus = RelationshipStatus;
            this.School = School;
            this.University = University;
        }

        public new static ExtendedPerson FillBlanks() => FillBlanks((Gender) NewValue.Int(2));
        public new static ExtendedPerson FillBlanks(Gender PersonGender)
        {
            ExtendedPerson p = (ExtendedPerson) Person.FillBlanks(PersonGender);
            
            p.PersonGender = PersonGender;
            p.DateOfBirth = NewValue.NewDateTime(new DateTime(1970, 1, 1), new DateTime(DateTime.UtcNow.Subtract(new DateTime(15, 1, 1)).Ticks));
            p.RelationshipStatus = (RelationshipStatus)NewValue.Int(4);

            switch (NewValue.Int(3))
            {
                case 0:
                    p.School = "Школа";
                    break;

                case 1:
                    p.School = "Лицей";
                    break;

                case 2:
                    p.School = "Гимназия";
                    break;
            }
            p.School += " №" + NewValue.Int(38, 159);

            switch (NewValue.Int(6))
            {
                case 0:
                    p.University = "НИУ ВШЭ";
                    break;

                case 1:
                    p.University = "СПБГУ";
                    break;

                case 2:
                    p.University = "МГУ";
                    break;

                case 3:
                    p.University = "ИТМО";
                    break;

                case 4:
                    p.University = "МГИМО";
                    break;

                case 5:
                    p.University = "МФТИ";
                    break;
            }
            return p;
        }

        public override string ToString() => $"{First} {Last}, {PersonGender} {Environment.NewLine}{DateOfBirth}, {RelationshipStatus} {Environment.NewLine}{School}, {University}";
        public override int GetHashCode() => ToString().GetHashCode();
    }
}