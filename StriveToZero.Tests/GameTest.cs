using StriveToZero.Core;
using Xunit;
namespace StriveToZero.Tests {
    public class GameTest {
        [Fact]
        public void TestName () {
            //Given
            byte min = 1;
            byte max = 5;
            //When
            Game game = new Game ();
            game.SetGameInterval (min, max);

            //Then
            Assert.True ((min <= game.GameNumber) && (game.GameNumber <= max));
        }
    }
}