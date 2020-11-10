namespace Skeleton.Domain.Options
{
    public class AuthOptions
    {
        /// <summary>
        /// Private key to encrypt the token.
        /// Prod key must be different from dev & staging
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Time before token expire in seconds.
        /// 7d = 604800
        /// </summary>
        public int TokenExpires { get; set; }
    }
}