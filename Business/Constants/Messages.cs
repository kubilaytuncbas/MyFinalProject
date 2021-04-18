using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //Publicler PascalCase yazılır.
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        

        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string MoreCategoryError="CategoryId 10 dan fazla olamaz";
        public static string SameProductName="2 Ürün aynı İsime sahip olamaz";
        public static string CategoryLimitExceded="Kategori limiti aşıldı, yeni ürün eklenemez";
    }
}
