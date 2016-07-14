using System.Linq;
using NUnit.Framework;

namespace WebScrape.Core.Tests.Give_Utility
{
    public class When_EnumeratePath
    {
        [Test]
        public void Should_get_path_if_no_enumarate_syntax()
            => Assert.That(Utility.EnumeratePath("http://test").Count(), Is.EqualTo(1));

        [Test]
        public void Should_get_three_paths_if_1__3()
            => Assert.That(Utility.EnumeratePath("http://test?page={1..3}").Count(), Is.EqualTo(3));

        [Test]
        public void Should_get_three_paths_if_1_1_3()
            => Assert.That(Utility.EnumeratePath("http://test?page={1.1.3}").Count(), Is.EqualTo(3));

        [Test]
        public void Should_get_five_paths_if_1_2_10()
            => Assert.That(Utility.EnumeratePath("http://test?page={1.2.10}").Count(), Is.EqualTo(5));


        [Test]
        public void Should_get_three_paths_if_3__5()
            => Assert.That(Utility.EnumeratePath("http://test?page={3..5}").Count(), Is.EqualTo(3));

        [Test]
        public void Should_enumerate_paths()
           => Assert.That(Utility.EnumeratePath("http://test?page={1..3}"), Is.EqualTo(new []
           {
               "http://test?page=1",
               "http://test?page=2",
               "http://test?page=3"
           }));


    }
}
