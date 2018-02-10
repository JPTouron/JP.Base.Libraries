using System;
using System.ComponentModel;

namespace JP.Base.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Find the enum from the description attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="desc"></param>
        /// <param name="lookALike">determines whether we use a like comparison or an == one</param>
        /// <returns></returns>
        public static T FromName<T>(this string desc, bool lookALike = false) where T : struct
        {
            string attr;
            var found = false;
            var result = (T)Enum.GetValues(typeof(T)).GetValue(0);

            foreach (var enumVal in Enum.GetValues(typeof(T)))
            {
                attr = ((Enum)enumVal).ToName();

                if ((!lookALike && attr == desc) || (lookALike && attr.ToLower().Contains(desc.ToLower())))
                {
                    result = (T)enumVal;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                //throw new Exception("Description not found in the required enumerator");
            }

            return result;
        }

        // This extension method is broken out so you can use a similar pattern with
        // other MetaData elements in the future. This is your base method for each.
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            // check if no attributes have been specified.
            if (attributes.Length > 0)
            {
                return (T)attributes[0];
            }
            return null;
        }

        // This method creates a specific call to the above method, requesting the
        // Description MetaData attribute.
        public static string ToName(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}