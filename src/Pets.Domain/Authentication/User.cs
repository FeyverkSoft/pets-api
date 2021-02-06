using System;

namespace Pets.Domain.Authentication
{
    /// <summary>
    /// пользователь системы
    /// </summary>
   public sealed class User
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// login пользователя
        /// </summary>
        public String Login { get; }

        /// <summary>
        /// имя пользователя
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// хеш пароля пользователя
        /// </summary>
        public String PasswordHash { get; }

        /// <summary>
        /// JSON array с правами доступа юзера
        /// </summary>
        public String Permissions { get; }

        /// <summary>
        /// Состояние юзера, активен/забанен/требуется подтверждение мыла
        /// </summary>
        public String State { get; }

        public Guid ConcurrencyToken { get; }
    }
}