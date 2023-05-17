using ServerAPI.Models.HocSinh;

namespace ServerAPI.Services.Interface
{
    public interface IHocSinhService
    {
        public Task<bool> AddHocSinh(HocSinh hocSinh);

        public Task<HocSinh> UpdateHocSinh(int maHS, HocSinh hocSinh);
        public Task<bool> DeleteHocSinh(int maHS);
        public Task<HocSinh> GetHocSinhByMaHS(int maHS);
        public Task<List<HocSinh>> GetAllHocSinh();
        public Task<List<HocSinh>> GetHocSinhByTenHocSinh(string tenHS);

    }
}
