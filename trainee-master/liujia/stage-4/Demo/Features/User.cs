using System;

namespace Features
{
    public class User
    {
        //ֻ�����Գ�ʼ��Getter-only auto-properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Lambda���ʽ��������Expression bodies on property-like function members  �ַ���Ƕ��ֵ
        public override string ToString() => $"{FirstName}����{LastName}";
        public string FullName => FirstName + " " + LastName;

        //�޲����Ľṹ�幹�캯������ Parameterless constructors in structs
        public User() : this("Jone", "Smith")
        {
        }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static void AddUser(User user)
        {
            if (user == null)
            {
                //nameof���ʽnameof expressions
                throw new ArgumentNullException(nameof(user));
            }
        }
    }
}