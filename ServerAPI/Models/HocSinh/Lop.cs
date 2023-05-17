namespace ServerAPI.Models.HocSinh
{
    public class Lop
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public ICollection<HocSinh> HocSinh { get; set;}
    }
}
