using ChiprovciCarpetsShop.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChiprovciCarpetsShop.Test.Mocks
{
    public static class DatabaseMock
    {
        public static ChiprovciCarpetsDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ChiprovciCarpetsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ChiprovciCarpetsDbContext(dbContextOptions);
            }
        }
    }
}
