﻿namespace Catalog.Data.Infrastructure;

public class CatalogDbSeed
{
    public static void Seed(ModelBuilder builder)
    {
        var types = new List<CatalogTypeEntity>
        {
            new CatalogTypeEntity { Id = 1, Title = "Shoes", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
            new CatalogTypeEntity { Id = 2, Title = "Hoodie", CreatedAt = DateTime.UtcNow, UpdatedAt = null }
        };

        var brands = new List<CatalogBrandEntity>
        {
            new CatalogBrandEntity { Id = 1, Title = "Nike", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
            new CatalogBrandEntity { Id = 2, Title = "Adidas", CreatedAt = DateTime.UtcNow, UpdatedAt = null }
        };

        var items = new List<CatalogItemEntity>
        {
            new CatalogItemEntity
            {
                Id = 1,
                Title = "Nike Dunk Low Retro Premium",
                Description = "Created for the hardwood but taken to the streets, " +
                "the '80s b-ball icon returns with " +
                "classic details and throwback hoops flair. ",
                Price = 100,
                PictureFile = "1.png",
                TypeId = 1,
                BrandId = 1,
                Quantity = 5,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },

            new CatalogItemEntity
            {
                Id = 2,
                Title = "Nike Dunk Mid",
                Description = "Created for the hardwood but taken to the streets, " +
                "the '80s b-ball icon returns " +
                "with crisp leather and and classic \"Panda\" color blocking.",
                Price = 120,
                PictureFile = "2.png",
                TypeId = 1,
                BrandId = 1,
                Quantity = 10,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },

            new CatalogItemEntity
            {
                Id = 3,
                Title = "Jordan Flight Fleece",
                Description = "Meet your new go-to hoodie. Heavyweight fleece feels" +
                " super soft, and the comfy, relaxed fit will " +
                "have you reaching for it again and again.",
                Price = 100,
                PictureFile = "3.png",
                TypeId = 2,
                BrandId = 1,
                Quantity = 15,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },

            new CatalogItemEntity
            {
                Id = 4,
                Title = "Forum Low",
                Description = "These are not just sneakers, but a real symbol" +
                " of the era. The adidas Forum model appeared in 1984 and won" +
                " love not only on basketball courts, but also in show business.",
                Price = 110,
                PictureFile = "4.png",
                TypeId = 1,
                BrandId = 2,
                Quantity = 6,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },

            new CatalogItemEntity
            {
                Id = 5,
                Title = "ENTRADA 22 SWEAT HOODIE",
                Description = "For your team. For your planet." +
                " Created with grassroots football in mind," +
                " the Entrada 22 range gives you everything " +
                "you need to make your game feel and look more beautiful. ",
                Price = 60,
                PictureFile = "5.png",
                TypeId = 2,
                BrandId = 2,
                Quantity = 10,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },

            new CatalogItemEntity
            {
                Id = 6,
                Title = "Ja 1",
                Description = "Ja Morant became the superstar he is today " +
                "by repeatedly sinking jumpers on crooked rims, " +
                "jumping on tractor tires and dribbling through traffic " +
                "cones in steamy South Carolina summers.",
                Price = 130,
                PictureFile = "6.png",
                TypeId = 1,
                BrandId = 1,
                Quantity = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },

            new CatalogItemEntity
            {
                Id = 7,
                Title = "Air Jordan I High G",
                Description = "Feel unbeatable," +
                " from the tee box to the final putt." +
                " Inspired by one of the most iconic sneakers of all time," +
                " the Air Jordan 1 G is an instant classic on the course. ",
                Price = 190,
                PictureFile = "7.png",
                TypeId = 1,
                BrandId = 1,
                Quantity = 5,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },
        };

        builder.Entity<CatalogTypeEntity>().HasData(types);
        builder.Entity<CatalogBrandEntity>().HasData(brands);
        builder.Entity<CatalogItemEntity>().HasData(items);

    }
}
