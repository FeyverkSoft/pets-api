﻿using System;

namespace Pets.DB.Migrations.Entities
{
    /// <summary>
    /// пользователь системы
    /// </summary>
    sealed class User
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// емайл пользователя
        /// </summary>
        public String Email { get; }

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