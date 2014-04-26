using System;

namespace RestByDesign.Infrastructure.Core
{
    public static class SystemTime
    {
        private static readonly Func<DateTime> _defaultNowStrategy = () => DateTime.Now;
        private static Lazy<Func<DateTime>> _nowStrategy = new Lazy<Func<DateTime>>(() => _defaultNowStrategy);

        public static DateTime Now 
        {
            get
            {
                return _nowStrategy.Value();
            }
        }

        public static void SetDateTime(Func<DateTime> nowStrategy)
        {
            if(_nowStrategy.IsValueCreated)
                throw new Exception("Strategy cannot be set up after it has been used");

            _nowStrategy = new Lazy<Func<DateTime>>(() => nowStrategy);
        }

        public static void ResetDateTime()
        {
            _nowStrategy = new Lazy<Func<DateTime>>(() => _defaultNowStrategy);
        }
    }
}