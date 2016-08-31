using System.Collections.Generic;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqAndString
    {
        public IEnumerable<string> GetStringAppearCount()
        {
            const string article = @"The Endorsed Standards for Java SE constitute all classes and " +
                                   @" interfaces that are defined in the packages listed in this section. " +
                                   @"Classes and interfaces defined in sub-packages of listed packages" +
                                   @" are not Endorsed Standards unless those sub-packages are themselves listed." +
                                   @" The Endorsed Standards Override Mechanism may be used to override the Java SE" +
                                   @" platform packages in this list, and these packages may be" +
                                   @" overridden only by versions of the Endorsed Standard that" +
                                   @"are newer than that provided by the Java platform as released by Sun." +
                                   @" With the exception of packages listed here and the technologies " +
                                   @"listed in the Standalone Technologies section below, no other " +
                                   @" packages from the Java SE platform API specification may be overridden.";

            var strArray = article.Split('.', ' ');

            var queryResult = from str in strArray
                where str == "packages"
                select str;
            return queryResult;
        }

        public IEnumerable<char> GetDigitFromString()
        {
            const string strSource = "sdfsdf232sd09sdfds";

            var queryResult = from str in strSource
                where char.IsDigit(str)
                select str;

            return queryResult;
        }
    }
}
