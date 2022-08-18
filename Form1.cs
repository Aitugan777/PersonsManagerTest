using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonsManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static List<Person> Peoples = new List<Person>();

        private void btnCreateObjects_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            

            for (int i = 0; i < 10; i++)
            {
                byte rchildCount = (byte)random.Next(10);
                long rBirthDate = (long)random.Next(1930, 2004);
                byte rGender = (byte)random.Next(2);
                double rSalary = random.Next(25000, 90000);
                byte rMarried = (byte)random.Next(2);
                byte rPhonesCount = (byte)random.Next(1, 5);
                byte rCreditCardCount = (byte)random.Next(5);

                Gender gender = Gender.Male;
                if (rGender == 1)
                    gender = Gender.Female;

                Person person = new Person()
                {
                    FirstName = RandomName(random, gender),
                    LastName = RandomLastName(random, gender),
                    BirthDate = rBirthDate,
                    Gender = gender,
                    Id = Peoples.Count,
                    Children = new People[rchildCount],
                    CreditCardNumbers = new string[rCreditCardCount],
                    Phones = new string[rPhonesCount],
                    Salary = rSalary,
                    Age = (int)(DateTime.Now.Year - rBirthDate),
                    SequenceId = Peoples.Count +1,
                    IsMarried = rMarried == 1,
                    TransportId = RandomGUID()
                };



                for (int j = 0; j < rchildCount; j++)
                {
                    long rBirthDateChild = (long)random.Next(2004, 2022);
                    byte rGenderChild = (byte)random.Next(2);

                    Gender genderChild = Gender.Male;
                    if (rGenderChild == 1)
                        genderChild = Gender.Female;

                    string lastName = person.LastName;
                    if (genderChild == Gender.Male)
                    {
                        if (lastName[lastName.Length - 1] == 'а')
                            lastName = lastName.Remove(lastName.Length - 1);
                    }
                    else
                    {
                        if (lastName[lastName.Length - 1] == 'н' || lastName[lastName.Length - 1] == 'в')
                            lastName += "а";
                    }

                    People child = new People()
                    {
                        FirstName = RandomName(random, genderChild),
                        LastName = lastName,
                        Id = j,
                        Gender = genderChild,
                        BirthDate = rBirthDateChild
                    };

                    person.Children[j] = child;
                }


                for (int g = 0; g < rPhonesCount; g++)
                {
                    person.Phones[g] = RandomPhone(random);
                }

                for (int k = 0; k < rCreditCardCount; k++)
                {
                    int credNum1 = random.Next(10000000, 99999999);
                    int credNum2 = random.Next(10000000, 99999999);
                    person.CreditCardNumbers[k] = credNum1 + "" + credNum2;
                }

                Peoples.Add(person);
            }

            string str = "";
            
            foreach(Person person in Peoples)
            {
                str += $"({person.Id}){person.LastName} {person.FirstName} | Пол: {person.Gender}, Год рождения: {person.BirthDate}, Транспорт ид: {person.TransportId}\n";
                str += $"Последовательность {person.SequenceId}, В браке: {person.IsMarried}, Зарплата {person.Salary}, Возраст {person.Age}\n";
                str += "Дети:\n";
                foreach (People child in person.Children)
                {
                    str += $"({child.Id}){child.LastName} {child.FirstName} | Пол: {child.Gender}, Год рождения: {child.BirthDate}\n";
                }
                str += "Кредитные карты:\n";
                foreach (string cards in person.CreditCardNumbers)
                {
                    str += $"{cards}\n";
                }
                str += "Номера телефонов:\n";
                foreach (string phone in person.Phones)
                {
                    str += $"{phone}\n";
                }
                str += "\n\n";
                listBox1.Items.Add(str);
                str = "";
            }

            JsonSerializeWrite.JsonSerialize();
        }

        public Guid RandomGUID()
        {
            byte[] bytes = new Byte[16];
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            rand.GetBytes(bytes);
            return new Guid(bytes);
        }

        public string RandomPhone(Random rand)
        {
            int num = rand.Next(000, 999);
            int num2 = rand.Next(00, 99);
            int num3 = rand.Next(00, 99);
            string result = "+7 (987)" + " " + num.ToString() + " " + num2.ToString() + " " + num3.ToString();
            return result;
        }

        public string RandomName(Random random, Gender gender)
        {
            List<string> NamesMale = new List<string>()
            {
                "Aitugan",
                "Matvey",
                "Igor",
                "Dinislam",
                "Magomed",
                "Aleksey",
                "Aleksandr",
                "Michail",
                "Kastiel",
                "Jucifer",
                "Nikolai",
                "Daniil"
            };

            List<string> NamesFemale = new List<string>()
            {
                "Firuza",
                "Mariya",
                "Altynai",
                "Anastasia",
                "Kristina",
                "Aleksandra",
                "Darya",
                "Kseniya",
                "Evgeniya",
                "Veronika",
            };

            if (gender == Gender.Male)
                return NamesMale[random.Next(0, NamesMale.Count - 1)];
            return NamesFemale[random.Next(0, NamesFemale.Count - 1)];

        }

        public string RandomLastName(Random random, Gender gender)
        {
            List<string> FirstNames = new List<string>()
            {
                "Irgalin",
                "Matveev",
                "Saitov",
                "Islamov",
                "Magomedov",
                "Kovtunyak",
                "Podaliki",
                "Winchester",
                "Aklz",
                "Martinas",
                "Ostapov",
                "Kulakov"
            };

            string returnFirstName = FirstNames[random.Next(0, FirstNames.Count - 1)];

            if (gender == Gender.Female)
            {
                if (returnFirstName[returnFirstName.Length - 1] == 'н' || returnFirstName[returnFirstName.Length - 1] == 'в')
                    returnFirstName += "а";
            }
            return returnFirstName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = JsonSerializeWrite.JsonSerialize();
        }
    }
}
