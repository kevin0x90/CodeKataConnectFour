using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ConnectFourGame.Test
{
    [TestFixture]
    public class ConnectFourTest
    {
        [Test]
        public void Map_Should_Be_6_x_7()
        {
            var sut = new ConnectFour();

            var map = sut.Map;

            Assert.AreEqual(6, map.GetLength(0));
            Assert.AreEqual(7, map.GetLength(1));
        }

        [Test]
        public void Map_Should_Be_Initialized_With_0()
        {
            var sut = new ConnectFour();
            var expectedMap = new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
            };

            var map = sut.Map;

            Assert.AreEqual(expectedMap, map);
        }

        [Test]
        public void Player1_Should_Be_Represented_By_1()
        {
            var sut = new ConnectFour();

            var player1 = sut.Player1;

            Assert.AreEqual(1, player1);
        }

        [Test]
        public void Player2_Should_Be_Represented_By_2()
        {
            var sut = new ConnectFour();

            var player2 = sut.Player2;

            Assert.AreEqual(2, player2);
        }

        [Test]
        public void It_Should_Be_Possible_To_Load_A_Map()
        {
            var sut = new ConnectFour();
            var expectedMap = new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {1, 1, 1, 1, 2, 2, 2 }
            };

            sut.LoadMap(expectedMap);

            Assert.AreEqual(expectedMap, sut.Map);
        }

        [Test]
        public void It_Should_Be_Checked_If_A_Move_Is_Possible()
        {
            var sut = new ConnectFour();

            var isMovePossible = sut.Move(sut.Player1, 0);

            Assert.AreEqual(true, isMovePossible);
        }

        [Test]
        public void An_Invalid_Move_Should_Return_False()
        {
            var sut = new ConnectFour();

            var isMovePossible = sut.Move(sut.Player1, 0);
            isMovePossible = sut.Move(sut.Player1, 0);
            isMovePossible = sut.Move(sut.Player1, 0);
            isMovePossible = sut.Move(sut.Player1, 0);
            isMovePossible = sut.Move(sut.Player1, 0);
            isMovePossible = sut.Move(sut.Player1, 0);
            isMovePossible = sut.Move(sut.Player1, 0);

            Assert.AreEqual(false, isMovePossible);
        }

        [Test]
        public void The_Game_Should_Print_The_Map_Correctly()
        {
            var sut = new ConnectFour();
            var expectedMap = new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {1, 1, 1, 1, 2, 2, 2 }
            };
            var expectedOutput = "0 0 0 0 0 0 0" + Environment.NewLine +
                                 "0 0 0 0 0 0 0" + Environment.NewLine +
                                 "0 0 0 0 0 0 0" + Environment.NewLine +
                                 "0 0 0 0 0 0 0" + Environment.NewLine +
                                 "0 0 0 0 0 0 0" + Environment.NewLine +
                                 "1 1 1 1 2 2 2" + Environment.NewLine;

            sut.LoadMap(expectedMap);

            Assert.AreEqual(expectedOutput, sut.MapToString());

        }

        [Test]
        public void The_Game_Should_Determine_If_The_Map_Is_Full()
        {
            var sut = new ConnectFour();
            var expectedMap = new int[6, 7]
            {
                {1, 1, 1, 2, 1, 1, 1 },
                {1, 1, 1, 2, 1, 2, 1 },
                {1, 1, 2, 2, 1, 2, 1 },
                {1, 1, 2, 2, 1, 2, 1 },
                {1, 1, 2, 2, 2, 2, 2 },
                {1, 1, 1, 1, 2, 2, 2 }
            };

            sut.LoadMap(expectedMap);

            Assert.AreEqual(true, sut.MapIsFull());
        }

        [Test]
        [TestCaseSource("win_situation_player_1_source_diagonal")]
        public void When_Player1_Has_4_Diagonal_The_Game_Should_End(int player, int[,] expectedMap)
        {
            var sut = new ConnectFour();

            sut.LoadMap(expectedMap);

            Assert.AreEqual(true, sut.HasWon(sut.Player1));
        }

        [Test]
        [TestCaseSource("win_situation_player_1_source_horizontal")]
        public void When_Player1_Has_4_Horizontal_The_Game_Should_End(int player, int[,] expectedMap)
        {
            var sut = new ConnectFour();

            sut.LoadMap(expectedMap);

            Assert.AreEqual(true, sut.HasWon(sut.Player1));
        }

        [Test]
        [TestCaseSource("win_situation_player_1_source_vertical")]
        public void When_Player1_Has_4_Vertical_The_Game_Should_End(int player, int[,] expectedMap)
        {
            var sut = new ConnectFour();

            sut.LoadMap(expectedMap);

            Assert.AreEqual(true, sut.HasWon(sut.Player1));
        }

        public static IEnumerable<TestCaseData> win_situation_player_1_source_vertical()
        {
            yield return new TestCaseData(1, new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {1, 0, 0, 0, 0, 0, 0 },
                {1, 0, 0, 0, 0, 0, 0 },
                {1, 2, 0, 0, 0, 0, 0 },
                {1, 2, 0, 0, 0, 0, 0 }
            });
        }

        public static IEnumerable<TestCaseData> win_situation_player_1_source_horizontal()
        {
            yield return new TestCaseData(1, new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {1, 1, 1, 1, 2, 2, 0 }
            });

            yield return new TestCaseData(1, new int[6, 7]
            {
                {0, 0, 1, 1, 1, 1, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 }
            });

            yield return new TestCaseData(1, new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {2, 2, 0, 1, 1, 1, 1 }
            });

            yield return new TestCaseData(1, new int[6, 7]
            {
                {1, 1, 1, 1, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 }
            });
        }
        public static IEnumerable<TestCaseData> win_situation_player_1_source_diagonal()
        {
            yield return new TestCaseData(1, new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 1, 2, 0, 0, 0 },
                {0, 0, 2, 1, 0, 0, 0 },
                {0, 0, 1, 1, 1, 0, 0 },
                {0, 0, 2, 2, 2, 1, 0 },
            });

            yield return new TestCaseData(1, new int[6, 7]
            {
                {0, 0, 0, 0, 0, 0, 0 },
                {1, 0, 0, 0, 0, 1, 0 },
                {1, 0, 0, 0, 1, 2, 0 },
                {1, 0, 0, 1, 1, 2, 0 },
                {2, 0, 1, 2, 1, 1, 0 },
                {2, 1, 2, 2, 2, 1, 0 }
            });
        }
    }
}
