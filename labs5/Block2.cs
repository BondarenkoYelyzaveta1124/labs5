using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace labs5
{
    using Student = (string LastName, string FirstName, string Patronymic, char Gender,
        DateTime BirthDate, string Math, string Physics, string Informatics, int Money);

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

                        if (parts.Length < 9)
                        {
                            Console.WriteLine($"Недостатньо даних у рядку {lineNumber}: {line}");
                            continue;
                        }
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

                            DateTime birthDate = DateTime.ParseExact(parts[4], "dd.MM.yyyy", CultureInfo.InvariantCulture).Date;

                            string mathGrade = parts[5];
                            string physicsGrade = parts[6];
                            string informaticsGrade = parts[7];

                            int money = int.Parse(parts[8]);

                            students.Add((lastName, firstName, patronymic, gender, birthDate, mathGrade, physicsGrade, informaticsGrade, money));
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine($"Помилка формату в рядку {lineNumber}: {line}");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Помилка читання файлу: " + ex.Message);
            }
            return students;
        }
        static void ProcessTask22(List<Student> students, string outFilePath)
        {
            DateTime referenceDate = DateTime.Today;
            bool foundAny = false;

            using (StreamWriter writer = new StreamWriter(outFilePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Студенти молодше 18 років з незданими іспитами:");
                foreach (var student in students)
                {
                    int age = referenceDate.Year - student.BirthDate.Year;
                    if (referenceDate.Month < student.BirthDate.Month ||
                        (referenceDate.Month == student.BirthDate.Month && referenceDate.Day < student.BirthDate.Day))
                    {
                        age--;
                    }

                    bool isUnder18 = (age < 18);
                    bool failedExam = student.Math == "-" || student.Physics == "-" || student.Informatics == "-" || 
                        student.Physics == "2" || student.Informatics == "2" || student.Math == "2";
                    if (isUnder18 && failedExam)
                    {
                        writer.WriteLine($"{student.LastName} {student.FirstName} {student.BirthDate:dd.MM.yyyy} (повних років: {age})");
                        Console.WriteLine($"{student.LastName} {student.FirstName} {student.BirthDate:dd.MM.yyyy} (повних років: {age})");
                        foundAny = true;
                    }
                }
                if (!foundAny)
                {
                    writer.WriteLine();
                    writer.WriteLine("Студентів, що відповідають умовам, не знайдено.");
                }
            }
        }
        public static void Block22()
        {
            string inputFilePath = "input.txt";
            string outFilePath = "tralala.txt";

            List<Student> allStudents = ReadStudentsFromFile(inputFilePath);
            if (allStudents.Any())
                ProcessTask22(allStudents, outFilePath);
            else
                Console.WriteLine("Студентів не знайдено або вхідний файл порожній.");
        }
    }
}
