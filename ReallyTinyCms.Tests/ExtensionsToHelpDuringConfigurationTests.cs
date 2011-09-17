using NUnit.Framework;

namespace ReallyTinyCms.Tests
{
    [TestFixture]
    public class ExtensionsToHelpDuringConfigurationTests
    {
        [Test]
        public void Hour_PassedInteger_ConvertsToMilliseconds()
        {
            const int expected = 3600000;
            var ms = 1.Hour();

            Assert.That(ms, Is.EqualTo(expected));
        }

        [Test]
        public void Hours_PassedInteger_ConvertsToMilliseconds()
        {
            const int expected = 3600000;
            var ms = 1.Hours();

            Assert.That(ms, Is.EqualTo(expected));
        }

        [Test]
        public void Minute_PassedInteger_ConvertsToMilliseconds()
        {
            const int expected = 60000;
            var ms = 1.Minute();

            Assert.That(ms, Is.EqualTo(expected));
        }

        [Test]
        public void Minutes_PassedInteger_ConvertsToMilliseconds()
        {
            const int expected = 60000;
            var ms = 1.Minutes();

            Assert.That(ms, Is.EqualTo(expected));
        }

        [Test]
        public void Second_PassedInteger_ConvertsToMilliseconds()
        {
            const int expected = 1000;
            var ms = 1.Second();

            Assert.That(ms, Is.EqualTo(expected));
        }

        [Test]
        public void Seconds_PassedInteger_ConvertsToMilliseconds()
        {
            const int expected = 1000;
            var ms = 1.Seconds();

            Assert.That(ms, Is.EqualTo(expected));
        }
    }
}
