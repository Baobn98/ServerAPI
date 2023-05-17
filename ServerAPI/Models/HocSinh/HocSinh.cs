namespace ServerAPI.Models.HocSinh
{
    public class HocSinh
    {
        public int MaHS { get; set; }
        public string TenHS { get; set; }
        public int MaLop { get; set; }
        public int TuoiHS { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public ICollection<MonHoc> MonHocs { get; set; }

    }
}
