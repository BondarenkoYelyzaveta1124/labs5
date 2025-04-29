using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace labs5
{
    using Student = ( string LastName, string FirstName, string Patronymic, char Gender,
        DateTime BirthDate, string Math, string Physics, string Informatics,int Money );

    class Block2
    {
        static List<Student> ReadStudentsFromFile(string filePath)
        {
            List<Student> students = new List<Student>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    string line;
                    int lineNumber = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        try
                        {
                            string lastName = parts[0];
                            string firstName = parts[1];
                            string patronymic = parts[2];

                            char gender = 'U';
                            string genderInput = parts[3].ToUpperInvariant();
                            if (genderInput == "M" || genderInput == "М" || genderInput == "Ч")
                                gender = 'M';
                            else if (genderInput == "F" || genderInput == "Ж")
                                gender = 'F';

                            DateTime birthDate = DateTime.ParseExact(parts[4], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

                            string Math = parts[5];
                            string Physics = parts[6];
                            string Informatics = parts[7];

                            int Money = int.Parse(parts[8]);

                            students.Add((lastName, firstName, patronymic, gender, birthDate,
                                          Math, Physics, Informatics, Money));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Попередження: Помилка формату в рядку {lineNumber}: {line}. Деталі: {ex.Message}");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка читання файлу: {ex.Message}");
            }
            return students;
        }
        static void ProcessTask22(List<Student> students)
        {
            Console.WriteLine("\nЗавдання 22: Студенти молодше 18 з незданими іспитами");

            DateTime referenceDate = DateTime.Today;
            bool foundAny = false;

            foreach (var student in students)
            {
                int age = referenceDate.Year - student.BirthDate.Year;
                if (student.BirthDate.Date > referenceDate.AddYears(-age)) age--;

                bool isUnder18 = (age < 18);

                int math = student.Math == "-" ? 2 : int.Parse(student.Math);
                int physics = student.Physics == "-" ? 2 : int.Parse(student.Physics);
                int informatics = student.Informatics == "-" ? 2 : int.Parse(student.Informatics);

                bool failedExam = math < 3 || physics < 3 || informatics < 3;

                if (isUnder18 && failedExam)
                {
                    Console.WriteLine($"{student.LastName} {student.FirstName} {student.BirthDate:dd.MM.yyyy} (повних років: {age})");
                    foundAny = true;
                }
            }
            if (!foundAny)
                Console.WriteLine("Студентів, що відповідають умовам, не знайдено.");
        }
        public static void Block22()
        {
            string inputFilePath = "input.txt";

            List<Student> allStudents = ReadStudentsFromFile(inputFilePath);

            if (allStudents.Any()) ProcessTask22(allStudents);
        }
    }
}