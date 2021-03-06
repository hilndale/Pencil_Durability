using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilDurability;

namespace PencilDurabilityTests
{
    [TestClass]
    public class PencilTests
    {
        [TestMethod]
        public void WhenPencilCreatedDefaultLengthIsFive()
        {
            Pencil pencil = new Pencil();
            Assert.AreEqual(5, pencil.Length);
        }

        [TestMethod]
        public void LengthAlwaysZeroOrPositive()
        {
            Pencil pencil = new Pencil(-1);
            Assert.AreEqual(0, pencil.Length);
        }

        [TestMethod]
        public void InitialPointDurabilityCannotBeLessThanOne()
        {
            Pencil pencil = new Pencil(5, -1);
            Assert.AreEqual(1, pencil.PointDurability);
        }

        [TestMethod]
        public void InitialEraserDurabilityCannotBeLessThanZero()
        {
            Pencil pencil = new Pencil(5, 20, -1);
            Assert.AreEqual(0, pencil.EraserDurability);
        }


        [TestMethod]
        public void WhenLengthIs5AndSharpenCalledLengthIsReducedBy1()
        {
            Pencil pencil = new Pencil(5);
            pencil.Sharpen();
            Assert.AreEqual(4, pencil.Length);
        }


        [TestMethod]
        public void WhenSharpenCalledCurrentPointDurabilityIsResetToInitialPointDurability()
        {
            Pencil pencil = new Pencil(5, 20);
            pencil.Sharpen();
            Assert.AreEqual(20, pencil.PointDurability);
        }

        [TestMethod]
        public void WhenSharpenCalledIfLengthIs0PointDurabilityWontBeReset()
        {
            Pencil pencil = new Pencil(0, 19);
            pencil.Sharpen();
            Assert.AreEqual(19, pencil.PointDurability);
        }


        [TestMethod]
        public void WheWritePassedUpperCaseCharCurrentPointDurabilityDecreasesByTwo()
        {
            Pencil pencil = new Pencil();
            pencil.Write("A");
            Assert.AreEqual(18, pencil.PointDurability);
        }


        [TestMethod]
        public void WhenWritePassedUpperCaseCharAndCurrentPointDurabilityIsOneCurrentPointDurabiltySetToZero()
        {
            Pencil pencil = new Pencil(1, 1);
            pencil.Write("A");
            Assert.AreEqual(0, pencil.PointDurability);
        }

        [TestMethod]
        public void WhenWritePassedEmptyStringPointDurabilityNotAffected()
        {
            Pencil pencil = new Pencil();
            pencil.Write(" ");
            Assert.AreEqual(20, pencil.PointDurability);
        }

        [TestMethod]
        public void WhenWritePassedLowerCaseCharPointDurabilityDecreasedByOne()
        {
            Pencil pencil = new Pencil();
            pencil.Write("a");
            Assert.AreEqual(19, pencil.PointDurability);
        }

        [TestMethod]
        public void WhenWritePassedSpecialCharPointDurabilityDecreasedByOne()
        {
            Pencil pencil = new Pencil();
            pencil.Write("$");
            Assert.AreEqual(19, pencil.PointDurability);
        }

        [TestMethod]
        public void WhenWritePassedNumberDurabilityDecreasedByOne()
        {
            Pencil pencil = new Pencil();
            pencil.Write("9");
            Assert.AreEqual(19, pencil.PointDurability);
        }

        [TestMethod]
        public void WhenWritePassedOneCharStringThatStringIsAppendedToPaper()
        {
            Pencil pencil = new Pencil();
            string newText = "a";
            pencil.Write(newText);
            Assert.AreEqual("a", pencil.Paper);
        }

        [TestMethod]
        public void WhenWritePassedAdditionalStringItAppendsToExistingText()
        {
            Pencil pencil = new Pencil(5, 20, 20, "A cat meows");
            string appendText = " a dog barks.";
            pencil.Write(appendText);
            Assert.AreEqual("A cat meows a dog barks.", pencil.Paper);
        }

        [TestMethod]
        public void WhenWritePassedFiveCharStringAndPointDurabilityIs4OnlyFirst4CharsWillBeWritten()
        {
            Pencil pencil = new Pencil(5, 4);
            string text = "chuck";
            pencil.Write(text);
            Assert.AreEqual("chuc ", pencil.Paper);
        }

    [TestMethod]
    public void WhenErasePassedOneCharEraserDurabilityDecreasesByOne()
    {
        Pencil pencil = new Pencil(5,20,20, "a");
        string textToErase= "a";
        pencil.Erase(textToErase);
        Assert.AreEqual(19, pencil.EraserDurability);
    }

        [TestMethod]
        public void WhenErasePassedWhiteSpaceEraserDurabilityIsNotAffectedByWhiteSpace()
        {
            Pencil pencil = new Pencil(5, 20, 20, "test  test");
            string textToErase = "  ";
            pencil.Erase(textToErase);
            Assert.AreEqual(18, pencil.EraserDurability);
        }

        [TestMethod]
        public void WhenErasePassedStringItErasesOppositeOrderStringWasWrittenAndReplacesWithWhiteSpace()
        {
            Pencil pencil = new Pencil(5, 20, 1, "Bill");
            pencil.Erase("Bill");
            Assert.AreEqual("Bil ", pencil.Paper);
        }

        [TestMethod]
        public void WhenErasePassedTextToEraseWithTwoInstancesofSameWordRemovesLastInstanceOfWordFromString()
        {
            Pencil pencil = new Pencil(5, 30, 30, "how much wood can a woodchuck");
            pencil.Erase("wood");
            Assert.AreEqual("how much wood can a     chuck", pencil.Paper);
        }

        [TestMethod]
        public void WhenErasePassedStringofTwoCharsEraserDurabilityDecreasesByTwo()
        {
            Pencil pencil = new Pencil(5, 20, 20, "ab");
            pencil.Erase("ab");
            Assert.AreEqual(18, pencil.EraserDurability);
        }

        [TestMethod]
        public void WhenEditPassedNewTextInsertsThatText()
        {
            Pencil pencil = new Pencil(5, 20, 20, "An apple a day");
            pencil.Erase("apple");
            string word = "onion";
            pencil.Edit(word);
            Assert.AreEqual("An onion a day", pencil.Paper);
        }

        [TestMethod]
        public void WhenEditPassedStringThatCollidesWithExistingTextCollsionsReplacedWithAtSymbol()
        {
            Pencil pencil = new Pencil(5, 20, 20, "An apple a day");
            pencil.Erase("apple");
            string newText = "artichoke";
            pencil.Edit(newText);
            Assert.AreEqual("An artich@k@ay", pencil.Paper);
        }

        [TestMethod]
        public void WhenEditPassedTextWithTwoCollisionsOneUpperCaseSixLowerCaseCharsPointDurabilityDecreasesByTen()
        {
            Pencil pencil = new Pencil(5, 20, 20, "An apple a day");
            pencil.Erase("apple");
            string newText = "ArtichOke";
            pencil.Edit(newText);
            Assert.AreEqual(10, pencil.PointDurability);
        }

        [TestMethod]
        public void WhenEditPassedStringLongerThanPaperAppendsExcessCharsToPaper()
        {
            Pencil pencil = new Pencil(5, 20, 20, "Buffalo Bill");
            pencil.Erase("Bill");
            string newText = ("William");
            pencil.Edit(newText);
            Assert.AreEqual("Buffalo William", pencil.Paper);

        }
    }
}
