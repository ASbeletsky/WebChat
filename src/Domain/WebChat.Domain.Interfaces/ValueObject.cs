namespace WebChat.Domain.Interfaces
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Reflection;

    #endregion

    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            T other = obj as T;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 17;
            int multiplier = 59;

            int hashCode = startValue;
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            Type myType = this.GetType();
            Type otherType = other.GetType();

            if (myType != otherType)
                return false;

            FieldInfo[] fields = myType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object myValue = field.GetValue(this); 
                object theirValue = field.GetValue(other);

                if (myValue == null)
                {
                    if (theirValue != null)
                        return false;
                }
                else
                {
                    if (!myValue.Equals(theirValue))
                        return false;
                }
            }

            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type myType = this.GetType();
            List<FieldInfo> fields = new List<FieldInfo>();

            while (myType != typeof(object))
            {
                fields.AddRange(myType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                myType = myType.BaseType;
            }

            return fields;
        }

        public static bool operator == (ValueObject<T> one, ValueObject<T> other)
        {
            return one.Equals(other);
        }

        public static bool operator != (ValueObject<T> one, ValueObject<T> other)
        {
            return !one.Equals(other);
        }
    }
}
