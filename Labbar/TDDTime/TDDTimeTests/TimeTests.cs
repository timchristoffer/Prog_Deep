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
            time = new Time();
        }

        [Test]
        public void IsValid_ValidTime_ReturnsTrue()
        {
            // Testar IsValid-metoden med en giltig tid. 

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
            //Testar IsValid-metoden med en ogiltig tid. 

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
            // Testar ToString-metoden med 24-timmarsformat.

            // Arrange
            string expected = "00:00:00"; 

            // Act
            string result = time.ToString(is24HourFormat: true);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_AMPMFormat_ReturnsCorrectFormat()
        {
            // Testar ToString-metoden med AM/PM-format.

            // Arrange
            string expected = "12:00:00 am"; 

            // Act
            string result = time.ToString(is24HourFormat: false);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void IsPM_ReturnsTrueForPMTime()
        {
            // Testar IsPM-metoden med en tid efter middag.  

            // Arrange
            Time pmTime = new Time(15, 30, 45); 

            // Act
            bool isPM = pmTime.IsPM();

            // Assert
            Assert.That(isPM, Is.True);
        }

        [Test]
        public void IsPM_ReturnsFalseForAMTime()
        {
            // Testar IsPM-metoden med en tid före middag.

            // Arrange
            Time amTime = new Time(5, 30, 45); 

            // Act
            bool isPM = amTime.IsPM();

            // Assert
            Assert.That(isPM, Is.False);
        }

        [Test]
        public void OperatorOverloading_PrefixIncrement_ReturnsCorrectTime()
        {
            // Testar operatoröverlagringen för prefix ökning (++Time).

            // Arrange
            Time initialTime = new Time(12, 30, 45);

            // Act
            Time incrementedTime = ++initialTime;

            // Assert
            Assert.That(incrementedTime, Is.EqualTo(new Time(12, 30, 46)));
        }

        [Test]
        public void OperatorOverloading_PrefixDecrement_ReturnsCorrectTime()
        {
            // Testar operatoröverlagringen för prefix minskning (--Time).

            // Arrange
            Time initialTime = new Time(12, 30, 45); 

            // Act
            Time decrementedTime = --initialTime;

            // Assert
            Assert.That(decrementedTime, Is.EqualTo(new Time(12, 30, 44)));
        }

        [Test]
        public void OperatorOverloading_Equality_ReturnsTrueForEqualTimes()
        {
            // Testar operatoröverlagringen för likhet (==) med två lika tider.

            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 45);

            // Act & Assert
            Assert.That(time1 == time2, Is.True);
        }

        [Test]
        public void OperatorOverloading_Equality_ReturnsFalseForUnequalTimes()
        {
            // Testar operatoröverlagringen för likhet (==) med två olika tider.

            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 46);

            // Act & Assert
            Assert.That(time1 == time2, Is.False);
        }

        [Test]
        public void OperatorOverloading_Inequality_ReturnsFalseForEqualTimes()
        {
            // Testar operatoröverlagringen för olikhet (!=) med två lika tider.

            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 45);

            // Act & Assert
            Assert.That(time1 != time2, Is.False);
        }

        [Test]
        public void OperatorOverloading_Inequality_ReturnsTrueForUnequalTimes()
        {
            // Testar operatoröverlagringen för olikhet (!=) med två olika tider.

            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 46);

            // Act & Assert
            Assert.That(time1 != time2, Is.True);
        }

        [Test]
        public void GreaterThanOperator_ReturnsTrue_WhenTime1IsGreaterThanTime2()
        {
            // Testar överlagring av större än-operatorn (>) för tid.

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
            // Testar överlagring av mindre än-operatorn (<) för tid.

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
            // Testar överlagring av större än eller lika med-operatorn (>=) för tid.

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
            // Testar överlagring av mindre än eller lika med-operatorn (<=) för tid.

            // Arrange
            Time time1 = new Time(6, 15, 30);
            Time time2 = new Time(6, 15, 30);

            // Act
            bool result = time1 <= time2;

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAM_ReturnsTrue_WhenTimeIsBeforeNoon()
        {
            // Testar IsAM-metoden för att avgöra om en tid är före middag.

            // Arrange
            Time amTime = new Time(10, 30, 0);

            // Act
            bool result = amTime.IsAM();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAM_ReturnsFalse_WhenTimeIsAfterNoon()
        {
            // Testar IsAM-metoden för att avgöra om en tid är efter middag.

            // Arrange
            Time pmTime = new Time(14, 45, 0);

            // Act
            bool result = pmTime.IsAM();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsPM_ReturnsTrue_WhenTimeIsAfterNoon()
        {
            // Testar IsPM-metoden för att avgöra om en tid är efter middag.

            // Arrange
            Time pmTime = new Time(14, 45, 0);

            // Act
            bool result = pmTime.IsPM();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsPM_ReturnsFalse_WhenTimeIsBeforeNoon()
        {
            // Testar IsPM-metoden för att avgöra om en tid är före middag.

            // Arrange
            Time amTime = new Time(10, 30, 0);

            // Act
            bool result = amTime.IsPM();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void AddSeconds_ReturnsCorrectTime_WhenAddingPositiveSeconds()
        {
            // Testar addition av sekunder till en tid.

            // Arrange
            Time initialTime = new Time(12, 30, 45);
            int secondsToAdd = 30;

            // Act
            Time result = initialTime + secondsToAdd;

            // Assert
            Assert.That(result.ToString(true), Is.EqualTo("12:31:15"));
        }

        [Test]
        public void SubtractSeconds_ReturnsCorrectTime_WhenSubtractingPositiveSeconds()
        {
            // Testar subtraktion av sekunder från en tid.

            // Arrange
            Time initialTime = new Time(12, 30, 45);
            int secondsToSubtract = 15;

            // Act
            Time result = initialTime - secondsToSubtract;

            // Assert
            Assert.That(result.ToString(true), Is.EqualTo("12:30:30"));
        }

        [Test]
        public void GetHashCode_ReturnsExpectedHashCode()
        {
            // Testar GetHashCode-metoden för att få hashkoden för en tid.

            // Arrange
            Time time1 = new Time(12, 30, 45);
            Time time2 = new Time(12, 30, 45); // Same time as time1

            // Act
            int hashCode1 = time1.GetHashCode();
            int hashCode2 = time2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.EqualTo(hashCode2));
        }

    }
}