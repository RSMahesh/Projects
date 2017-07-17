namespace UserActivityLogger
{
    public abstract class ValueObject<T>

    where T : ValueObject<T>
    {
        public override bool Equals(object obj)

        {

            var valueObject = obj as T;

            if (ReferenceEquals(valueObject, null))

                return false;

            return GetHashCode() == obj.GetHashCode() ;
        }
  
        //Object.GetHashCode() uses an internal field in the System.Object class to generate the hash value. Each object created is assigned a unique object key, stored as an integer,when it is created. These keys start at 1 and increment every time a new object of any type gets created. However, because this index can be reused after the object is reclaimed during garbage collection, it is possible to obtain the same hash code for two different objects.The two objects won't have the same hash code, because an object's code isn't reused until the object is garbage collected (i.e. no longer exists).
        //If you have written a class which implements its own equality that is different from reference equality then you are REQUIRED to override GetHashCode such that two objects that compare as equal have equal hash codes.
        //Mutable objects which compare for equality on their mutable state, and therefore hash on their mutable state, are dangerously bug-prone.You can put an object into a hash table, mutate it, and be unable to get it out again.Try to never hash or compare for equality on mutable state.
        //The default object.GetHashCode is a good implemention of GetHashCode for types which use reference equality, i.e. for classes and/or interfaces (but not structs) for which you have not overriden object.Equals; where 'good implementation' means good/speedy performance when using these types as the key of a Dictionary.

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }
        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {

            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))

                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))

                return false;

            return a.Equals(b);
        }
        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}