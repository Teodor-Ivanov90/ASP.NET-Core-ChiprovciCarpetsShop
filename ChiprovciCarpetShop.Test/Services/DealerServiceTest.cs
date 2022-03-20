namespace ChiprovciCarpetsShop.Test.Services
{
    using ChiprovciCarpetsShop.Data.Models;
    using ChiprovciCarpetsShop.Services.Dealers;
    using ChiprovciCarpetsShop.Test.Mocks;
    using Xunit;
    public class DealerServiceTest
    {
        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIsDealer()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            data.Dealers.Add(new Dealer { UserId = "TestUserId" });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            //Act
            var result = dealerService.IsDealer("TestUserId");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDealerShouldReturnFalseWhenUserIsNotDealer()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            data.Dealers.Add(new Dealer { UserId = "TestUserId" });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            //Act
            var result = dealerService.IsDealer("WrongUserId");

            //Assert
            Assert.False(result);
        }
    }
}
