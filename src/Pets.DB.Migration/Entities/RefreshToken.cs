﻿using System;

namespace Pets.DB.Migrations.Entities
{
    /// <summary>
    /// сущность токенов для перевыпуска JWT токена
    /// </summary>
    internal sealed class RefreshToken
    {
        /// <summary>
        /// id токена обновления сессии
        /// </summary>
        public String Id { get; }

        /// <summary>
        /// id юзера которому принадлежит токен
        /// </summary>
        public Guid UserId { get; }
        
        /// <summary>
        ///  юзер которому принадлежит токен
        /// </summary>
        public User User { get; }
        
        /// <summary>
        /// Ip которому был выдан токен
        /// </summary>
        public String IpAddress{ get; }

        /// <summary>
        /// Дата стухания
        /// </summary>
        public DateTime ExpireDate { get; }
    }
}