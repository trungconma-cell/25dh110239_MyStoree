using System.Collections.Generic;
using PagedList;
using _25dh110239.Models;

namespace _25dh110239.Models.ViewModel
{
    public class HomeProductVM
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;

        // Danh sách sản phẩm nổi bật
        public List<Product> FeaturedProducts { get; set; }

        // Danh sách Sản phẩm mới đã phân trang
        public IPagedList<Product> NewProducts { get; set; }
    }
}