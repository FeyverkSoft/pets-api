using System;

namespace Pets.Domain.Authentication
{
    /// <summary>
    /// сущность токенов для перевыпуска JWT токена
    /// </summary>
    public sealed class RefreshToken
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
        /// Ip которому был выдан токен
        /// </summary>
        public String IpAddress { get; }

        /// <summary>
        /// Дата стухания
        /// </summary>
        public DateTime ExpireDate { get; private set; }

        public RefreshToken(String id, Guid userId, String ip, DateTime expireDate)
        {
            Id = id;
            IpAddress = ip;
            UserId = userId;
            ExpireDate = expireDate;
        }

        public void Terminate()
        {
            ExpireDate = DateTime.UtcNow;
        }
    }
}