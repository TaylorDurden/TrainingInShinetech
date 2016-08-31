using System;

namespace Features
{
    public class User
    {
        //只读属性初始化Getter-only auto-properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Lambda表达式用作属性Expression bodies on property-like function members  字符串嵌入值
        public override string ToString() => $"{FirstName}——{LastName}";
        public string FullName => FirstName + " " + LastName;

        //无参数的结构体构造函数—— Parameterless constructors in structs
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
                //nameof表达式nameof expressions
                throw new ArgumentNullException(nameof(user));
            }
        }
    }
}