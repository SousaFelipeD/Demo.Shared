using Flunt.Notifications;
using System;

namespace Demo.Shared.Entities
{
    /// <summary>
    /// Entidade base
    /// </summary>
    public abstract class Entity : Notifiable
    {

        /// <summary>
        /// ID
        /// </summary>
        public virtual Guid Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
