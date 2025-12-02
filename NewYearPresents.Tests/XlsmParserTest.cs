using NewYearPresents.Parser;

namespace NewYearPresents.Tests
{
    [TestClass]
    public sealed class XlsmParserTest : XlsmParser
    {
        [TestMethod]
        [DataRow("Айналайын карамель пп/пл 1,0 (6 кг)")]
        [DataRow("Достык карамель в пп/пл 1.0 (6 кг)")]
        [DataRow("Рахат карамель  пп/пл 1.0 (6 кг)")]
        [DataRow("Малина со сливками карамель пп/пл 1.0 (6 кг)")]
        public void GetWeightAndPiecesFromStringTest1(string source)
        {
            var x = GetTotalWeightFromString(source);
            if(x == 6.0f)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Мармелад  желейный в сахаре 0,250 (13 шт)")]
        [DataRow("Мармелад Махаббат от Рахат 0,250 (13 шт)")]
        [DataRow("Мармелад Ягоды от Рахат 0,250 (13 шт)")]
        public void GetWeightAndPiecesFromStringTest2(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 0.250f * 13)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Шоколад Almaty пористый особый 80гр.(32 шт)")]
        [DataRow("Шоколад Almaty пористый тёмный 80гр.(32 шт)")]
        [DataRow("Шоколад Almaty пористый молочный 90 гр.(32 шт)")]
        public void GetWeightAndPiecesFromStringTest3(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x >= 0.080f * 32)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Молоко концентрированное стерильное цельное 8,6%, ж/б 300 гр. (45 шт)")]
        [DataRow("Молоко цельное сгущ. с сахаром 8,5% дойпак 280гр (24шт) Глубокое")]
        [DataRow("Молоко цельное сгущенное с сахаром  8,5% ж/б 380 гр (30 шт)")]
        public void GetWeightAndPiecesFromStringTest4(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x >= 0.280f * 24)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Молоко сгущенное  0,600кг 8,5% ж /15шт")]
        [DataRow("Молоко сгущенное вареное 0,600кг 8,5% ж /15шт")]
        public void GetWeightAndPiecesFromStringTest5(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 0.600f * 15)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Asia с кунжутом  печ.отсадное   6*1,5кг/9кг")]
        public void GetWeightAndPiecesFromStringTest6(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 9f * 1)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("ФАС/Конфеты ЛЮБИМКА корп. 1 кг/5 шт/Сладуница/")]
        [DataRow("ФАС/Конфеты ЛАКТИ суфлейные Йогурт/клубника/сливки 1 кг/5 шт/Сладуница/")]
        [DataRow("ФАС/Конфеты МИННИ-УХ суфлейные 1 кг/5 шт/Сладуница/")]
        [DataRow("ФАС/Конфеты МИСС КО-КО Типа ассорти с начинкой 1 кг/5 шт/Сладуница/")]
        public void GetWeightAndPiecesFromStringTest7(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 1f * 5)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Перец черный молотый \"слон\" 100г (55 шт)")]
        [DataRow("Перец черный молотый \"слон\" 100г (55 шт)")]
        public void GetWeightAndPiecesFromStringTest8(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x >= 0.05f * 55)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Алтын кесе кения чай черн.гран.250гр/40шт")]
        public void GetWeightAndPiecesFromStringTest9(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 0.25f * 40)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Фас/конфеты апельсиновое чудо пралин.1 кг/5 шт/сладуница/")]
        [DataRow("Фас/конфеты бурундук достал фундук пралин.1 кг/5 шт/сладуница/")]
        [DataRow("Фас/конфеты звездная парочка пралин.1 кг/5 шт/сладуница/")]
        [DataRow("Фас/конфеты клубничное чудо жел. глазир.1 кг/5 шт/сладуница/")]
        public void GetWeightAndPiecesFromStringTest10(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 1f * 5)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Соевый соус чесночно-острый 0,500/20шт")]
        [DataRow("Соевый соус традиционный 0,500/20шт")]
        [DataRow("Соевый соус классический 0,500/20шт")]
        public void GetWeightAndPiecesFromStringTest11(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 0.5f * 20)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Сахарная пудра 50г/120шт")]
        public void GetWeightAndPiecesFromStringTest12(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 0.05f * 120)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }

        [TestMethod]
        [DataRow("Паштет печеночный со слив.маслом с опен-крышкой 0,120/42шт / кублей")]
        public void GetWeightAndPiecesFromStringTest13(string source)
        {
            var x = GetTotalWeightFromString(source);
            if (x == 0.12f * 42)
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail();
        }
    }
}
