using System;
using NUnit.Framework;

namespace LicenseKey.Tests
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [TestFixture]
    public class IntegrationTests
    {
        #region Setup/Teardown

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Init()
        {
            rnd = new Random();
        }


        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        public void Dispose()
        {
        }

        #endregion

        private Random rnd; // the random number 


        /// <summary>
        /// Test the random number generation
        /// </summary>
        [Test]
        public void Test01_Random()
        {
            try
            {
                int rannum;
                string strrndnum;
                int lopcnt;
                int lopcnt1;

                for (lopcnt1 = 0; lopcnt1 < 10; lopcnt1++)
                {
                    for (lopcnt = 1; lopcnt < 7; lopcnt++)
                    {
                        rannum = rnd.Next(lopcnt); 
                        strrndnum = NumberDisplay.CreateNumberString((uint) rannum, lopcnt);
                        if (strrndnum.Length != lopcnt)
                        {
                            throw new Exception("Strings not the correct length");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }


        /// <summary>
        /// Test a generic key generation with no parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test07_Gen2ParB16Bit55555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvppppxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "1");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "2");
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }

        [Test]
        public void Test07_5_Gen2ParB16Bit55555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxxpppp-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "1");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "2");
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4) / 4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }


        /// <summary>
        /// Test a generic key generation with 3 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test08_Gen3ParB16Bit55555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvppppxxxxxxxxxxxx-wwwwxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "1");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "2");
                    gkey.AddToken(2, "w", Key.TokenTypes.Number, "8");
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }

        /// <summary>
        /// Test a generic key generation with no parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test0_5_Gen2ParB16Bit55555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvvvvvppppxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(new SimpleToken('v', "11"));
                    gkey.AddToken(new SimpleToken('p', "2"));
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }


        /// <summary>
        /// Test a generic key generation with 2 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test10_5_Gen3ParB16Bit55555()
        {
            try
            {
                int lopcnt;
                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    var gkey = new Key("vvvvppppxxxxxxxxxxxx-wwwwwwwwxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(new SimpleToken('v', 1));
                    gkey.AddToken(new SimpleToken('p', 2));
                    gkey.AddToken(new SimpleToken('w', "QR"));
                    var finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }


        /// <summary>
        /// Test a generic key generation with 2 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test10_8_Gen3ParB16Bit55555()
        {
            try
            {
                int lopcnt;
                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    var gkey = new Key("vvvvppppxxxxxxxxxxxx-wwwwwwwwxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(new SimpleToken('v', 1));
                    gkey.AddToken(new SimpleToken('p', 2));
                    gkey.AddToken(new SimpleToken('w', "QR"));
                    var finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4) / 4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }

        /// <summary>
        /// Test a generic key generation with 2 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test10_Gen3ParB16Bit55555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvppppxxxxxxxxxxxx-wwwwwwwwxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "1");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "2");
                    gkey.AddToken(2, "w", Key.TokenTypes.Character, "QR");
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }

        /// <summary>
        /// Test a generic key generation with 3 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test14_Gen3DisParB16Bit55555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;
                string result;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvppppxxxxxxxxxxxx-wwwwxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx");
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "1");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "2");
                    gkey.AddToken(2, "w", Key.TokenTypes.Number, "6");
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                    result = gkey.DisassembleKey("v");
                    if (result != "1")
                    {
                        throw new Exception("The first tokens are not equal");
                    }
                    result = gkey.DisassembleKey("p");
                    if (result != "2")
                    {
                        throw new Exception("The second tokens are not equal");
                    }
                    result = gkey.DisassembleKey("w");
                    if (result != "6")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }


        /// <summary>
        /// Test a generic key generation with 5 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test15_Gen5DisParB16Bit55555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;
                string result;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvvvvvppppxxxxxxxx-wwwwxxxxxxxxxxxxxxxx-ssssssssxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxrrrr");
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "14");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "2");
                    gkey.AddToken(2, "w", Key.TokenTypes.Number, "6");
                    gkey.AddToken(3, "s", Key.TokenTypes.Number, "BC");
                    gkey.AddToken(4, "r", Key.TokenTypes.Number, "5");
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                    result = gkey.DisassembleKey("v");
                    if (result != "14")
                    {
                        throw new Exception("The first tokens are not equal");
                    }
                    result = gkey.DisassembleKey("p");
                    if (result != "2")
                    {
                        throw new Exception("The second tokens are not equal");
                    }
                    result = gkey.DisassembleKey("w");
                    if (result != "6")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                    result = gkey.DisassembleKey("s");
                    if (result != "BC")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                    result = gkey.DisassembleKey("r");
                    if (result != "5")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }


        /// <summary>
        /// Test a generic key generation with 5 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test16_Gen5DisParB16BitChk155555()
        {
            int lopcnt;

            try
            {
                Key gkey;
                string finalkey;
                string result;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    gkey =
                        new Key(
                            "vvvvvvvvppppxxxxxxxx-wwwwxxxxxxxxxxxxxxxx-ssssssssxxxxxxxxxxxx-xxxxxxxxxxxxcccccccc-xxxxxxxxxxxxxxxxrrrr", Checksum.ChecksumType.Type1);
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "14");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "2");
                    gkey.AddToken(2, "w", Key.TokenTypes.Number, "6");
                    gkey.AddToken(3, "s", Key.TokenTypes.Number, "BC");
                    gkey.AddToken(4, "r", Key.TokenTypes.Number, "5");
                    finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                    result = gkey.DisassembleKey("v");
                    if (result != "14")
                    {
                        throw new Exception("The first tokens are not equal");
                    }
                    result = gkey.DisassembleKey("p");
                    if (result != "2")
                    {
                        throw new Exception("The second tokens are not equal");
                    }
                    result = gkey.DisassembleKey("w");
                    if (result != "6")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                    result = gkey.DisassembleKey("s");
                    if (result != "BC")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                    result = gkey.DisassembleKey("r");
                    if (result != "5")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }


        /// <summary>
        /// Test a generic key generation with 5 parameters 5-5-5-5-5
        /// </summary>
        [Test]
        public void Test17_Gen5DisParB16BitChk255555()
        {
            int lopcnt;

            try
            {
                string result;

                for (lopcnt = 1; lopcnt < 30; lopcnt++)
                {
                    var gkey = new Key("vvvvvvvvppppxxxxxxxx-wwwwxxxxxxxxxxxxxxxx-ssssssssxxxxxxxxxxxx-xxxxxxxxxxxxcccccccc-xxxxxxxxxxxxxxxxrrrr", Checksum.ChecksumType.Type2);
                    gkey.AddToken(0, "v", Key.TokenTypes.Number, "34");
                    gkey.AddToken(1, "p", Key.TokenTypes.Number, "6");
                    gkey.AddToken(2, "w", Key.TokenTypes.Number, "8");
                    gkey.AddToken(3, "s", Key.TokenTypes.Number, "AB");
                    gkey.AddToken(4, "r", Key.TokenTypes.Number, "3");
                    var finalkey = gkey.CreateKey();
                    if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4)/4))
                    {
                        throw new Exception("Keys are not the same length");
                    }
                    result = gkey.DisassembleKey("v");
                    if (result != "34")
                    {
                        throw new Exception("The first tokens are not equal");
                    }
                    result = gkey.DisassembleKey("p");
                    if (result != "6")
                    {
                        throw new Exception("The second tokens are not equal");
                    }
                    result = gkey.DisassembleKey("w");
                    if (result != "8")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                    result = gkey.DisassembleKey("s");
                    if (result != "AB")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                    result = gkey.DisassembleKey("r");
                    if (result != "3")
                    {
                        throw new Exception("The third tokens are not equal");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(66, 1, "UnExpected Failure. " + e.Message);
            }
        }

        [Test]
        public void GenerateKey()
        {
            var template = "vvvvvvvvppppxxxxxxxx-wwwwxxxxxxxxxxxxxxxx-ssssssssxxxxxxxxxxxx-xxxxxxxxxxxxcccccccccccc-xxxxxxxxxxxxxxxxrrrr";

            var test = new TestData
                           {
                               V = "34",
                               P = "6",
                               W = "8",
                               S = "AB",
                               C = "RSd",
                               R = "3"
                           };

            var gen = new KeyFactory(template);
            var gkey = gen.Generate(test);

            var finalkey = gkey.CreateKey();
            if ((finalkey.Length - 4) != ((gkey.LicenseTemplate.Length - 4) / 4))
            {
                throw new Exception("Keys are not the same length");
            }
            var result = gkey.DisassembleKey("v");
            if (result != "34")
            {
                throw new Exception("The first tokens are not equal");
            }
            result = gkey.DisassembleKey("p");
            if (result != "6")
            {
                throw new Exception("The second tokens are not equal");
            }
            result = gkey.DisassembleKey("w");
            if (result != "8")
            {
                throw new Exception("The third tokens are not equal");
            }
            result = gkey.DisassembleKey("s");
            if (result != "AB")
            {
                throw new Exception("The third tokens are not equal");
            }
            result = gkey.DisassembleKey("r");
            if (result != "3")
            {
                throw new Exception("The third tokens are not equal");
            }
        }

        public class TestData : ITokenizable
        {
            [Token('v', 8)]
            public string V { get; set; }

            [Token('p', 4)]
            public string P { get; set; }

            [Token('w', 4)]
            public string W { get; set; }

            [Token('s', 8)]
            public string S { get; set; }

            [Token('c', 8)]
            public string C { get; set; }

            [Token('r', 4)]
            public string R { get; set; }
        }
    }
}