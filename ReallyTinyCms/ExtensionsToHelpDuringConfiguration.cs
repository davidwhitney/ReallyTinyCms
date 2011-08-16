namespace ReallyTinyCms
{
    public static class ExtensionsToHelpDuringConfiguration
    {
        public static int Second(this int milliseconds)
        {
            return milliseconds*1000;
        }

        public static int Seconds(this int milliseconds)
        {
            return milliseconds.Second();
        }

        public static int Millisecond(this int milliseconds)
        {
            return milliseconds;
        }

        public static int Milliseconds(this int milliseconds)
        {
            return milliseconds.Millisecond();
        }

        public static int Minute(this int milliseconds)
        {
            return milliseconds*1000*60;
        }

        public static int Minutes(this int milliseconds)
        {
            return milliseconds.Minute();
        }

        public static int Hour(this int milliseconds)
        {
            return milliseconds*1000*60*60;
        }

        public static int Hours(this int milliseconds)
        {
            return milliseconds.Hour();
        }
    }
}
