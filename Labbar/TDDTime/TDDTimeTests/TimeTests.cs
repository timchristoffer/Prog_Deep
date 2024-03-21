using NUnit.Framework;
using TDDTime; // Assuming Time class is in the same namespace

namespace TDDTime
{
    public class Tests
    {
        private Time time;

        [SetUp]
        public void Setup()
        {
            // Initial setup before each test method
            time = new Time(12, 30, 45); // Example time: 12:30:45
        }

        [Test]
        public void IsValid_ValidTime_ReturnsTrue()
        {
            // Arrange
            Time validTime = new Time(12, 30, 45);

            // Act
            bool isValid = validTime.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void IsValid_InvalidTime_ReturnsFalse()
        {
            // Arrange
            Time invalidTime = new Time(25, 70, 80);

            // Act
            bool isValid = invalidTime.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void ToString_24HourFormat_ReturnsCorrectFormat()
        {
            // Arrange
            string expected = "12:30:45";

            // Act
            string result = time.ToString(is24HourFormat: true);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_AMPMFormat_ReturnsCorrectFormat()
        {
            // Arrange
            string expected = "12:30:45 pm";

            // Act
            string result = time.ToString(is24HourFormat: false);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void IsPM_ReturnsTrueForPMTime()
        {
            // Arrange
            Time pmTime = new Time(15, 30, 45); // 3:30:45 PM

            // Act
            bool isPM = pmTime.IsPM();

            // Assert
            Assert.That(isPM, Is.True);
        }

        [Test]
        public void IsPM_ReturnsFalseForAMTime()
        {
            // Arrange
            Time amTime = new Time(5, 30, 45); // 5:30:45 AM

            // Act
            bool isPM = amTime.IsPM();

            // Assert
            Assert.That(isPM, Is.False);
        }

        [Test]
        public void OperatorOverloading_PrefixIncrement_ReturnsCorrectTime()
        {
            // Arrange
            Time initialTime = new Time(12, 30, 45); // 12:30:45

            // Act
            Time incrementedTime = ++initialTime;

            // Assert
            Assert.That(incrementedTime, Is.EqualTo(new Time(12, 30, 46)));
        }

        [Test]
        public void OperatorOverloading_PrefixDecrement_ReturnsCorrectTime()
        {
            // Arrange
            Time initialTime = new Time(12, 30, 45); // 12:30:45

            // Act
            Time decrementedTime = --initialTime;

            // Assert
            Assert.That(decrementedTime, Is.EqualTo(new Time(12, 30, 44)));
        }

        [Test]
        public void OperatorOverloading_Equality_ReturnsTrueForEqualTimes()
        {
            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 45);

            // Act & Assert
            Assert.That(time1 == time2, Is.True);
        }

        [Test]
        public void OperatorOverloading_Equality_ReturnsFalseForUnequalTimes()
        {
            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 46);

            // Act & Assert
            Assert.That(time1 == time2, Is.False);
        }

        [Test]
        public void OperatorOverloading_Inequality_ReturnsFalseForEqualTimes()
        {
            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 45);

            // Act & Assert
            Assert.That(time1 != time2, Is.False);
        }

        [Test]
        public void OperatorOverloading_Inequality_ReturnsTrueForUnequalTimes()
        {
            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 46);

            // Act & Assert
            Assert.That(time1 != time2, Is.True);
        }

        [Test]
        public void GreaterThanOperator_ReturnsTrue_WhenTime1IsGreaterThanTime2()
        {
            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(10, 15, 30);

            // Act
            bool result = time1 > time2;

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void LessThanOperator_ReturnsTrue_WhenTime1IsLessThanTime2()
        {
            // Arrange
            Time time1 = new Time(8, 45, 15);
            Time time2 = new Time(12, 30, 45);

            // Act
            bool result = time1 < time2;

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void GreaterThanOrEqualOperator_ReturnsTrue_WhenTime1IsGreaterThanOrEqualToTime2()
        {
            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 45);

            // Act
            bool result = time1 >= time2;

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void LessThanOrEqualOperator_ReturnsTrue_WhenTime1IsLessThanOrEqualToTime2()
        {
            // Arrange
            Time time1 = new Time(6, 15, 30);
            Time time2 = new Time(6, 15, 30);

            // Act
            bool result = time1 <= time2;

            // Assert
            Assert.That(result, Is.True);
        }
    }
}