using System;

namespace AbsentSystem.Data.Entities
{
    public class BaseEntity<TKey> : IEntiity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public interface IEntiity
    {

    }
}
