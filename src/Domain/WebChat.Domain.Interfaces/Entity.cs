namespace WebChat.Domain.Interfaces
{
    #region Using

    using System;

    #endregion

    public abstract class Entity<TKey> : IEquatable<Entity<TKey>> where TKey: struct
    {
        private TKey id;
        private int? oldHashCode;

        public TKey Id
        {
            get
            {
                return this.id;
            }
            private set
            {
                this.id = value;
            }
        }

        public bool Equals(Entity<TKey> other)
        {
            if (other == null)
            {
                return false;
            }

            bool otherIsTransient = Equals(other.Id, default(TKey));
            bool thisIsTransient = Equals(this.Id, default(TKey));

            if (otherIsTransient && thisIsTransient)
            {
                return ReferenceEquals(this, other);
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            Entity<TKey> other = obj as Entity<TKey>;
            return other.Equals(this);            
        }

        public override int GetHashCode()
        {
            if (oldHashCode.HasValue)
            {
                return oldHashCode.Value;
            }

            bool thisIsTransient = Equals(Id, default(TKey));

            if (thisIsTransient)
            {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }

            return Id.GetHashCode();
        }
    }
}
